# 1.1.35
### New
- **Weather menu: weathers from other mods.** With WeatherRegistry (and WeatherTweaks' combined / progressing weathers), every modded weather now gets its own scrap amount and scrap value row under the game's six, like modded moons do. They're in the menu right away (Wesley's Weathers, WeatherTweaks' mixes...); weathers that another mod only adds once a game starts show up after you've started one. Their starting values are the mods' own multipliers. Remove the mod and its weathers leave the menu (your numbers stay saved and come back if you add it again).
- **Mixed weathers (WeatherTweaks) follow your rows.** Combined weathers ("Stormy + Rainy") and weathers that change during the day ("None > Foggy") are listed after the other modded ones and start on auto: while their box is off they're worked out from your own rows of the weathers they're made of, the way WeatherTweaks does it (A + B = 75% of each added up, A > B = the average of what it changes to). So changing Rainy also changes "Stormy + Rainy". The grey number shows what will be used. Tick the box to set your own; Reset goes back to auto.
- A weather that changes during the day keeps its own row for the whole day (before, the game's weather it changed to was used).
- **"Only these (WeatherRegistry's own off)"** in the weather menu (on by default): turns off WeatherRegistry's own scrap multipliers (scrap and Apparatus), so only AdvancedCompany's weather values are used. Before, both were applied on top of each other. Turn it off to stack them like before.
- **Spectator screen: player names** under each player's box. The name stays still while the box bounces when they talk.

### Fixes
- LAN (no Steam): AdvancedCompany wrote "Player" over everyone's name when someone joined, breaking LCDirectLAN's custom usernames. It now keeps the game's / LCDirectLAN's names.
- Weather multipliers: Foggy, Flooded, Stormy and Eclipsed used Rainy's on/off switch instead of their own.
- Weathers the menu doesn't know no longer get Clear's multipliers.
- LAN: a client could not join (or rejoin) a save after a day was played with a modded weather (Forsaken, "Rainy > Eclipsed"...): the join crashed while sending the weather, the game kept retrying ("An item with the same key has already been added") and the client got "An error occurred!".

# 1.1.34
### Fixes
- Fixed not being able to land on the next moon / getting disconnected when rejoining a save ("SoundManager initialization failed"). LethalLevelLoader destroyed AdvancedCompany's voice mixer when a moon loaded; AdvancedCompany's mixers are now protected from it. As a safety net, voice pitch and lobby setup fall back to the game's own mixer instead of breaking.
- Fixed the Bulletproof Vest not taking damage from turrets and shotguns (and an invalid-IL crash on boot when used together with MoreShipUpgrades).
- Fixed "buy" in the portable terminal saying teleporters, inverse teleporters and furniture were "already bought" when they were not.
- Fixed the stamina perks when other mods (e.g. LategameUpgrades) also change stamina: Sprint endurance slowed stamina regen instead of reducing drain, and Bodybuilder did nothing. The perks are now applied on top of the final stamina change, so they work together with other mods:
  - Sprint endurance: only reduces stamina drain (sprinting and walking while slowed).
  - Bodybuilder: reduces the extra drain from carried weight while sprinting.
  - Stamina regen: increases all stamina regen (standing still and walking).
- Heavy runner now also reduces the weight slowdown on acceleration, not only on top speed (also works together with LategameUpgrades' Traction Boots).
- Scanner (scan distance) perk is now applied by temporarily scaling the scan node range (V.81).

### Portable terminal
- Hold W/S or the arrow keys to keep scrolling through menus. In the store, hold A/D or Left/Right to change amounts quickly.

### Mod compatibility
- **Elad's HUD:** battery time left now includes the Battery Pack upgrade (TZP gas time is not affected). The stamina drain/regen per second now shows the real rate, including AdvancedCompany perks and other mods.
- **UsualScrap:** the Productivity Auto-Injector now lasts 1 minute (it only lasted 5 seconds).
- **UsualScrap:** the Handlamp no longer fills the screen with bright fog when held. Its fog glow, brightness and shadows can be adjusted in the config (`UsualScrap Handlamp` section, also in LethalConfig) and change live.
- **QualityCompany:** fixed errors when picking up or throwing items from extra inventory slots or the utility slot.
- **GeneralImprovements:** lightning warnings now work for clients, not only the host. Items you keep through a teleporter no longer attract lightning inside the facility.
- **Frailty:**
  - Fixed the injury chance: at full health every matching hit always gave an injury. Frailty's chance setting is now the real chance per hit (optional in the config: scale it with how badly you are hurt).
  - Injuries are now rolled after the damage is applied, using the damage you actually took. A hit that another mod blocks completely no longer injures you.
  - Steam valves no longer blind you instantly: blindness is rolled once per second near a burst valve instead of every frame.
  - Leg injuries no longer give extra stamina regen. Their extra sprint drain is reduced by Sprint endurance and Bodybuilder, and shows in Elad's HUD stamina /sec.
- **LethalLevelLoader:** no longer destroys AdvancedCompany's voice mixers (see the first fix).

# 1.1.33
- Update for v81 compatible.

# 1.1.32
- Fixed compatibility with some weird mods adding null components to GameObjects.

# 1.1.31
- Adjusted for v62 
- Fixed a couple of AI bugs regarding the Butler and other enemy types

# 1.1.30
- Changed the way the MoreCompany stub dll is accessed to hopefully prevent errors.
- Fixed the dancing avatar in cosmetics tab.

# 1.1.29
- Fixed errors with the cruiser and animation errors with terminal
- Fixed an error which prevented the default preset and skip preset option in ModPack.cfg to not work correctly.
- Moved the client presets to AppData so reinstalling a modpack won't delete your client configurations.
- Added the option to remove the copyright text at the top right entirely.
- First compatibility patch for v55/v56

# 1.1.28


# 1.1.27
- Fixed a bug introduced with 1.1.26 which prevented the client-side settings from being saved correctly.

# 1.1.26
- Removed AdvancedCompany logo from main menu and loading screen (mod pack creators can still replace the logo and users can deactivate this in the settings)
- Only the text on the top right "Powered by AdvancedCompany" is kept.
- Removed all anti-tamper code

# 1.1.25
- Changed README.md

# 1.1.24
- Fixed compatibility with LLL. ALL is no longer needed.

# 1.1.23
- Added array patching to butler

# 1.1.22
- Maybe this time fixed a bug with unreleased moons? :0

# 1.1.21
- Fixed a bug with unreleased moons :'3

# 1.1.20
- Fixed butler breaking with more than 4 players

# 1.1.19
- Updated for v50 (Still might be buggy)

# 1.1.18
- Replaced music due to licensing issues. You should now be free to stream AdvancedCompany :)

# 1.1.17
- Added new configuration options regarding the copyright notice and logo replacement. You can now change the main menu logo to become the original game one which will lead to the copyright notice to show up. You can also make the copyright notice become simplified (text in corner of the screen)

# 1.1.16
- Added new API methods to get the GrabbableObject objects equipped in slots. (Requested by a modder)

# 1.1.15
- Maybe fixed lag produced by portable terminal for some.
- Some more refactoring done (and more will come soon).

# 1.1.14
- Fixes

# 1.1.13
- Refactored code.

# 1.1.12
- Hopefully fixed a bug with skip presets

# 1.1.11
- Removed LLL fork from AdvancedCompany and added it to its own mod so you can decide to use different level loaders.
- Fixed a couple of small bugs (mostly NullReferenceExceptions)
- Refactored some code
- AdvancedLevelLoader will now correctly show the correct prices.

# 1.1.10
- Hopefully fixed an error with network prefabs
- Refactored code (no real effect on the mod)

# 1.1.9
- Fixed a bug with loading levels introduced in 1.1.2
- Fixed a bug in LethalLevelLoader breaking the switch command on the terminal.

# 1.1.8
- Changed some paths in LethalLevelLoader to actually find the plugin folder correctly.

# 1.1.7
- Renamed LethalLevelLoader back again and changed some stuff to make it load correctly. :D

# 1.1.6
- Renamed LethalLevelLoader to AdvancedLevelLoader

# 1.1.5
- Added a modified and compatible fork of LethalLevelLoader to AdvancedCompany in compliance with MIT license given at commit 85a42881a50fb3525444e0d56a1074355fdb1d7b
  - Moon prices are now directly received from AdvancedCompany, making AdvancedCompany a hard dependency for the LLL fork
  - Dependency for the fork of LLL of LethalLib was removed

# 1.1.4
- Changed the prevention code for watermark removal detection
- Removed LethalLevelLoader from assembly patching as legally requested by IAmBatby specifically probably making changes to moons added by LethalLevelLoader incompatible due to the greedy nature of LLL.
  - Dont contact me about this. I have no way of changing this. IAmBatby is the only one who can revoke their legal request. AC might add custom moon loading later tho to circumvent the requirement of LLL to do that.

# 1.1.3
- Changed how lights work for cosmetics making them visible in third person but not ego perspective.
- Fixed an error which resulted in the new "Individual XP" setting preventing the start of the game.
- Added license to readme

# 1.1.2
- Added new lobby settings to customize your lobby. Giving you the ability when save progress is off to carry over XP when not meeting quota (Reset XP -> false) and all players having the same XP as the host (Individual progress -> false). Also gives the ability to adjust starting ship XP.
- Fixed an error with the savegames not loading correctly when other mods change the item names.
- Fixed some bugs with the controller item not letting you die from death zones and making the controller visible again when climbing ladders.
- Added a compability feature to deactivate most features of AC by removing all inventory patches making it compatible with other mods changing the inventory. You won't get any support from me when activating this option if you don't provide logs showing an error happening when this option is not activated.
- Hopefully fixed tactical helmet lights not turning off when battery runs out for good this time.
- Fixed a bug which resulted in the weight of the bulletproof vest not being removed from player when its breaking
- Changed culling mask of local players cosmetics lights to not affect any renderers.
- Fixed the compatibility typo.
- Changed the Mostly Vanilla preset to not include the controller and remove the hotbar changes as well. Please make sure your errors happen with other presets as well and only provide logs not produced with mostly vanilla when reporting bugs or your bug reports will be ignored for the same reason stated in the hotbar setting.

# 1.1.1
- Further reduced logs and changed a lot of message logs in stable code to debug level.
- Added music volume slider to settings
- Fixed controller item not removing post processing effect after quitting
- Added Y offset slider for hotbar
- Removed BetterEmotes compability mode requirement.
- Tactical helmet will correctly turn of its lights when battery runs out

# 1.1.0
- Introduced first API calls (subject to change):
  - GameObject[] AdvancedCompany.Lib.Cosmetics.GetSpawnedCosmetics(GamenetCodeStuff.PlayerControllerB)
  - void AdvancedCompany.Lib.Cosmetics.LoadCosmeticsFromPrefab(GameObject)
  - void AdvancedCompany.Lib.Cosmetics.LoadCosmeticsFromBundle(AssetBundle)
  - GameObject[] AdvancedCompany.Lib.Equipment.GetSpawnedEquipment(AssetBundle)
  - PostProcessingInstance AdvancedCompany.Lib.HDRP.AddPostProcessing<T>(InjectionPoint, PostProcessingFlags, string)
  - void AdvancedCompany.Lib.Mod.RegisterRequiredMod(string, string)
  - void AdvancedCompany.Lib.Player.AddAnimation(AnimationClip)
  - void AdvancedCompany.Lib.Player.SetAnimationOverride(GamenetCodeStuff.PlayerControllerB, string, string, bool)
  - void AdvancedCompany.Lib.Player.RemoveAnimationOverride(GamenetCodeStuff.PlayerControllerB, string, bool)
  - void AdvancedCompany.Lib.Mod.RegisterRequiredMod(BaseUnityPlugin)
  - void AdvancedCompany.Lib.Flavour.SetLogo(Texture2D)
  - void AdvancedCompany.Lib.Sync.AddSyncHandler<T>()
  - void AdvancedCompany.Lib.Sync.SyncHandler.ItemSynchronization.AddItemType<T>()
- Reworked the whole lobby netcode
  - You will only be able to see AdvancedCompany lobbies with the same all-clients mods as you (if they use RegisterRequiredMod).
  - Vanilla players joining will see a meaningful error message.
  - Mismatched all-clients mod registered with the new API will be communicated with the player.
  - Handshake is done during MainMenu resulting in less sync problems for player data.
  - Lobby size will now be communicated before scene initialization, resulting in only instantiating the max amount of players which can join.
  - Server configuration is shared before scene switching resulting in less config sync problems.
  - AudioMixer will only get replaced for player voices and its output is linked to the games audio mixer, resulting in less audio problems.
  - AudioMixer will expand depending on the max players in the lobby. (4, 8, 12, 16, 20, 24, 28, 32)
  - Added new experimental "Join after start" option which sets players joining on moon as spectator.
  - Added custom synchronization code to prevent the errors of past mods.
  - Name syncing is now properly working.
  - Radar targets should not desync
  - Custom synchronizers ensure more synchronization when joining late (state of animations like storage and ship lights, red locust bees hives, state of locked doors etc.)
  - Connecting players will now be put in a queue until the client before them joined fully. Reducing (even vanilla) errors in multiple clients joining at the same time.
- Added ModPack.cfg
  - You can now add custom flavour logos to AC for your modpack
  - You can skip the new lobby config by this configuration.
- Added lobby settings and presets:
  - Lobby setup is decativateable by the ModPack.cfg when a valid default preset is given
  - Selected server presets are added to the savegame, so the preset will stay selected.
  - You can configure moons and their item and enemy spawns 
  - You can configure scrap items
  - You can configure buyable items
  - You can configure weather multipliers
  - You can configure game rules regarding AdvancedCompany
  - You can configure day length
  - You can configure starting credits and deadline days
  - You can configure the max amount of players allowed
  - You can configure enemy powers (there will be more configuration for enemies later)
  - You can configure moon prices
  - You can configure perks
  - You can now configure unlockable objects (ship upgrades & decorations)
    - **Note:** As I had to read from other modders that AC is "f\*\*\*ing" with the terminal and got reminded by their users that my shitty mod doesnt has nearly as many downloads as the mods of the modders saying so, I decided to **NOT** change the terminal store. It will still show the wrong prices for ship upgrades and still show deactivated ship upgrades because of it.
- Added client settings and presets:
  - Hotbar settings added
  - Vision enhancer is now configurable in brightness
  - Including some compability settings to hide equipment or deactivate added music.
  - Animation compability mode can manually be activated. Its activated by default for MoreEmotes and BetterEmotes as of now.
  - Cosmetics can be changed in main menu and while in-game
- Added the portable terminal:
  - Perks moved to this terminal
  - Includes a store
  - Includes an encyclopedia to help understanding the new items added.
  - Extend deadline also moved here
  - Fallback handler for commands will try to find out what you wanted to buy when typing stuff like "ligning" for lightning rod
- Added cursed items (SPOILERS):
  - Light Shoes will make you fast but all enemies will hunt you. Better start running!
  - Bunny ears will make you only be able to move by jumping
  - PietSmiet (German Youtubers) controller resembling PietSmiet logo will start a game of DOOM inside Lethal Company.
  - Cursed items will lose their curse upon reaching the ship. They are unequippable when taken.
- Added headset
- Added helmet lamp
- Added tactical helmet
- Added stamina regen perk
- Renamed Night Vision to Vision Enhancer
- Added custom death screen compatible for up to 32 players
- Added new post processing stack
- Added MoreCompany cosmetics compability with a MoreCompany stub DLL
- Changed how the flippers fix broken in-game water to prevent water interaction breaking due to the fix.
- If MoreCompany is present (due to a dependency of a cosmetics mod or by choice) it will get deactivated at runtime.
- If MoreCompany is present the included cosmetics of MoreCompany will become available.
- When dying in a lobby without Save progress, the inventory slots will correctly reset now
- Fortified jump height transpiler
- Added more exception handling and fail safes to the mod patcher
- Scrap multipliers will now be forcefully reset in RoundManager.SpawnScrapInLevel which should prevent ever-increasing multipliers from happening.
- AdvancedCompany will patch other mods to make them compatible with lobby configuration.
- Added a check for assemblies to exclude them from ldfld patching. Just add [assembly: AssemblyMetadata("AdvancedCompanyIgnore", "True")] to your assembly.
- Changed the way XP are given to make it compatible with QuotaRollover
- Head mount is removed when no equippable head items are activated
- Apparatus value now correctly follows value multiplier.
- Added more error checks for items added in a wrong way to the game
- Added debug command to terminal to help debugging
- Removed all terminal commands and nearly all previously added Terminal Code.
- Added a custom loading and saving routine which will add additional information to save files to restore the state of a save even when items were added by mods or removed by removing mods.
- Added more meaningful NetworkConfig mismatch error information, so it's easier to pinpoint errors.
- All XP progress has been reset due to a new savefile. You can edit your savefile now as it is in JSON format.
- Added default presets
- Added compability mode for BetterEmotes/MoreEmotes
- Limited respec. You can only respec player on ship and ship at company building.
- Extended the used font asset for all available unicode chars from 0000 to 2600
- Added keybinds with LethalInputUtils
- Travel discount should now also affect modded moons.
- Made equipment and cosmetics compatible with mirror mod and 3rd person of TooManyEmotes
- Added a hint when you gained XP.
- You can now deactivate certain equipment slots by deactivating all items going into a certain slot.
- Now also deactivated LateCompany and ShipLobby as they dont add any functionality and are more likely to break stuff.
- Removed legacy file configuration. Only in-game and JSON files from now on :)
- Fixed an error which only showed the simple death messages with Coroner
- Fixed an error with damage reduction perk
- Fixed a bug which prevented items making sounds in your inventory.
- Fixed an error regarding the save suits option
- Fixed a bug which resulted in the activated flashlight not being shown active any longer when dropping an inactive one.
- Fixed a bug which resulted in the level screen not showing
- Fixed [playerNum] appearing in chat
- Fixed radar targets having the wrong name
- Fixed disconnected players appearing on endscreen.
- Fixed a rare error which occured when picking up an item while being in equipment slots resulting in desyncs of the inventory.
- Fixed an error with other mods changing the despawn scrap mechanics by adding a transpiler.
- Fixed a bug which prevented Travel discount from working
- Fixed climbing speed not working
- Fixed fall damage not working correctly (Zone with no damage is now correctly adjusted)
- Fixed a bug with rehosting after dying breaking the scan
- Fixed a bug with inventory slots not showing up immediately after purchase.
- Fixed a bug with relayed message overflow
- Fixed a bug which prevented the updated deadline to be shown when extending the deadline.
- Fixed a bug which prevented the flashlight from shutting down correctly when the batteries were used up.
- Fixed the jump stamina perk mistakenly making jumps cost more stamina
- Fixed a bug with the vest not correctly reducing damage
- Fixed masked players not wearing cosmetics

# 1.1.0 Beta 9 Hotfix 17 (1.0.149)

- Fixed a rare error which occured when picking up an item while being in equipment slots resulting in desyncs of the inventory.
- Fixed an error with other mods changing the despawn scrap mechanics by adding a transpiler.
- Added new compability options (you can activate animation compability mode manually now and hide equipment, hide cosmetics and deactivate the music clientside)
- Korean player names should now function correctly again. (In fact all UTF8 characters should work)
- Fixed an error which resulted in savegame being corrupted by the bulletproof vest. Yay!
- Renamed the infamous hall of shame to hall of extraordinary gentlemen and made it debug level. So if you feel overly offended by this joke, just deactivate debug level logs.
- Fixed a bug in post processing stack resulting in a null error when beamUpPlayer was called without, well, beaming a player like the method suggests. \*shrug\*
- Added an encyclopedia with in-game explanation of all added items.
- Changed most application on portable terminal to return to help screen when no output is printed (just exiting store, closing perks etc.)
- Items which are deactivated will now automatically have a highest sale percentage of 0 resulting in them not having discounts any longer shown in other mods.
- Added predefined presets for lobby configuration. Default, Alternative (with 4 starting inventory slots) and Mostly Vanilla.
- You can now configure unlockable objects (ship upgrades & decorations)
  - **Note:** As I had to read from other modders that AC is "f\*\*\*ing" with the terminal and got reminded by their users that my shitty mod doesnt has nearly as many downloads as the mods of the modders saying so, I decided to **NOT** change the terminal store. It will still show the wrong prices for ship upgrades and still show deactivated ship upgrades because of it.
- Added GZIP compression to handshake so larger configs can be transferred within steams 512K limit. Compression ratio for configs are about 1:6. So in theory you can now transfer configs up to 3MB.

# 1.1.0 Beta 9 Hotfix 16 (1.0.148)

- Fixed a bug which resulted in cosmetics being missing after dying.
- Changed how the flippers fix broken in-game water to prevent water interaction breaking due to the fix.
- Fixed portable terminal being closed for other clients when player closes their own.
- Fixed deadline not being applied correctly
- Fixed a visual bug in the portable terminal store showing non discounted prices
- Added the ability to add decimal numbers into perk config

# 1.1.0 Beta 9 Hotfix 15 (1.0.147)

- Added more exception handling and fail safes to the mod patcher
- Finished the new HDRP PostProcessing library.

# 1.1.0 Beta 9 Hotfix 14 (1.0.146)

- Unity decided to reset my prefabs once again. So this update is to address that :)
- Added some code for post processing to library (unfinished yet)

