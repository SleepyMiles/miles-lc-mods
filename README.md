# SleepyMiles mods

A small Thunderstore-style site for Lethal Company mods that can't go on Thunderstore. Friends click **Install** and the mod lands in their Gale profile, the same way Gale's own "Import local mod" does it.

Site: https://sleepymiles.github.io/miles-lc-mods/

## For friends

**Recommended:** download [SleepyMiles Mod Manager](https://github.com/SleepyMiles/SleepyMiles-Mod-Manager/releases/latest), then import Miles's profile code. It gets mods from Thunderstore and this site in one app.

**Staying on Gale?** Use the helper:

1. Download **MilesModHelper.exe** from the site and run it once. Click **Set up**.
2. Click **Install** on any mod (or **Install all**).
3. Pick your Gale profile in the window that pops up and click **Install**.

To get updates, click **Install all** again. The helper checks the profile and only installs mods that are missing or out of date, like Gale's update button. (Gale's profile codes never include manually installed mods, which is why this step exists.)

## Publishing a mod or an update (Miles)

1. Build the mod zip the way you do for Gale (`manifest.json`, `icon.png`, `README.md` at the top of the zip).
2. On GitHub: **Releases → Draft a new release**.
   - Tag: `<ModName>-<version>`, for example `AdvancedCompany-1.1.35`.
   - Attach the one `.zip`.
   - Click **Publish release**.
3. The **Publish to mod site** action checks the manifest, adds it to `docs/mods.json`, and the site updates within a minute or two.

Keep the manifest `name` the same between versions. Gale matches local mods by that name, so a renamed mod installs next to the old one instead of replacing it.

If the action fails, open the **Actions** tab: the error says which manifest field Gale would reject.

## How it works

- `docs/` is the website (GitHub Pages, `main` branch, `/docs` folder). `docs/mods.json` is the mod list.
- `helper/` is the Windows helper. It registers the `milesmods://` link type for the current user (no admin), reads the Gale profile list from `%APPDATA%\com.kesomannen.gale\data.sqlite3` (read-only), downloads the zip from this repo's releases, checks its SHA-256, then runs `Gale.exe --game lethal-company --profile "<picked>" --install "<zip>"` and waits until the mod appears in that profile.
- The helper only installs mods listed in `mods.json` and only downloads from this repo's releases, so a link on some other site can't make it install anything else.
- `tools/update_index.py` is what the action runs. You can also run it by hand: `python3 tools/update_index.py --tag AdvancedCompany-1.1.35 --file AdvancedCompany-1.1.35.zip`.

### Updating the helper

Bump `Config.Version` and the `AssemblyVersion` lines in `helper/MilesModHelper.cs`, build with `helper/build.cmd` (Windows) or `helper/build.sh` (Mono), and publish a release tagged `helper-v<version>` with `MilesModHelper.exe` attached. Friends with an older helper see a "newer helper" link in its window.

### Testing the helper without Windows

`mono helper/bin/MilesModHelper.exe --selftest docs/mods.json path/to/data.sqlite3` checks link parsing, download allowlisting, argument quoting, and reading profiles from a Gale database (needs a `MilesModHelper.exe.config` mapping `winsqlite3` to `libsqlite3.so.0`).
