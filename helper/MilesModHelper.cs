// Miles Mod Helper
// One-time helper that lets the website's "Install" buttons put mods straight into a Gale profile.
//
// How it works:
//   1. Run once with no arguments -> registers the milesmods:// link type for the current Windows user.
//   2. The website opens milesmods://install/<ModId>[,<ModId>...] (or milesmods://install/all).
//   3. The helper reads mods.json from the site (never a URL from the link itself), shows the
//      friend's Gale profiles for Lethal Company, downloads the zip from this repo's GitHub
//      Releases, checks its SHA-256, and runs:
//          Gale.exe --game lethal-company --profile "<picked>" --install "<zip>"
//      which is the same code path as Gale's own "Import local mod" button.
//   4. It watches Gale's database until the mod shows up in that profile.
//
// Build: see build.sh (Mono mcs) or build.cmd (Windows, .NET Framework csc). Targets .NET Framework 4.x,
// which ships with Windows 10/11, so friends do not need to install anything else.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.Win32;

[assembly: AssemblyTitle("Miles Mod Helper")]
[assembly: AssemblyProduct("Miles Mod Helper")]
[assembly: AssemblyDescription("Installs SleepyMiles' Lethal Company mods into Gale")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace MilesModHelper
{
    static class Config
    {
        public const string Version = "1.0.0";
        public const string Scheme = "milesmods";
        public const string SiteUrl = "https://sleepymiles.github.io/miles-lc-mods/";
        public const string IndexUrl = SiteUrl + "mods.json";
        // Zips are only ever downloaded from this repo's releases. A link can't point the helper anywhere else.
        public const string AllowedDownloadPrefix = "https://github.com/SleepyMiles/miles-lc-mods/releases/download/";
        public const string GameSlug = "lethal-company";
        public const string AppName = "Miles Mod Helper";

        // Time to let Gale load the Thunderstore mod list when we had to open it ourselves.
        // Gale's CLI install doesn't wait for that list, and dependencies are looked up in it.
        public const int GaleWarmupSeconds = 20;
        public const int InstallTimeoutSeconds = 180;
    }

    // ------------------------------------------------------------------ entry point

    static class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            // GitHub only accepts TLS 1.2+; .NET Framework 4.x may default to older protocols.
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

            if (args.Length >= 1 && args[0] == "--selftest")
                return SelfTest.Run(args.Skip(1).ToArray());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                if (args.Length == 0)
                {
                    Application.Run(new SetupForm());
                    return 0;
                }

                if (args[0] == "--uninstall")
                {
                    Setup.Unregister();
                    Ui.Info("The helper was removed. Website Install buttons won't work until you run it again.");
                    return 0;
                }

                if (args[0] == "--register")
                {
                    Setup.Register(Setup.InstallSelf());
                    return 0;
                }

                LinkRequest request;
                string error;
                if (!LinkRequest.TryParse(args[0], out request, out error))
                {
                    Ui.Error("That install link isn't valid:\n" + args[0] + "\n\n" + error);
                    return 1;
                }

                Application.Run(new InstallForm(request));
                return 0;
            }
            catch (Exception ex)
            {
                Ui.Error("Something went wrong:\n\n" + ex.Message);
                return 1;
            }
        }
    }

    static class Ui
    {
        public static void Info(string text) { MessageBox.Show(text, Config.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        public static void Error(string text) { MessageBox.Show(text, Config.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    // ------------------------------------------------------------------ link parsing

    class LinkRequest
    {
        public bool All;
        public List<string> Ids = new List<string>();

        static readonly Regex IdPattern = new Regex(@"^[A-Za-z0-9_.\- ]{1,100}$");

        public static bool TryParse(string link, out LinkRequest request, out string error)
        {
            request = null;
            error = null;
            if (link == null) { error = "Empty link."; return false; }

            string prefix = Config.Scheme + ":";
            if (!link.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) { error = "Not a " + Config.Scheme + ":// link."; return false; }

            string rest = Uri.UnescapeDataString(link.Substring(prefix.Length)).Trim().TrimStart('/').TrimEnd('/');
            string[] parts = rest.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2 || !parts[0].Equals("install", StringComparison.OrdinalIgnoreCase))
            {
                error = "Expected " + Config.Scheme + "://install/<ModName>.";
                return false;
            }

            var r = new LinkRequest();
            foreach (string raw in parts[1].Split(','))
            {
                string id = raw.Trim();
                if (id.Length == 0) continue;
                if (id.Equals("all", StringComparison.OrdinalIgnoreCase)) { r.All = true; continue; }
                if (!IdPattern.IsMatch(id)) { error = "Bad mod name: " + id; return false; }
                if (!r.Ids.Contains(id, StringComparer.OrdinalIgnoreCase)) r.Ids.Add(id);
            }
            if (!r.All && r.Ids.Count == 0) { error = "No mods in the link."; return false; }

            request = r;
            return true;
        }
    }

    // ------------------------------------------------------------------ mods.json

    class ModEntry
    {
        public string Id, Name, Author, Version, Description, Download, Sha256;
        public long Size;

        public string Label { get { return Name + "  v" + Version + (string.IsNullOrEmpty(Author) ? "" : "  by " + Author); } }
    }

    class ModIndex
    {
        public List<ModEntry> Mods = new List<ModEntry>();
        public string HelperVersion, HelperDownload;

        public static ModIndex Fetch()
        {
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers[HttpRequestHeader.UserAgent] = Config.AppName + "/" + Config.Version;
                wc.Headers[HttpRequestHeader.CacheControl] = "no-cache";
                // Query string sidesteps GitHub Pages' CDN cache so a fresh update shows up right away.
                string json = wc.DownloadString(Config.IndexUrl + "?t=" + DateTime.UtcNow.Ticks);
                return Parse(json);
            }
        }

        public static ModIndex Parse(string json)
        {
            var root = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;
            if (root == null) throw new Exception("mods.json is not a JSON object.");

            var index = new ModIndex();
            object helperObj;
            if (root.TryGetValue("helper", out helperObj))
            {
                var h = helperObj as Dictionary<string, object>;
                if (h != null) { index.HelperVersion = Str(h, "version"); index.HelperDownload = Str(h, "download"); }
            }

            object modsObj;
            if (!root.TryGetValue("mods", out modsObj) || !(modsObj is object[]))
                throw new Exception("mods.json has no \"mods\" list.");

            foreach (object o in (object[])modsObj)
            {
                var m = o as Dictionary<string, object>;
                if (m == null) continue;
                var e = new ModEntry
                {
                    Id = Str(m, "id"),
                    Name = Str(m, "name") ?? Str(m, "id"),
                    Author = Str(m, "author"),
                    Version = Str(m, "version"),
                    Description = Str(m, "description"),
                    Download = Str(m, "download"),
                    Sha256 = Str(m, "sha256"),
                };
                object size;
                if (m.TryGetValue("size", out size) && size != null) { long s; long.TryParse(size.ToString(), out s); e.Size = s; }
                if (string.IsNullOrEmpty(e.Id) || string.IsNullOrEmpty(e.Download)) continue;
                index.Mods.Add(e);
            }
            return index;
        }

        static string Str(Dictionary<string, object> d, string key)
        {
            object v;
            return d.TryGetValue(key, out v) && v != null ? v.ToString() : null;
        }

        public List<ModEntry> Resolve(LinkRequest req, out List<string> missing)
        {
            missing = new List<string>();
            if (req.All) return Mods.ToList();
            var result = new List<ModEntry>();
            foreach (string id in req.Ids)
            {
                var m = Mods.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (m == null) missing.Add(id); else result.Add(m);
            }
            return result;
        }
    }

    static class Versions
    {
        public static bool IsNewer(string candidate, string current)
        {
            Version a, b;
            if (!System.Version.TryParse(candidate ?? "", out a) || !System.Version.TryParse(current ?? "", out b)) return false;
            return a > b;
        }
    }

    // ------------------------------------------------------------------ Gale database (read-only)

    // Gale keeps its profiles in %APPDATA%\com.kesomannen.gale\data.sqlite3 (see Gale's src-tauri/src/db).
    // We read it with winsqlite3.dll, which is part of Windows 10/11, so no extra DLLs ship with the helper.
    static class Sqlite
    {
        const string Lib = "winsqlite3";
        const int SQLITE_OK = 0, SQLITE_ROW = 100, SQLITE_DONE = 101, SQLITE_OPEN_READONLY = 1;

        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern int sqlite3_open_v2(byte[] filename, out IntPtr db, int flags, IntPtr vfs);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern int sqlite3_close_v2(IntPtr db);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern int sqlite3_busy_timeout(IntPtr db, int ms);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern int sqlite3_prepare_v2(IntPtr db, byte[] sql, int nByte, out IntPtr stmt, IntPtr tail);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern int sqlite3_step(IntPtr stmt);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern int sqlite3_finalize(IntPtr stmt);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern int sqlite3_column_count(IntPtr stmt);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern IntPtr sqlite3_column_text(IntPtr stmt, int col);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern int sqlite3_column_bytes(IntPtr stmt, int col);
        [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] static extern IntPtr sqlite3_errmsg(IntPtr db);

        static byte[] Z(string s) { return Encoding.UTF8.GetBytes(s + "\0"); }

        static string Utf8(IntPtr p)
        {
            if (p == IntPtr.Zero) return null;
            int len = 0;
            while (Marshal.ReadByte(p, len) != 0) len++;
            var buf = new byte[len];
            Marshal.Copy(p, buf, 0, len);
            return Encoding.UTF8.GetString(buf);
        }

        public static List<string[]> Query(string path, string sql)
        {
            IntPtr db;
            int rc = sqlite3_open_v2(Z(path), out db, SQLITE_OPEN_READONLY, IntPtr.Zero);
            if (rc != SQLITE_OK)
            {
                string msg = db != IntPtr.Zero ? Utf8(sqlite3_errmsg(db)) : "code " + rc;
                if (db != IntPtr.Zero) sqlite3_close_v2(db);
                throw new Exception("Couldn't open Gale's database: " + msg);
            }
            try
            {
                sqlite3_busy_timeout(db, 3000);
                IntPtr stmt;
                rc = sqlite3_prepare_v2(db, Z(sql), -1, out stmt, IntPtr.Zero);
                if (rc != SQLITE_OK) throw new Exception("Couldn't read Gale's database: " + Utf8(sqlite3_errmsg(db)));
                try
                {
                    var rows = new List<string[]>();
                    int cols = sqlite3_column_count(stmt);
                    while (true)
                    {
                        rc = sqlite3_step(stmt);
                        if (rc == SQLITE_DONE) break;
                        if (rc != SQLITE_ROW) throw new Exception("Couldn't read Gale's database: " + Utf8(sqlite3_errmsg(db)));
                        var row = new string[cols];
                        for (int i = 0; i < cols; i++)
                        {
                            IntPtr t = sqlite3_column_text(stmt, i);
                            int n = sqlite3_column_bytes(stmt, i);
                            if (t == IntPtr.Zero) { row[i] = null; continue; }
                            var buf = new byte[n];
                            Marshal.Copy(t, buf, 0, n);
                            row[i] = Encoding.UTF8.GetString(buf);
                        }
                        rows.Add(row);
                    }
                    return rows;
                }
                finally { sqlite3_finalize(stmt); }
            }
            finally { sqlite3_close_v2(db); }
        }
    }

    class GaleProfile
    {
        public long Id;
        public string Name;
        public override string ToString() { return Name; }
    }

    class LocalModRecord
    {
        public string Name, Version, Uuid;
    }

    static class GaleDb
    {
        public static string DefaultPath
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "com.kesomannen.gale", "data.sqlite3"); }
        }

        public static string PathOverride; // used by --selftest
        static string DbPath { get { return PathOverride ?? DefaultPath; } }

        public static bool Exists { get { return File.Exists(DbPath); } }

        public static List<GaleProfile> Profiles()
        {
            return Sqlite.Query(DbPath, "SELECT id, name FROM profiles WHERE game_slug = '" + Config.GameSlug + "' ORDER BY id")
                .Select(r => new GaleProfile { Id = long.Parse(r[0]), Name = r[1] })
                .ToList();
        }

        public static long? ActiveProfileId()
        {
            var rows = Sqlite.Query(DbPath, "SELECT active_profile_id FROM managed_games WHERE slug = '" + Config.GameSlug + "' LIMIT 1");
            long id;
            if (rows.Count > 0 && rows[0][0] != null && long.TryParse(rows[0][0], out id)) return id;
            return null;
        }

        // Local mods in a profile. Gale stores profile.mods as JSON; local mods carry name/version/uuid.
        public static List<LocalModRecord> LocalMods(long profileId)
        {
            var rows = Sqlite.Query(DbPath, "SELECT mods FROM profiles WHERE id = " + profileId);
            var result = new List<LocalModRecord>();
            if (rows.Count == 0 || rows[0][0] == null) return result;
            return ParseLocalMods(rows[0][0]);
        }

        public static List<LocalModRecord> ParseLocalMods(string modsJson)
        {
            var result = new List<LocalModRecord>();
            var list = new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.DeserializeObject(modsJson) as object[];
            if (list == null) return result;
            foreach (object o in list)
            {
                var d = o as Dictionary<string, object>;
                if (d == null || !d.ContainsKey("name") || !d.ContainsKey("uuid")) continue;
                object v;
                result.Add(new LocalModRecord
                {
                    Name = d["name"] == null ? null : d["name"].ToString(),
                    Uuid = d["uuid"] == null ? null : d["uuid"].ToString(),
                    Version = d.TryGetValue("version", out v) && v != null ? v.ToString() : null,
                });
            }
            return result;
        }
    }

    // ------------------------------------------------------------------ Gale executable

    static class Gale
    {
        public static Process[] Running()
        {
            var list = new List<Process>();
            foreach (string n in new[] { "Gale", "gale" })
            {
                try { list.AddRange(Process.GetProcessesByName(n)); } catch { }
            }
            return list.GroupBy(p => p.Id).Select(g => g.First()).ToArray();
        }

        public static bool IsRunning { get { return Running().Length > 0; } }

        public static string FindExe()
        {
            foreach (var p in Running())
            {
                try { string f = p.MainModule.FileName; if (File.Exists(f)) return f; } catch { }
            }

            // Gale registers the gale:// link type on start; its command line holds the exe path.
            foreach (var hive in new[] { Registry.CurrentUser, Registry.LocalMachine })
            {
                try
                {
                    using (var k = hive.OpenSubKey(@"Software\Classes\gale\shell\open\command"))
                    {
                        string cmd = k == null ? null : k.GetValue("") as string;
                        string exe = FirstToken(cmd);
                        if (exe != null && File.Exists(exe)) return exe;
                    }
                }
                catch { }
            }

            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string pf = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var candidates = new[]
            {
                Path.Combine(local, "Gale", "Gale.exe"),
                Path.Combine(local, "Programs", "Gale", "Gale.exe"),
                Path.Combine(pf, "Gale", "Gale.exe"),
                Path.Combine(home, "scoop", "apps", "gale", "current", "Gale.exe"),
            };
            return candidates.FirstOrDefault(File.Exists);
        }

        static string FirstToken(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd)) return null;
            cmd = cmd.Trim();
            if (cmd.StartsWith("\""))
            {
                int end = cmd.IndexOf('"', 1);
                return end > 1 ? cmd.Substring(1, end - 1) : null;
            }
            int sp = cmd.IndexOf(' ');
            return sp > 0 ? cmd.Substring(0, sp) : cmd;
        }

        // Windows command-line quoting (CommandLineToArgvW rules).
        public static string Quote(string arg)
        {
            if (arg.Length > 0 && arg.IndexOfAny(new[] { ' ', '\t', '"' }) < 0) return arg;
            var sb = new StringBuilder("\"");
            int backslashes = 0;
            foreach (char c in arg)
            {
                if (c == '\\') { backslashes++; continue; }
                if (c == '"') { sb.Append('\\', backslashes * 2 + 1); sb.Append('"'); }
                else { sb.Append('\\', backslashes); sb.Append(c); }
                backslashes = 0;
            }
            sb.Append('\\', backslashes * 2);
            sb.Append('"');
            return sb.ToString();
        }

        public static string InstallArgs(string profile, string zip)
        {
            return "--game " + Config.GameSlug + " --profile " + Quote(profile) + " --install " + Quote(zip);
        }
    }

    // ------------------------------------------------------------------ install pipeline

    class Installer
    {
        public Action<string> Status = s => { };
        public Action<int> Progress = p => { };

        public string Run(List<ModEntry> mods, GaleProfile profile, string galeExe)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "MilesModHelper");
            Directory.CreateDirectory(tempDir);

            if (!Gale.IsRunning)
            {
                Status("Opening Gale...");
                Process.Start(new ProcessStartInfo(galeExe) { UseShellExecute = false, WorkingDirectory = Path.GetDirectoryName(galeExe) });
                for (int i = Config.GaleWarmupSeconds; i > 0; i--)
                {
                    Status("Waiting for Gale to load its mod list... " + i + "s");
                    Thread.Sleep(1000);
                }
            }

            var done = new List<string>();
            for (int i = 0; i < mods.Count; i++)
            {
                var m = mods[i];
                int baseProgress = i * 100 / mods.Count;
                int span = 100 / mods.Count;

                Status("Downloading " + m.Name + " v" + m.Version + "...");
                string zip = Path.Combine(tempDir, Safe(m.Id) + "-" + Safe(m.Version) + ".zip");
                Download(m, zip, p => Progress(baseProgress + p * span / 2 / 100));

                var before = new HashSet<string>(GaleDb.LocalMods(profile.Id).Select(x => x.Uuid ?? ""));

                Status("Installing " + m.Name + " into \"" + profile.Name + "\"...");
                Process.Start(new ProcessStartInfo(galeExe, Gale.InstallArgs(profile.Name, zip))
                {
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(galeExe),
                });

                var sw = Stopwatch.StartNew();
                LocalModRecord added = null;
                while (sw.Elapsed.TotalSeconds < Config.InstallTimeoutSeconds)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        added = GaleDb.LocalMods(profile.Id)
                            .FirstOrDefault(x => !before.Contains(x.Uuid ?? "") && NameMatches(x.Name, m));
                    }
                    catch { /* Gale may be mid-write; try again */ }
                    if (added != null) break;
                    Progress(baseProgress + span / 2 + (int)Math.Min(span / 2 - 1, sw.Elapsed.TotalSeconds * span / 2 / 30));
                }

                if (added == null)
                    throw new Exception(m.Name + " didn't show up in \"" + profile.Name + "\" after " + Config.InstallTimeoutSeconds +
                        " seconds.\nCheck Gale for an error message. Mods finished before this one: " + (done.Count == 0 ? "none" : string.Join(", ", done)));

                try { File.Delete(zip); } catch { }
                done.Add(m.Name + " v" + (added.Version ?? m.Version));
                Progress((i + 1) * 100 / mods.Count);
            }

            return string.Join("\n", done);
        }

        static bool NameMatches(string galeName, ModEntry m)
        {
            if (galeName == null) return false;
            return galeName.Equals(m.Id, StringComparison.OrdinalIgnoreCase) || galeName.Equals(m.Name, StringComparison.OrdinalIgnoreCase);
        }

        static string Safe(string s) { return Regex.Replace(s ?? "x", @"[^A-Za-z0-9_.\-]", "_"); }

        public static void CheckDownloadUrl(string url)
        {
            if (url == null || !url.StartsWith(Config.AllowedDownloadPrefix, StringComparison.Ordinal) || url.Contains(".."))
                throw new Exception("Refusing to download from an unexpected address:\n" + url);
        }

        void Download(ModEntry m, string target, Action<int> progress)
        {
            CheckDownloadUrl(m.Download);
            if (string.IsNullOrEmpty(m.Sha256)) throw new Exception(m.Name + " has no checksum in mods.json, so it can't be verified.");

            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.UserAgent] = Config.AppName + "/" + Config.Version;
                var finished = new ManualResetEvent(false);
                Exception error = null;
                wc.DownloadProgressChanged += (s, e) => progress(e.ProgressPercentage);
                wc.DownloadFileCompleted += (s, e) => { error = e.Error; finished.Set(); };
                wc.DownloadFileAsync(new Uri(m.Download), target);
                finished.WaitOne();
                if (error != null) throw new Exception("Download failed for " + m.Name + ": " + error.Message);
            }

            string hash = Sha256File(target);
            if (!hash.Equals(m.Sha256, StringComparison.OrdinalIgnoreCase))
            {
                try { File.Delete(target); } catch { }
                throw new Exception(m.Name + " failed its checksum check (the download is corrupted or was changed). Nothing was installed for it.");
            }
        }

        public static string Sha256File(string path)
        {
            using (var sha = SHA256.Create())
            using (var f = File.OpenRead(path))
                return BitConverter.ToString(sha.ComputeHash(f)).Replace("-", "").ToLowerInvariant();
        }
    }

    // ------------------------------------------------------------------ one-time setup

    static class Setup
    {
        public static string InstallDir
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MilesModHelper"); }
        }

        public static string InstalledExe { get { return Path.Combine(InstallDir, "MilesModHelper.exe"); } }

        public static bool IsRegistered
        {
            get
            {
                using (var k = Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + Config.Scheme + @"\shell\open\command"))
                {
                    string cmd = k == null ? null : k.GetValue("") as string;
                    return cmd != null && cmd.IndexOf(InstalledExe, StringComparison.OrdinalIgnoreCase) >= 0 && File.Exists(InstalledExe);
                }
            }
        }

        // Copies this exe to %LOCALAPPDATA%\MilesModHelper so the link keeps working after the download is deleted.
        public static string InstallSelf()
        {
            string self = Assembly.GetExecutingAssembly().Location;
            Directory.CreateDirectory(InstallDir);
            if (!string.Equals(Path.GetFullPath(self), Path.GetFullPath(InstalledExe), StringComparison.OrdinalIgnoreCase))
                File.Copy(self, InstalledExe, true);
            return InstalledExe;
        }

        // Per-user registration (HKCU): no admin rights needed.
        public static void Register(string exe)
        {
            using (var k = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + Config.Scheme))
            {
                k.SetValue("", "URL:Miles Mods");
                k.SetValue("URL Protocol", "");
                using (var icon = k.CreateSubKey("DefaultIcon")) icon.SetValue("", "\"" + exe + "\",0");
                using (var cmd = k.CreateSubKey(@"shell\open\command")) cmd.SetValue("", "\"" + exe + "\" \"%1\"");
            }
        }

        public static void Unregister()
        {
            try { Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\" + Config.Scheme, false); } catch { }
        }
    }

    // ------------------------------------------------------------------ windows

    class SetupForm : Form
    {
        public SetupForm()
        {
            Text = Config.AppName + " " + Config.Version;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(440, 190);
            Font = new Font("Segoe UI", 9.5f);
            Icon = SystemIcons.Application;

            bool registered = Setup.IsRegistered;
            var label = new Label
            {
                Text = registered
                    ? "The helper is already set up.\n\nGo back to the mod site and click Install on any mod. Click Repair if the buttons stopped working."
                    : "This lets the Install buttons on the mod site put mods straight into your Gale profile.\n\nClick Set up once. You don't need to open this again.",
                Location = new Point(16, 16), Size = new Size(408, 100),
            };
            var setup = new Button { Text = registered ? "Repair" : "Set up", Location = new Point(16, 136), Size = new Size(120, 34) };
            var remove = new Button { Text = "Remove", Location = new Point(146, 136), Size = new Size(100, 34), Enabled = registered };
            var close = new Button { Text = "Close", Location = new Point(324, 136), Size = new Size(100, 34), DialogResult = DialogResult.Cancel };

            setup.Click += (s, e) =>
            {
                try
                {
                    Setup.Register(Setup.InstallSelf());
                    Ui.Info("All set!\n\nGo back to the mod site and click Install. Your browser may ask to open \"Miles Mod Helper\": allow it (tick \"Always allow\" so it won't ask again).");
                    Close();
                }
                catch (Exception ex) { Ui.Error("Setup failed: " + ex.Message); }
            };
            remove.Click += (s, e) =>
            {
                Setup.Unregister();
                Ui.Info("Removed. You can delete " + Setup.InstallDir + " if you want.");
                Close();
            };

            Controls.AddRange(new Control[] { label, setup, remove, close });
            AcceptButton = setup; CancelButton = close;
        }
    }

    class InstallForm : Form
    {
        readonly LinkRequest request;
        readonly Label header = new Label();
        readonly ListBox modList = new ListBox();
        readonly Label profileLabel = new Label();
        readonly ComboBox profileBox = new ComboBox();
        readonly Label status = new Label();
        readonly ProgressBar bar = new ProgressBar();
        readonly Button install = new Button();
        readonly Button cancel = new Button();
        readonly LinkLabel helperNote = new LinkLabel();

        List<ModEntry> mods = new List<ModEntry>();
        string galeExe;
        bool working;

        public InstallForm(LinkRequest request)
        {
            this.request = request;

            Text = Config.AppName;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(460, 360);
            Font = new Font("Segoe UI", 9.5f);
            Icon = SystemIcons.Application;
            TopMost = true; // browsers often leave the launched window behind them

            header.SetBounds(16, 14, 428, 22); header.Text = "Loading mod list...";
            modList.SetBounds(16, 40, 428, 100); modList.SelectionMode = SelectionMode.None;
            profileLabel.SetBounds(16, 152, 428, 20); profileLabel.Text = "Install into this Gale profile:";
            profileBox.SetBounds(16, 174, 428, 26); profileBox.DropDownStyle = ComboBoxStyle.DropDownList;
            status.SetBounds(16, 212, 428, 60); status.Text = "";
            bar.SetBounds(16, 274, 428, 16);
            helperNote.SetBounds(16, 296, 428, 20); helperNote.Visible = false;
            install.SetBounds(232, 318, 104, 32); install.Text = "Install"; install.Enabled = false;
            cancel.SetBounds(340, 318, 104, 32); cancel.Text = "Cancel";

            install.Click += (s, e) => StartInstall();
            cancel.Click += (s, e) => { if (!working) Close(); };
            helperNote.LinkClicked += (s, e) => { try { Process.Start(Config.SiteUrl); } catch { } };
            FormClosing += (s, e) => { if (working) e.Cancel = true; };

            Controls.AddRange(new Control[] { header, modList, profileLabel, profileBox, status, bar, helperNote, install, cancel });
            AcceptButton = install; CancelButton = cancel;

            Shown += (s, e) => BeginInvoke(new Action(LoadIndex));
        }

        void LoadIndex()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ModIndex index = ModIndex.Fetch();
                List<string> missing;
                mods = index.Resolve(request, out missing);
                if (missing.Count > 0)
                {
                    Fail("These mods aren't on the site (anymore): " + string.Join(", ", missing) + ".\nRefresh the site and try again.");
                    return;
                }
                if (mods.Count == 0) { Fail("The site has no mods listed right now."); return; }

                header.Text = mods.Count == 1 ? "Install this mod:" : "Install these " + mods.Count + " mods:";
                foreach (var m in mods) modList.Items.Add(m.Label);

                if (Versions.IsNewer(index.HelperVersion, Config.Version))
                {
                    helperNote.Text = "A newer helper (v" + index.HelperVersion + ") is on the site. Click to get it.";
                    helperNote.Visible = true;
                }

                galeExe = Gale.FindExe();
                if (galeExe == null) { Fail("Couldn't find Gale on this PC. Install Gale (github.com/Kesomannen/gale), open it once, then click Install on the site again."); return; }
                if (!GaleDb.Exists) { Fail("Gale hasn't been set up yet. Open Gale once, pick Lethal Company and make a profile, then try again."); return; }

                var profiles = GaleDb.Profiles();
                if (profiles.Count == 0) { Fail("You have no Lethal Company profiles in Gale. Make one in Gale, then click Install again."); return; }

                foreach (var p in profiles) profileBox.Items.Add(p);
                long? active = GaleDb.ActiveProfileId();
                int idx = active.HasValue ? profiles.FindIndex(p => p.Id == active.Value) : -1;
                profileBox.SelectedIndex = idx >= 0 ? idx : 0;

                status.Text = Gale.IsRunning ? "" : "Gale is closed. The helper will open it for you.";
                install.Enabled = true;
                install.Focus();
            }
            catch (WebException ex) { Fail("Couldn't reach the mod site. Check your internet.\n(" + ex.Message + ")"); }
            catch (Exception ex) { Fail(ex.Message); }
            finally { Cursor = Cursors.Default; }
        }

        void Fail(string message)
        {
            status.Text = message;
            status.ForeColor = Color.Firebrick;
            install.Enabled = false;
            cancel.Text = "Close";
        }

        void StartInstall()
        {
            var profile = profileBox.SelectedItem as GaleProfile;
            if (profile == null) return;

            working = true;
            install.Enabled = false; cancel.Enabled = false; profileBox.Enabled = false;
            status.ForeColor = SystemColors.ControlText;
            TopMost = false;

            var installer = new Installer
            {
                Status = s => BeginInvoke(new Action(() => status.Text = s)),
                Progress = p => BeginInvoke(new Action(() => bar.Value = Math.Max(0, Math.Min(100, p)))),
            };

            var thread = new Thread(() =>
            {
                string result = null; Exception error = null;
                try { result = installer.Run(mods, profile, galeExe); }
                catch (Exception ex) { error = ex; }
                BeginInvoke(new Action(() =>
                {
                    working = false;
                    cancel.Enabled = true; cancel.Text = "Close";
                    if (error != null)
                    {
                        status.ForeColor = Color.Firebrick;
                        status.Text = error.Message;
                    }
                    else
                    {
                        bar.Value = 100;
                        status.ForeColor = Color.DarkGreen;
                        status.Text = "Done! Installed into \"" + profile.Name + "\":\n" + result;
                        cancel.Focus();
                    }
                }));
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }

    // ------------------------------------------------------------------ headless checks (used for testing, not by friends)

    static class SelfTest
    {
        public static int Run(string[] a)
        {
            int failures = 0;
            Action<bool, string> check = (ok, what) => { Console.WriteLine((ok ? "PASS " : "FAIL ") + what); if (!ok) failures++; };

            LinkRequest r; string err;
            check(LinkRequest.TryParse("milesmods://install/AdvancedCompany", out r, out err) && r.Ids.Count == 1 && r.Ids[0] == "AdvancedCompany", "parse single id");
            check(LinkRequest.TryParse("milesmods://install/AdvancedCompany/", out r, out err) && r.Ids.Count == 1, "parse trailing slash");
            check(LinkRequest.TryParse("milesmods:install/A,B,%20C", out r, out err) && r.Ids.SequenceEqual(new[] { "A", "B", "C" }), "parse list + escapes");
            check(LinkRequest.TryParse("MILESMODS://install/all", out r, out err) && r.All, "parse all");
            check(LinkRequest.TryParse("milesmods://install/LC%20Office%20Patcher", out r, out err) && r.Ids[0] == "LC Office Patcher", "parse name with spaces");
            check(!LinkRequest.TryParse("milesmods://install/..%5C..%5Cevil", out r, out err), "reject path chars");
            check(!LinkRequest.TryParse("milesmods://download/https://evil.example/x.zip", out r, out err), "reject other actions");
            check(!LinkRequest.TryParse("gale://install/x", out r, out err), "reject other schemes");

            bool threw = false;
            try { Installer.CheckDownloadUrl("https://evil.example/x.zip"); } catch { threw = true; }
            check(threw, "reject foreign download host");
            threw = false;
            try { Installer.CheckDownloadUrl("https://github.com/SleepyMiles/miles-lc-mods/releases/download/../../other/x.zip"); } catch { threw = true; }
            check(threw, "reject .. in download url");
            threw = false;
            try { Installer.CheckDownloadUrl(Config.AllowedDownloadPrefix + "AdvancedCompany-1.1.34/AdvancedCompany-1.1.34.zip"); } catch { threw = true; }
            check(!threw, "accept repo release url");

            check(Gale.Quote("v.80 Testing Custom Mod") == "\"v.80 Testing Custom Mod\"", "quote spaces");
            check(Gale.Quote("He said \"hi\"") == "\"He said \\\"hi\\\"\"", "quote embedded quotes");
            check(Gale.Quote(@"C:\My Temp\dir\") == "\"C:\\My Temp\\dir\\\\\"", "quote trailing backslash");
            check(Gale.Quote(@"C:\Temp\dir\") == @"C:\Temp\dir\", "unquoted path keeps backslashes");
            check(Gale.Quote("plain") == "plain", "no quotes when not needed");
            check(Gale.InstallArgs("My Profile", @"C:\T\a.zip") == "--game lethal-company --profile \"My Profile\" --install C:\\T\\a.zip", "install args");

            check(Versions.IsNewer("1.0.1", "1.0.0") && !Versions.IsNewer("1.0.0", "1.0.0") && !Versions.IsNewer(null, "1.0.0"), "version compare");

            if (a.Length >= 1)
            {
                string json = File.ReadAllText(a[0]);
                var idx = ModIndex.Parse(json);
                check(idx.Mods.Count > 0, "mods.json parsed: " + idx.Mods.Count + " mods, helper v" + idx.HelperVersion);
                foreach (var m in idx.Mods)
                {
                    threw = false;
                    try { Installer.CheckDownloadUrl(m.Download); } catch { threw = true; }
                    check(!threw && !string.IsNullOrEmpty(m.Sha256) && m.Sha256.Length == 64, "entry ok: " + m.Label);
                }
                List<string> missing;
                LinkRequest.TryParse("milesmods://install/all", out r, out err);
                check(idx.Resolve(r, out missing).Count == idx.Mods.Count, "resolve all");
                LinkRequest.TryParse("milesmods://install/DoesNotExist", out r, out err);
                idx.Resolve(r, out missing);
                check(missing.Count == 1, "resolve reports missing");
            }

            if (a.Length >= 2)
            {
                GaleDb.PathOverride = a[1];
                var profiles = GaleDb.Profiles();
                Console.WriteLine("profiles: " + string.Join(" | ", profiles.Select(p => p.Id + ":" + p.Name)));
                check(profiles.Count > 0, "read profiles from Gale db");
                var active = GaleDb.ActiveProfileId();
                Console.WriteLine("active profile id: " + active);
                check(active.HasValue, "read active profile");
                foreach (var p in profiles)
                {
                    var locals = GaleDb.LocalMods(p.Id);
                    Console.WriteLine("  " + p.Name + " local mods: " + string.Join(", ", locals.Select(l => l.Name + "@" + l.Version + "#" + l.Uuid)));
                }
                if (a.Length >= 4)
                {
                    // a[2] = profile id, a[3] = expected local mod name: used to check completion detection
                    var locals = GaleDb.LocalMods(long.Parse(a[2]));
                    check(locals.Any(l => l.Name == a[3]), "local mod '" + a[3] + "' detected in profile " + a[2]);
                }
            }

            Console.WriteLine(failures == 0 ? "ALL PASSED" : failures + " FAILED");
            return failures == 0 ? 0 : 1;
        }
    }
}