# 1.1.0 Beta 9 Hotfix 14 (1.0.145)

- Did a small oopsie with new player sync to clients
- Added reinforced steel to the queue to make it more stable :'D

# 1.1.0 Beta 9 Hotfix 13 (1.0.144)

- Maybe fixed a bug with username
- Solidified the lobby code to the max. :3
- Some smaller bug fixes like clamping inventory slots.

# 1.1.0 Beta 9 Hotfix 12 (1.0.143)

- Fixed a bug which resulted in the defaulting of usernames

# 1.1.0 Beta 9 Hotfix 11 (1.0.142)

- Added a null check to library GetSpawnedEquipment
- Fixed an error with debug mode
- Fixed an error in transpiler introduced in 141

# 1.1.0 Beta 9 Hotfix 10 (1.0.141)

- Apparatus values now dont use the base 0.4 multiplier of the game.
- Fixed a bug with the preview characters pose
- Apparatus values should now work correctly with LethalRadiation and FacilityMeltdown
- Scrap multipliers will now be forcefully reset in RoundManager.SpawnScrapInLevel which should prevent ever-increasing multipliers from happening.
- Error resulting in all inventory slots being available for clients is fixed
- Added names to all player objects which will prevent missing null reference checks in mods like MonitorLabels from breaking them
- Added a check for assemblies to exclude them from ldfld patching. Just add [assembly: AssemblyMetadata("AdvancedCompanyIgnore", "True")] to your assembly.
- Fixed deadline not resetting to set value after being fired. (Needs more testing)

