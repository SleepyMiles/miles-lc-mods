@echo off
rem Builds MilesModHelper.exe on Windows with the C# compiler that ships with .NET Framework 4.x.
cd /d "%~dp0"
if not exist bin mkdir bin
"%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /nologo /optimize+ /target:winexe /platform:x64 ^
  /win32icon:icon.ico ^
  /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Web.Extensions.dll ^
  /out:bin\MilesModHelper.exe MilesModHelper.cs
