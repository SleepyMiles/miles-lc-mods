#!/usr/bin/env bash
# Builds MilesModHelper.exe with Mono's C# compiler (Linux/macOS).
# Output targets .NET Framework 4.x, which is built into Windows 10 and 11.
set -euo pipefail
cd "$(dirname "$0")"
mkdir -p bin
mcs -nologo -optimize+ -target:winexe -platform:x64 -sdk:4.5 \
    -win32icon:icon.ico \
    -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:System.Web.Extensions.dll \
    -out:bin/MilesModHelper.exe MilesModHelper.cs
sha256sum bin/MilesModHelper.exe