# 1.1.0 Beta 9 Hotfix 9 (1.0.140)

- Apparatus value now correctly follows value multiplier.
- One vary hoppy cursed item lets you jump in water now. And even some other things :3

# 1.1.0 Beta 9 Hotfix 8 (1.0.139)

- Attempt to fix animation errors with broken mods like MoreEmotes
- First step of adding way more sync to the lobby and players. This might lead to new errors. Needs testing. Names etc. should now be synced at all times.
- Added a couple of fixes for late joining on moon.
- Fixed an error which resulted in the mod not applying its patches on other mods when BepInEx Manager object was hidden.

# 1.1.0 Beta 9 Hotfix 7 (1.0.138)

- Added stamina regen perk

# 1.1.0 Beta 9 Hotfix 6 (1.0.137)

- Fixed a bug which resulted in other mods being patched multiple times
- Fixed another source of potentially lobby desyncs

# 1.1.0 Beta 9 Hotfix 5 (1.0.136)

- Fixed a bug which occured when deactivating enemies in enemies tab.

# 1.1.0 Beta 9 Hotfix 5 (1.0.135)

- Fortified jump height transpiler

# 1.1.0 Beta 9 Hotfix 4 (1.0.133)

- Fixed a bug which prevented entering float values into probability fields.
- Added new configs for headset, helmet lamp, tactical helmet and flippers.
- Fixed a bug which prevented Travel discount from working
- Added new game config tab with new options:
  - Starting credits
  - Global scrap value multiplier
  - Global scrap amount multiplier
  - Global enemy max power multiplier
  - Activate portable terminal (you wont have access to perks when deactivated)
  - Deadline length
