### v2.0.3 (SleepyMiles edit)
- New option **Can Revive Mangled bodies** (Limits section, also in LethalConfig). Off by default: a body that is cut in half (Barber) or has lost its head (Coil-Head, head burst) shows "Can't revive" at the bunkbed, and the host refuses it too, like the Defibrillator in Usual Scrap. Turn it on to revive every body like before.
- The option comes from the host like the other settings, so everyone in the lobby needs this version.

### v2.0.2
- Fix the trigger/interaction box being able to physically collide and block player movement
- Fix rotation desync on save load
- Shouldn't break any saves, just bugfixes

### v2.0.1
- Working for v73 hopefully...
- Added some more checks for Body ID Mismatches (should solve weird issues with wrong people being revives or something)
- Hopefully fix Money Desync between hosts and clients
- Hopefully fix non-hosts clients unable to revive
- Tested it using 3 local instances via Lan and was able to revive myself and everybody else with no bugs. Seems to be in a "usable" state. 


### v2.0.0
- Removed deprecated dependency
- Switch to StaticNetcodeLib
- Added 5 new options

### v1.1.2
- Fixed issue with v55+

### v1.1.1
- Fixed PercentOfQuota

### v1.1.0
- Added config options to change the revive cost & method

### v1.0.2
- updated deps

### v1.0.1
- netcode fixes

### v1.0.0
- Release