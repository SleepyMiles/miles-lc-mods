#!/usr/bin/env python3
"""Add or update a mod (or the helper) in docs/mods.json from a release asset.

Used by .github/workflows/publish.yml when a GitHub Release is published, and can be run by hand:

    python3 tools/update_index.py --tag AdvancedCompany-1.1.35 --file path/to/AdvancedCompany-1.1.35.zip
    python3 tools/update_index.py --tag helper-v1.0.1 --file helper/bin/MilesModHelper.exe

For a mod zip it reads manifest.json (the same file Gale reads), checks it has everything Gale needs,
copies icon.png / README.md / CHANGELOG.md into docs/mods/<name>/, and records the download URL,
size and SHA-256 that the helper verifies before installing.
"""
import argparse
import datetime
import hashlib
import json
import os
import re
import sys
import urllib.parse
import zipfile

REPO = "SleepyMiles/miles-lc-mods"
DOWNLOAD_BASE = f"https://github.com/{REPO}/releases/download/"
ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
DOCS = os.path.join(ROOT, "docs")
INDEX = os.path.join(DOCS, "mods.json")
SEMVER = re.compile(r"^\d+\.\d+\.\d+(?:-[0-9A-Za-z.-]+)?(?:\+[0-9A-Za-z.-]+)?$")
DEP = re.compile(r"^[^-\s]+-[^-\s]+-\d+\.\d+\.\d+$")


def fail(msg):
    print(f"ERROR: {msg}", file=sys.stderr)
    sys.exit(1)


def sha256(path):
    h = hashlib.sha256()
    with open(path, "rb") as f:
        for chunk in iter(lambda: f.read(1 << 20), b""):
            h.update(chunk)
    return h.hexdigest()


def load_index():
    if os.path.exists(INDEX):
        with open(INDEX, encoding="utf-8") as f:
            return json.load(f)
    return {"schema": 1, "helper": None, "mods": []}


def save_index(index):
    index["mods"].sort(key=lambda m: m["name"].lower())
    with open(INDEX, "w", encoding="utf-8", newline="\n") as f:
        json.dump(index, f, indent=2, ensure_ascii=False)
        f.write("\n")


def download_url(tag, filename):
    # GitHub turns spaces in asset names into dots.
    asset = filename.replace(" ", ".")
    return DOWNLOAD_BASE + urllib.parse.quote(tag) + "/" + urllib.parse.quote(asset)


def read_manifest(z):
    try:
        raw = z.read("manifest.json")
    except KeyError:
        fail("manifest.json must be at the top level of the zip (Gale reads it from there).")
    text = raw.decode("utf-8-sig")
    try:
        m = json.loads(text)
    except json.JSONDecodeError as e:
        fail(f"manifest.json is not valid JSON: {e}")

    # These are the fields Gale's PackageManifest requires; a zip missing one fails to import in Gale.
    for key, kind in (("name", str), ("description", str), ("version_number", str), ("dependencies", list), ("website_url", str)):
        if not isinstance(m.get(key), kind):
            fail(f"manifest.json needs \"{key}\" ({kind.__name__}); Gale won't import the zip without it.")
    if not re.fullmatch(r"[A-Za-z0-9_.\- ]{1,100}", m["name"]):
        fail(f"manifest name \"{m['name']}\" may only use letters, numbers, spaces, _ . -")
    if not SEMVER.match(m["version_number"]):
        fail(f"version_number \"{m['version_number']}\" must look like 1.2.3")
    for d in m["dependencies"]:
        if not isinstance(d, str) or not DEP.match(d.strip()):
            fail(f"dependency \"{d}\" must look like Author-Name-1.2.3")
    return m


def add_mod(index, tag, path, notes):
    filename = os.path.basename(path)
    with zipfile.ZipFile(path) as z:
        m = read_manifest(z)
        name = m["name"]
        out = os.path.join(DOCS, "mods", name)
        os.makedirs(out, exist_ok=True)
        files = {n.lower(): n for n in z.namelist()}
        for wanted, target in (("icon.png", "icon.png"), ("readme.md", "README.md"), ("changelog.md", "CHANGELOG.md")):
            src = files.get(wanted)
            dst = os.path.join(out, target)
            if src:
                with open(dst, "wb") as f:
                    f.write(z.read(src))
            elif os.path.exists(dst):
                os.remove(dst)

    old = next((x for x in index["mods"] if x["id"].lower() == name.lower()), None)
    entry = {
        "id": name,
        "name": name.replace("_", " ").strip(),
        "author": m.get("author") or m.get("author_name") or "SleepyMiles",
        "version": m["version_number"],
        "description": m["description"],
        "dependencies": [d.strip() for d in m["dependencies"]],
        "website": m["website_url"],
        "download": download_url(tag, filename),
        "size": os.path.getsize(path),
        "sha256": sha256(path),
        "updated": datetime.date.today().isoformat(),
        "icon": f"mods/{name}/icon.png" if os.path.exists(os.path.join(DOCS, "mods", name, "icon.png")) else None,
        "readme": f"mods/{name}/README.md" if os.path.exists(os.path.join(DOCS, "mods", name, "README.md")) else None,
        "changelog": f"mods/{name}/CHANGELOG.md" if os.path.exists(os.path.join(DOCS, "mods", name, "CHANGELOG.md")) else None,
        "notes": notes if notes is not None else (old or {}).get("notes"),
    }
    if old:
        index["mods"].remove(old)
    index["mods"].append(entry)
    print(f"{'Updated' if old else 'Added'} {name} {entry['version']} ({entry['size']} bytes, sha256 {entry['sha256'][:12]}...)")


def add_helper(index, tag, path):
    v = re.sub(r"^helper-v", "", tag)
    if not SEMVER.match(v):
        fail("helper tags must look like helper-v1.0.0")
    index["helper"] = {
        "version": v,
        "download": download_url(tag, os.path.basename(path)),
        "size": os.path.getsize(path),
        "sha256": sha256(path),
        "updated": datetime.date.today().isoformat(),
    }
    print(f"Helper set to {v}")


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--tag", required=True, help="release tag the file is attached to")
    ap.add_argument("--file", required=True, help="the .zip (mod) or .exe (helper) attached to that release")
    ap.add_argument("--notes", help="optional short note shown on the mod's card")
    a = ap.parse_args()

    index = load_index()
    if a.tag.startswith("helper-v"):
        add_helper(index, a.tag, a.file)
    elif a.file.lower().endswith(".zip"):
        add_mod(index, a.tag, a.file, a.notes)
    else:
        fail("expected a .zip for a mod release, or a helper-v* tag for the helper")
    save_index(index)


if __name__ == "__main__":
    main()