- Changed the way XP are given to make it compatible with QuotaRollover

# 1.1.0 Beta 9 Hotfix 3 (1.0.132)

- Fixed a bug which resulted in perk configuration to be not synchronized
- Fixed a bug when clients havent started the game yet and encounter an enemy not present in their local dict.
- Fixed a bug which resulted in the default preset showing wrong values when building the dict.
- Fixed the mod patcher to actually work again.

# 1.1.0 Beta 9 Hotfix 2 (1.0.131)

- Changed README.md

# 1.1.0 Beta 9 Hotfix 2 (1.0.130)

- Ensured the mod patcher can only run once even if it runs on an error.

# 1.1.0 Beta 9 Hotfix 1 (1.0.129)

- Fixed headset/tactical helmet problems with hearing other players.

# 1.1.0 Beta 9 (1.0.128)

- Added a secret item. Get ready to jump!
- Deferred the patching of mods to the first frame. Should help fixing some bugs but might introduce other config related ones?
- Manually loading stub MoreCompany.dll to prevent MoreCompany related errors.

# 1.1.0 Beta 8 Hotfix 5 (1.0.127)

- Attempt to fix missing field errors with MoreCompany stub DLL.

# 1.1.0 Beta 8 Hotfix 4 (1.0.126)

- Unity decided to not save my prefabs. Had to reapply prices etc. to my items.

