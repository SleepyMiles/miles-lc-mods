#!/usr/bin/env python3
"""Build the package index that SleepyMiles Mod Manager reads, from docs/api/v1/versions.json.

SleepyMiles Mod Manager (a fork of Gale) treats this site as a second mod source next to Thunderstore.
It expects the same files Gale reads from Thunderstore/Hexium:

  api/v1/package-listing-index.gz      gzip JSON: list of chunk URLs
  api/v1/packages-<hash>.json.gz       gzip JSON: list of PackageListing (Thunderstore's format)

The chunk is named after a hash of its contents, so a new list always has a new address and GitHub
Pages' CDN (which caches files for up to 10 minutes) can never hand out an old copy of it. The app
asks for the small index with a cache-busting query, so it always sees the newest chunk name.
  api/v1/categories.json               {"results": []}
  api/v1/markdown/<name>/<version>/readme.json, changelog.json   {"markdown": "..."}

Every published version is kept in versions.json, so profile codes that pin an older version still resolve
(the zip for each version stays on its GitHub release).
"""
import datetime
import glob
import gzip
import hashlib
import json
import os
import uuid

REPO = "SleepyMiles/miles-lc-mods"
SITE = "https://sleepymiles.github.io/miles-lc-mods/"
OWNER = "SleepyMiles"
ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
DOCS = os.path.join(ROOT, "docs")
API = os.path.join(DOCS, "api", "v1")
HISTORY = os.path.join(API, "versions.json")
RECENT_CHUNKS = os.path.join(API, "chunks.json")
KEEP_CHUNKS = 5  # older chunk files are deleted; a few are kept for apps mid-download

# Stable IDs: the same mod always gets the same UUID, and they can't collide with Thunderstore's random ones.
NAMESPACE = uuid.UUID("5f0c8c7e-3d1a-4b9e-9a51-6d1c2a7f4e10")


def package_uuid(name):
    return str(uuid.uuid5(NAMESPACE, f"package/{OWNER}-{name}"))


def version_uuid(name, version):
    return str(uuid.uuid5(NAMESPACE, f"version/{OWNER}-{name}-{version}"))


def semver_key(v):
    core, _, pre = v.partition("-")
    nums = [int(x) for x in core.split(".")]
    # releases sort above pre-releases of the same version
    return (nums, 1 if not pre else 0, pre)


def load_history():
    if os.path.exists(HISTORY):
        with open(HISTORY, encoding="utf-8") as f:
            return json.load(f)
    return {"mods": {}}


def save_history(history):
    os.makedirs(API, exist_ok=True)
    with open(HISTORY, "w", encoding="utf-8", newline="\n") as f:
        json.dump(history, f, indent=2, ensure_ascii=False, sort_keys=True)
        f.write("\n")


def record_version(history, name, version, description, dependencies, website, size, date, readme=None, changelog=None):
    """Add or replace one version of a mod in the history (called when a release is published)."""
    mods = history.setdefault("mods", {})
    entry = mods.setdefault(name, {"date_created": date, "versions": {}})
    entry["versions"][version] = {
        "date_created": date,
        "description": description,
        "dependencies": dependencies,
        "website_url": website or "",
        "file_size": size,
    }
    # Markdown is stored per version so older versions keep their own README/CHANGELOG.
    out = os.path.join(API, "markdown", name, version)
    os.makedirs(out, exist_ok=True)
    for kind, text in (("readme", readme), ("changelog", changelog)):
        with open(os.path.join(out, kind + ".json"), "w", encoding="utf-8", newline="\n") as f:
            json.dump({"markdown": text}, f, ensure_ascii=False)


def listing(name, entry):
    versions = sorted(entry["versions"].items(), key=lambda kv: semver_key(kv[0]), reverse=True)
    latest_date = max(v["date_created"] for _, v in versions)
    return {
        "full_name": f"{OWNER}-{name}",
        "categories": [],
        "date_created": entry["date_created"],
        "date_updated": latest_date,
        "donation_link": None,
        "has_nsfw_content": False,
        "is_deprecated": bool(entry.get("deprecated", False)),
        "is_pinned": False,
        "package_url": f"{SITE}#mod-{name}",
        "rating_score": 0,
        "uuid4": package_uuid(name),
        "versions": [
            {
                "full_name": f"{OWNER}-{name}-{ver}",
                "date_created": v["date_created"],
                "dependencies": v["dependencies"],
                "suggestions": [],
                "description": v["description"],
                "downloads": 0,
                "file_size": v["file_size"],
                "is_active": True,
                "uuid4": version_uuid(name, ver),
                "website_url": v["website_url"],
            }
            for ver, v in versions
        ],
    }


def gz_bytes(data):
    import io
    raw = json.dumps(data, ensure_ascii=False, separators=(",", ":")).encode("utf-8")
    buf = io.BytesIO()
    with gzip.GzipFile(fileobj=buf, mode="wb", mtime=0) as gz:
        gz.write(raw)
    return buf.getvalue()


def write_gz_json(path, data):
    raw = json.dumps(data, ensure_ascii=False, separators=(",", ":")).encode("utf-8")
    # mtime=0 keeps the file byte-identical when nothing changed, so commits stay clean.
    with open(path, "wb") as f:
        with gzip.GzipFile(fileobj=f, mode="wb", mtime=0) as gz:
            gz.write(raw)


def build(history=None):
    history = history or load_history()
    os.makedirs(API, exist_ok=True)
    packages = [listing(name, entry) for name, entry in sorted(history["mods"].items(), key=lambda kv: kv[0].lower())]
    chunk = gz_bytes(packages)
    chunk_name = f"packages-{hashlib.sha256(chunk).hexdigest()[:12]}.json.gz"
    with open(os.path.join(API, chunk_name), "wb") as f:
        f.write(chunk)
    # Older app versions (1.0.0-1.0.6) still read this fixed name.
    with open(os.path.join(API, "packages-0.json.gz"), "wb") as f:
        f.write(chunk)
    write_gz_json(os.path.join(API, "package-listing-index.gz"), [SITE + "api/v1/" + chunk_name])

    recent = []
    if os.path.exists(RECENT_CHUNKS):
        with open(RECENT_CHUNKS, encoding="utf-8") as f:
            recent = json.load(f)
    recent = [chunk_name] + [c for c in recent if c != chunk_name]
    recent = recent[:KEEP_CHUNKS]
    with open(RECENT_CHUNKS, "w", encoding="utf-8", newline="\n") as f:
        json.dump(recent, f, indent=2)
        f.write("\n")
    for old in glob.glob(os.path.join(API, "packages-*.json.gz")):
        base = os.path.basename(old)
        if base != "packages-0.json.gz" and base not in recent:
            os.remove(old)
    with open(os.path.join(API, "categories.json"), "w", encoding="utf-8", newline="\n") as f:
        json.dump({"results": []}, f)
        f.write("\n")
    return packages


def now_iso():
    return datetime.datetime.now(datetime.timezone.utc).replace(microsecond=0).isoformat().replace("+00:00", "Z")


if __name__ == "__main__":
    pkgs = build()
    print(f"Wrote app index with {len(pkgs)} mods")