# 1.1.0 Beta 8 Hotfix 3 (1.0.125)

- Changed some things in the patcher to make it work with more mods correctly.

# 1.1.0 Beta 8 Hotfix 2 (1.0.124)

- Updated the MoreCompany stub dll to work with other mods better (TooManyEmotes for example)

# 1.1.0 Beta 8 Hotfix 1 (1.0.123)

- Fixed a bug which resulted in weather multiplier being applied twice
- Fixed a bug which prevented using "." as decimal point in some locales.

# 1.1.0 Beta 8 (1.0.122)

- Added patcher for other mods (decorating ldflds)
- Fixed a couple of bugs regarding moon/item/scrap scanner
- Fixed a bug which allowed the light shoes to be unequipped when the curse wasnt lifted

# 1.1.0 Beta 7 Hotifx 2 (1.0.121)

- Fixed a small bug which resulted in server config not being synced with clients.

# 1.1.0 Beta 7 Hotifx 1 (1.0.120)

- Fixed an error with late-game syncing and total value in level value
- Added new secret item to list of items enabling the feet slot
- Head mount is removed when no equippable head items are activated
- Fixed an error which happens on first boot
- Added more error checks for items added in a wrong way to the game
- Fixed a bug which added enemy spawns etc. in moon settings with a start value of 100 after scan.

# 1.1.0 Beta 7 (1.0.119)

- Nerfed Tactical Helmet slightly
- Fixed climbing speed not working
- Fixed fall damage not working correctly (Zone with no damage is now correctly adjusted)
- Fixed a bug with rehosting after dying breaking the scan
- Might have fixed a cause for desync with perks
- Added debug command to terminal to help debugging
- Fixed client presets being synched by mod managers. Old client presets wont work any longer.
- Changed death screen box size and effect when talking
- Added a secret loot item (First cursed item, look out for it on Titan, Rend and Dine)
- Added a lot of new config settings, including moon loot tables, enemy spawn rates etc.
- Added scrap config
- Reworked the configuration system to be more dynamic.
- Nearly all changes to the lobby settings are now applied via transpilers
- Added MoreCompany stub DLL to provide a way to load MoreCompany cosmetics without having MoreCompany installed
- Fixed an error with hip cosmetics being attached to the wrong bone in-game

# 1.1.0 Beta 6 Hotfix 2 (1.0.118)

- When pressing return on an element in the shop it will now switch to the buy button for quicker navigation.
- When going back from the store the screen is cleared correctly now.

# 1.1.0 Beta 6 Hotfix 1 (1.0.117)

- Instead of inserting, adding to the terminal keywords again to prevent mods not fortified for index changes to work again.

# 1.1.0 Beta 6 (1.0.116)

- New item: Tactical helmet
- Added Store and the ability to buy items to the portable terminal.
- Removed all terminal commands and nearly all previously added Terminal Code.
- Reworked how item weights, prices, item activations, moon prices etc. are applied to use transpilers.
- Added more meaningful NetworkConfig mismatch error information, so it's easier to pinpoint errors.
- Added a custom loading and saving routine which will add additional information to save files to restore the state of a save even when items were added by mods or removed by removing mods.
- Fixed a bug which occured when deleting a previously selected preset.
- Fixed a bug which resulted in weird shadows with the helmet lamp while looking down
- Fixed a couple of small bugs reported to me over the last few days

# 1.1.0 Beta 5 Hotfix 5 (1.0.115)

- **Nerfed the helmet lamp due to complains :3.**
- Fixed a bug which prevent client settings from being applied
- Selected client settings preset is now saved between sessions
- Selected server settings are now correctly saved in the game savefile
- All XP progress has been reset due to a new savefile. You can edit your savefile now as it is in JSON format.
- Fixed a bug which prevented items making sounds in your inventory.

# 1.1.0 Beta 5 Hotfix 4 (1.0.114)

- Added default presets

# 1.1.0 Beta 5 Hotfix 3 (1.0.113)

- Fixed a bug with removed the head equipment slot when vision enhancer was disabled but helmet lamp or headset not
- Added compability mode for BetterEmotes/MoreEmotes
- Added new library methods for other modders to add animation overrides and sync them.
- Fixed a bug with the headset not working correctly
- Fixed a bug which resulted in the headset still using battery when dropping it directly from an equipment slot.
- Removed legacy file configuration. Only in-game and JSON files from now on :)
- Added new configuration values for the hotbar. (Border thickness, min and max spacing)
- Fixed a typo on portable terminal

# 1.1.0 Beta 5 Hotfix 2 (1.0.112)

- Fixed a bug with relayed message overflow
- Fixed a bug with inventory slots not showing up immediately after purchase.

# 1.1.0 Beta 5 Hotfix 1 (1.0.111)

- Fixed a bug which prevented AdvancedCompany from saving due to the new backup system.

# 1.1.0 Beta 5 (1.0.110)

- Added the new portable terminal. Press X to open it. You will only be able to level your perks on the new portable terminal from now on.
- Changed the behaviour of cosmetics by spawning them on Layer 23 (The layer Mirror Mod uses to show own cosmetics in mirror) and changed SpectateCamera accordingly (added 23 to culling mask), so mods relying on spectator camera for third person views will work with own cosmetics out of the box. All other mods have to adapt.
- Hopefully fixed a rounding bug with the item weights
- Hopefully fixed a rare desync bug with client ids in lobbys (resulting in wrong perks being shown)
- Fixed a bug which resulted in players wearing their equipment after dying.
- Fixed a bug which prevented the [playerNum] fix to be applied.
- Fixed a bug which prevented the updated deadline to be shown when extending the deadline.
- Limited respec. You can only respec player on ship and ship at company building.
- Fixed a bug which prevented the flashlight from shutting down correctly when the batteries were used up.
- Extended the used font asset for all available unicode chars from 0000 to 2600
- Adjusted the helmet lamp color
- Added keybinds with LethalInputUtils
- Added a safety feature to prevent AdvancedCompany savefile corruption. (Backup before save and if backup is also corrupted, deleting save files)

# 1.1.0 Beta 4 Hotfix 4 (1.0.110)

- Added length checks to the new store to prevent mods from exceeding 26 character item names and prices over 5 digits

# 1.1.0 Beta 4 Hotfix 3 (1.0.109)

- Fixed an issue with bulletproof vest file config
- Fixed an issue with free moon file config
- Fixed an error with the new time setting
- Fixed an error which resulted in the cosmetics file to be written to the root of the hard drive
- Fixed an error which prevented equipment receiving their unequipped event when switching items in slots
- Fixed an issue with console not being able to show more than 10 levels. Will show them now but break layout instead of not working at all.

# 1.1.0 Beta 4 Hotfix 2 (1.0.108) 

- Fixed a bug which caused Jeb calling you when directly dropping the Headset from an equipment slot

# 1.1.0 Beta 4 Hotfix 1 (1.0.107) 

- Fixed new presets having wrong moon prices
- Fixed incompability of the headset with LECore.
- Fixed some values of moons not saving correctly.

# 1.1.0 Beta 4 (1.0.106) 

- Added new item: Headset
- Fixed a bug with the fall damage perk not applying correctly
- Fixed a bug with the damage reduction perk reducing fall damage
- Fixed a bug which caused the helmet lamp to be applied multiple times and not being removed when dying
- Fixed a bug which prevented buying items on the terminal (especially when the full name was entered)
- Fixed a bug which resulted in wrong items being delivered when other mods added items.
- Fixed a couple of small bugs
- Added the possibility to add own moon configs by name. (Enabling configuration of modded moons)
- Added the possibility to add own item configs by name. (Enabling configuration of items from other mods)
- Added scrap multiplier values for moons.
- Reworked a lot of the lobby code to stabilize it. Heavily tested this locally and joining as spectator should work very good now. Hopefully.
- Moved the cosmetics config out of the config folder to prevent all players having the same cosmetics when loading a profile via mod manager.
- Added new configuration value to define the length of day.
- Hopefully fixed masked players not wearing cosmetics :)
- Travel discount should now also affect modded moons.
- Added brightness slider for night vision

# 1.1.0 Beta 3 (1.0.105) 

- Added new item: Helmet lamp
- Hopefully fixed all configuration errors. Lets cross fingers.
- Added cosmetics to masked enemies. Also applies the selected suit to it.
- Fortified the netcode even further and added granular logging to find bugs.
- Fixed a bug which prevented clients from disconnecting when the lobby closes.
- Fixed small bugs here and there.
- Made equipment compatible with mirror mod and 3rd person of TooManyEmotes
- Hopefully fixed the bug with the vest not correctly reducing damge for good
- Fixed the jump stamina perk mistakenly making jumps cost more stamina :D

# 1.1.0 Beta 2 Hotfix 2 (1.0.104) 

- Hopefully fixed a bug which caused the bulletproof vest to heal you.

# 1.1.0 Beta 2 Hotfix "[Redacted] was too tired and forgot to add the correct DLL" (1.0.103) 

- Title says it all.

# 1.1.0 Beta 2 Hotfix 1 (1.0.102)

- Fixed a bug which showed cosmetics on own players when Mirror mod wasnt present

# 1.1.0 Beta 2 (1.0.101)

- Hardened the handshake process to not fail when a part of the handshake failes
- Added compability with third person camera of TooManyEmotes and Mirror mod.
- Fixed a bug with the hotbar not appearing when entering a game.
- Fixed some more little sources of exceptions to ensure smooth gaming.
- Added a hint when you gained XP.
- You can now deactivate certain equipment slots by deactivating all items going into a certain slot.
- Fixed a bug which resulted in items being placeable in equipment slots.

# 1.1.0 Beta 2 (1.0.100)

- Fixed a bug with items showing up twice in the new store
- Fixed a bug with some configs getting multiplied by 100.
- Changed the save location of client presets. (File configs will still be shared but you can create new client presets which wont get shared)
- Changed positioning of flavour text when logo is replaced.
- Removed compability with ReservedSlots
- Fixed incompability with InsanityRemastered
- Change positioning of equipment slots resulting in the possibility to level up to 10 inventory slots if set by the lobby.
- Maybe fixed a bug from showing the main menu after cancelling lobby setup with certain mods
- Fixed a bug which resulted in the wrong players being removed from the connected players list in certain circumstances resulting in weird behaviour.
- Now also deactivated LateCompany and ShipLobby as they dont add any functionality and are more likely to break stuff.

# 1.1.0 Beta 1 (1.0.99)

- Introduced the first API calls:
  - AdvancedCompany.Lib.Mod.RegisterRequiredMod()
  - AdvancedCompany.Lib.Flavour.SetLogo()
  - AdvancedCompany.Lib.Sync.AddSyncHandler<T>()
  - AdvancedCompany.Lib.Sync.SyncHandler.ItemSynchronization.AddItemType<T>()
- Reworked the whole lobby netcode
  - You will only be able to see AdvancedCompany lobbies with the same all-clients mods as you (if they use RegisterRequiredMod).
  - Vanilla players joining will see a meaningful error message.
  - Mismatched all-clients mod registered with the new API will be communicated with the player.
  - Handshake is done during MainMenu resulting in less sync problems for player data.
  - Lobby size will now be communicated before scene initialization, resulting in only instantiating the max amount of players which can join.
  - Server configuration is shared before scene switching resulting in less config sync problems.
  - AudioMixer will only get replaced for player voices and its output is linked to the games audio mixer, resulting in less audio problems.
  - AudioMixer will expand depending on the max players in the lobby. (4, 8, 12, 16, 20, 24, 28, 32)
  - Added new experimental "Join after start" option.
  - Added custom synchronization code to prevent the errors of past mods.
- Added a new store to make the terminal more robust
- Added a fallback if item was not found making it possible to buy lightning rod with input like "lignting"
- Change the terminal text field to behave more nicely with its scrollarea.
- Fixed an error which only showed the simple death messages with Coroner
- Fixed an error with damage reduction perk
- Added lobby settings and presets 
- Added client settings and presets
- Added MoreCompany cosmetics compability
- Cosmetics can be changed while in-game
- Added custom death screen compatible for up to 32 players
- If MoreCompany is present (due to a dependency of a cosmetics mod or by choice) it will get deactivated at runtime.
- If MoreCompany is present the included cosmetics of MoreCompany will become available.
- Fixed an error regarding the save suits option
- When dying in a lobby without Save progress, the inventory slots will correctly reset now
- Fixed a bug which resulted in the activated flashlight not being shown active any longer when dropping an inactive one.
- Fixed a bug which resulted in the level screen not showing
- Fixed [playerNum] appearing in chat
- Fixed radar targets having the wrong name
- Fixed disconnected players appearing on endscreen.
- Selected server presets are added to the savegame, so the preset will stay selected.

# 1.0.10

- Removed debug hotkeys H and J for rescaling the Hotbar
- Fixed the screen observer. The hotbar should now automatically adjust to the screen correctly when the window is resized.

# 1.0.9

- Fixed the missile launcher not working as intended. Dont read further if you want to find it out yourself:
  - If being chased by dogs or giants, they will stop chasing and going to the fireworks
  - If being eaten by a giant the giant will drop you
  - Giants will ignore players and wont pick up players for 5s
  - Dogs will ignore players for around 7s
  - Still makes beautiful fireworks

# 1.0.8

- Fixed the hotbar bug
- Fixed the flipped steam avatars
- Added a new more fitting design for energy indicators

# 1.0.7

- Fixed a bug which resulted in some results on the endscreen not being removed correctly.
- Reworked the inventory slots to make it more consistent and work even better with ReservedSlots
- Added a check if a keybind for flashlight is set and if so removing the flashlight keybind from ReservedSlots
- Player avatars are now loaded during the run asynchronously which should prevent avatars missing in end screen and any hangs.
- Added missing foggy weather multipliers
- Added the ability to save your progress in your profile folder. Allowing you to create multiple profiles with different progress.
- Added the ability to change the hotbar alpha and scale
- Added more settings for when save progress is off to make the progression faster:
  - Starting XP
  - XP multiplier

# 1.0.6

- Fixed a bug which prevented users from inputting into the terminal
- Hopefully fixed a bug with the new endscreen freezing the game
- Added compability with ReservedSlots
- Added the ability to deactivate clothing slots by deactivating all items which can be worn in the config

# 1.0.5

- Fixed a bug with the map which might have led to unexpected behaviour.
- Added cause of death to the end screen.
- Added Coroner for cause of death integration. (If you have Coroner installed, the cause of death is read from that mod instead)

# 1.0.4

- Added a custom end screen to enable lobbies of up to 32 players to be displayed.
- Fixed a bug which prevented from spectating all players when playing with up to 32 players.
- Fixed a bug which prevented the correct names of players to be displayed in lobbys bigger than 4.
- Fixed a configuration bug with the Server/General.cfg. It should now show up correctly. Happy bundling mod packs out there! :)
- Fixed a bug with the synchronization of flashlights. They should now no longer desync.
- Fixed an error in one of the transpilers which could have led to unexpected behaviour when playing with more than 4 players. I am pretty sure by now that 32 players should be stable.

# 1.0.3

- Added configuration to moon prices and weather scrap modifiers, making them deactivateable. Deactivating moon prices will result in the travel discount perk to be disabled too.
- Added configuration to let the server keep track of all player XP and reset it after not meeting the quota. Players wont lose their permanent XP but cant use them in lobbys with this enabled. (SaveProgress in Server/General.cfg)
- Hopefully fixed 32 player lobbys not working for steam lobbies. I hope for feedback on this one :)
- Fixed a configuration error which resulted in default values not being set correctly. All affected configuration values will get overwritten after launching the game for the first time after this update. Affected perks were Critical strike chance and Loot Saver.

# 1.0.2

- Fixed a small bug which was introduced by 1.0.1 preventing the night vision and the jump height perk from working correctly.

# 1.0.1

- Slightly changed the missile launcher to aim more for the middle of the screen. Please give me feedback if it feels better now.
- Improved lobby handling and therefore increasing performance for lobbies smaller than 32 players greatly.
- Fixed wrong configuration paths so you can adjust the configs in ThunderStore correctly now.
- Added the ability to deactivate the bigger lobby patches. Please note: All connectings clients need to synchronize this setting before connecting to the server. I am pretty sure other bigger lobby mods will likely result in incompabilities when playing with more than 4 players but you can try it.