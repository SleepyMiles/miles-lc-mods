## 2.3.6
- Private v81 fork by SleepyMiles, for playing with friends.
- Fixed the metal detectors: v81 changed the game's kill function, and the detector still called the old one. Its zap threw an error every frame instead of killing the player carrying metal. It works again.
- The LC Office Patcher is now part of this mod (in `patchers`). Remove the separate "LC Office Patcher" mod.
- Fixed the package: a second copy of LCOffice.dll and the patcher sat in a misspelled `BeplnEx` folder, so the DLL was loaded twice and BepInEx had to skip one.
- The plugin inside the DLL now says 2.3.6 (it still said 2.3.4), so it always wins over Piggy's 2.3.4 if both are installed.
- Added the missing DungeonGenerationPlus dependency (LC Office uses it; without it the interior couldn't load), and LC Office now loads after LethalLevelLoader and DungeonGenerationPlus.

## 2.3.5
- v80 fork by SleepyMiles
- Fix an error that make this mod cannot run on v80

## 2.3.4
- Recompiled netcode for v73
- Updated Dependencies
- Replaced LLL dependency with updated fork
- Added SmartEnemyPathfinding dependency. This is not a requirement for the mod but it does allow masked to use the office elevator similar to how they use the mineshaft elevator.
- I changed when the elevator offmeshlink gets enabled to right before when the doors open and moved the exit position as per Zaggy's request to improve enemy's ability to use the elevator on the 3rd floor.

Note: In my testing masks using the patch mod tend to get in a loop of riding the elevator to a floor, walk over and touch an ai node, then go to another floor and repeat. There isn't much I can do about this. If that behavior bothers you you can disable SmartEnemyPathfinding and everything will work just fine.

- Removed one of the vending machine drinks apon request but replaced it with a new one.
- Vending machine now checks when it's spawned if custom scraps are disabled and unhooks buttons from the component.

## 2.3.3
- Implemented method for future PathFindingLib update.
- The pillar wall cubes in some tiles have had their navmesh carvers removed and replaced with modifier volumes that bake the holes directly into the navmesh instead of carving during runtime. In previous versions, these carvers weren't set to carve, which caused enemies to sometimes get stuck on them.
- Fixed incorrect name displayed in the top right for one of the soda flavors.
- Masks can now use the ladder on the elevator. Standing on top is no longer safe.
- Two links have been added to allow enemies to jump off the elevator's roof.
- Elevator mesh has been changed from a probuilder mesh to a normal asset.
- I changed one of the walls in the elevator model slightly. tbh I doubt anyone will even be able to notice the change, but I'm including this for the sake of keeping complete change logs.

## 2.3.2
- Fixed blocker/connector objects on the right door of the new watercooler tile being set to the wrong objects.
- Fixed radar sprites for the water cooler tile.

## 2.3.1
- Increased the double doors' animated trigger hitbox size to make it easier for some enemies to open those doors.
- Fixed Office Apparatus having incorrect item properties. (Caused it not to save)
- Office Apparatus now has a turned-off prefab. (Fixes issue of spawned apparatus spawning in a docked state)

## 2.3.0
- Added PathfindingLib dependency.
- Enemies that implement PathfindingLib's elevator logic will now be able to use the office elevator.
- Elevator's Offmeshlink is now set to the correct navigation layer.
- LC Office scraps are now set to be inside the facility by default. (No more lightning indoors)
- The vending machine has been restocked and given new textures.
- The notice board on the main entrance floor has been reworked. Blocked floors still display the blocked texture. Now unblocked floors display their colored texture. Indicator lights have been added that show the elevator's position on the board.

<details>
<summary>Added 2 new Tiles (Click for spoilers)</summary>

Office Tiles:
- Water Cooler Hall

Warehouse Tiles:
- V-Fan Room
</details>

- Radar sprites for several staircases have been modified to make their location more apparent from different elevation levels.
- New Painting.
- Stevy's light has been dimmed and his door has closed slightly more.
- Moved Camera lights closer to the cameras themselves.
- Fixed some uvs & removed couch.
- Raised ceiling a bit on curved hall tile. (Fix small clipping issue with roof spike traps)
- Breakroom and confrence table have had their models converted from probuilder shapes to asset models.

## 2.2.3
- Reverted DLL to v2.2.1. I had forgotten code implementing an unreleased Pathfindinglib update when I intended on just posting a recompile.

## 2.2.2

- Added Ship Radar Sprites to all tiles. 
- Pro Builder Meshes have been made into real models. (Cleans up a lot of hte read/write access errors on dungeon generation but not all of them yet.) (This did mess up some UVs but I fixed the obnoxious ones. If some things look slightly different that's probably why.)
- Puddle Materials have been given transparency & removed alpha clipping.
- Couch (I'm not lazy. You're lazy)
- Metal Detector in the Control Room now has a warning sign next to it.
- Added various scan nodes to things in the interior. Mostly for flavor, some for gameplay.
- Recomp for v72. (Nothing was broken as far as I am aware but figure'd I'd do it anyways) (No code changes)

## 2.2.1

- Added null check to the terminalEvent string in the terminal patch. This fixes null refrence errors when buying modded items that leave the field null.

## 2.2.0

<details>
<summary>Added 4 new Tiles (Click for spoilers)</summary>

Office Tiles:
- Curved Hall
- Private Office

Warehouse Tiles:
- U-Shape Room
- Fan Room
</details>

- Removed needless mesh colliders on the elevator indicator graphic on the main floor.
- The indicator graphic on the main floor now correctly displays what floor the elevator is on / going to. And displays a blocker when no tiles have generated on a floor.
- Unclipped more floor bottles in the side hall tile.
- Adjusted bounds of the three way hall tile to be a bit smaller.
- Added "(Client Sided)" to the description of some config options to make it clear that you can change those values without causing desync. (This applies to the music volume and pitch along with camera display configs)
- Seperated Control Room (Apparatus room) camera to a different config. This means you can turn off the start room cameras and keep the control room active if you'd like that.
- Disabled rotation on the stair tile. (when spawned at some rotations it could lead to areas that weren't able to reconnect to other paths)
- Adjusted scrap spawn positions in the server room.
- Added more spike trap spawn positions to hopefully prevent spike trap stacking in some rooms.
- Elevator lights now update after the display changes off of "Proccessing" to show you the floor it plans to land on more immediantly.
- Restored Elevator Pannel Font to how it was before the update.
- Dungeon Bounds have been widened to hopefully allow the appy room more space to generate.
- Resized override tile bounds for a lot of old tiles with a focus of making the virtical bounds match the tile height better.
- Adjusted spawn curve for Stair tile to disallow it as far near the end of paths.
- Added breakroom to maze areas for more tile diversity.
- Adjusted weights of several tiles.
- Replaced music tracks under default config from their normal tracks to the refiltered variants. (Some people mentioned the music being slightly different after 2.0.0)

## 2.1.0

- Added [DunGenPlus](https://thunderstore.io/c/lethal-company/p/Alice/DungeonGenerationPlus/) as a dependancy.
- Generation bounds have been set up to confine the interior into a smaller horizontal space.
- Enabled branch simulation to encourage branches to connect paths more frequently.
- The dungeon now by default generates 2 main paths at a shorter length than the old generation. This should result in more tiles on different elevator floors.
- A config has been added to allow you to change the number of main branches between One, Two, and Three. The default value is Two. Setting this to one reverts the length and branch count back to similar values to the 2.0.0 update. Two and Three use the same length and branch count values. I don't really recomend playing with Three because it generates pretty large dungeons at that value. But it is at least fun to mess around with.
- Added null check to TerminalNode in the terminal patch.
- Stair tile side halls now make carpet sounds instead of metal sounds when walked on.
- Moved several props in the stair floor up so they no longer clip into the floor.
- Fixed items being scannable through the walls of the monitor room on the bottom floor.
- Moved wood pallet in side hall to no longer clip inside the wall.
- Fixed description on camera FPS config.
- Added the storage/railing room in the factory area to the maze tileset at a smaller weight.
- Decreased weight of stair tiles.

## 2.0.0

Note: I ([JacobG5](https://thunderstore.io/c/lethal-company/p/JacobG5/)) starting this update am maintaining this mod with Piggy's permission. If you run into any problems please reach out to JacobG5 in the Lethal Company Modding Discord.

Also, I usually keep track of my changelogs as I work on updates but for this one, I didn't do that. If I missed anything in the changelog then I apologize.

- Rewrote most of the mod's code.
- Added 14 new tiles. (I will not list them in order to keep them a surprise for players to discover)
- Replaced the 3rd-floor part of the start tile...
- Added a new sub tileset.
- Resized all tiles to fit in a set grid size. (this allows branch paths to merge into other paths creating loops in interior generation)
- Significantly reduced filesize (Nearly 25% of the old size)
- Removed Halt
- Removed Shrimp (FR this time)
- Removed Apparatus machines from large room tiles in favor of a new apparatus tile inspired by the [Stanley Parable](https://store.steampowered.com/app/1703340/The_Stanley_Parable_Ultra_Deluxe/).
- The "Emergency Power System" config has been removed in the code rewrite to now be always on. This means that when the facility apparatus is removed, the elevator will fall to the bottom floor and no longer be operational. That is until you take an apparatus (works with other modded apparatuses) and plug it into the elevator to power it back online. You can then pull the apparatus back out of the elevator once you're finished using it and the elevator will fall back to the bottom floor.
- Added & removed many scrap and hazard spawns in old tiles. (this will lead to fewer items merged inside of other objects along with fewer hazards piled on top of each other.)
- LC Office now correctly registers with LLL in a way that allows LLL config to be usable with LC Office.
- LC Office Scrap items that were removed in a previous version have been re-added (These scraps are made with assets ripped from The Upturned). They now register through LLL. If you don't like them they can be disabled in the config.

<details>
<summary>Item Spoilers</summary>

All of these items were made by Piggy and are just being re-enabled in this update. Assets used are from The Upturned (Zeeker's Previous Game)

- Laptop
- Screw Driver
- Wrench
- Toolbox
- Coin (I wonder what you could use this for...)
- The Pink Apparatus formerly socketed inside the elevator is now an item found within the interior.
</details>

- Migrated old project files into an updated NomNom LC Patcher. I believe all the quirks caused by this migration should now be fixed but if there are any missing sounds or assets please report them in the discord thread! 
- Part of the code rewrite was to use UnityNetcodePatcher for the networking to replace LethalNetworkingAPI
- Removed [LethalNetworkAPI](https://thunderstore.io/c/lethal-company/p/xilophor/LethalNetworkAPI/) & [LethalLib](https://thunderstore.io/c/lethal-company/p/Evaisa/LethalLib/) as dependancies.
- Added [JLL](https://thunderstore.io/c/lethal-company/p/JacobG5/JLL/) as a dependancy.
- Merged several similar empty hallway tiles into one tile.
- Removed Diagonal tiles because it messed up room generation.
- Replaced the old stair tile with a new stair tile that is large enough to connect the different elevator floors together.
- Removed the TVs on the main floor desk that displayed the camera feeds. The camera feeds have been moved into the easter egg room on the bottom floor. 
- A camera has been added to the 3rd and 2nd floor of the start tile to fill the other two monitors in the bottom floor room.
- Scrap can now spawn in some areas of the start tile.
- The bathroom tile now has scrap spawns in all of the stalls to make them worth searching.
- The "ElevatorMusicPitchDown" config now plays the pitched-down version of all 3 of the music tracks instead of just "bossa lullaby"
- The terminal in the easter egg room of the bottom floor now displays different texts in relation to what moon the interior is generated on.
- Removed all the legacy patches aside from the terminal patch (responsible for the commands to move the elevator) and a patch to the apparatus for some of the mechanics added in this update. (Removal of the other patches should fix some of the edge case mod incompatibilities that were occurring before. especially when they are no longer required anyways)
- Rebalanced room weights for (in my own opinion) a more interesting generation. There are more rooms in the normal areas while still allowing "maze" areas to generate with interconnected hallways.
- Added a Vending Machine to the main floor near where the monitors used to be.
- I've messed with generation to try and make more branch paths occur on the different floors. It isn't perfect but there should at least be a few rooms that spawn on each floor. (No more elevator trips to visit a blank wall)
- Door colliders are now disabled when interacting with the door. (You will no longer get pushed by doors you open)
- Enemies will now be able to correctly pathfind through double doors.
- The "Old Lobby" tile now has significantly more scrap spawns so on the rare chance it does spawn you can find a significant reward inside.
- Have you ever ran into weird invisible boxes in some of the larger rooms? Turns out they were meant to be file cabinets with a missing mesh renderer! I've fixed their renderer so you can now see them.
- More Secure Doors should spawn in the interior to go along with more tiles having spots for them to generate. (Especially in the new area)
- Breaker Boxes have more places they can spawn.
- Updated ReadMe.
- Office cubicle seperaters now have colliders.
- Fixed item sounds being global.
- (hopefully) Fixed laptop item softlocking players.
- Changed desk colliders to allow you to grab items underneath desks.
- Raised tabletop scrap spawns in several places to prevent them clipping through the objects they are placed on.

## 1.2.6

- Completely removed shrimp. (Probably)
- Edited preview text.

## 1.2.6

- Removed Shrimp - Separated into [LC_Shrimp](https://thunderstore.io/c/lethal-company/p/Piggy/LC_Shrimp/).

## 1.2.5

- Fixed an error that occurred when entering orbit.

## 1.2.4

- Fixed an issue where shrimp would increase their hunger level when eating scraps.

## 1.2.3

- Fixed an issue where shrimp would not change their status when killing a player.
- Fixed an issue where shrimp hunger was inconsistent.

## 1.2.2

- Shrimp's code has been completely refactored. Now fully compatible with mods that change mod creatures, like StarlancerAIFix.
- This hasn't been fully tested in multiplayer yet, so you may want to disable it unless you're testing it.
- Shrimp's behavior will be slightly reworked in the future.

## 1.2.1

- Updated v60 compatibility.
- Shrimp spawning has been completely blocked (shrimp will return after the rework!)

## 1.2.0

- Updated v55 compatibility.
- Currently Shrimp is not working properly. Thank you for your understanding! It will be fixed later.

- Bugfixes:
- screen would shake very quickly when the elevator is moving.
- other players would sometimes be invisible when entering an elevator.
- other players would briefly travel away when entering an elevator and then return.
- dropping an item into an elevator would cause it to get stuck in the middle of the elevator.

## 1.1.30

- Changed the pitch of the music that was changing when the elevatorMusicPitchdown config was set to true to match the elevator music.
- Removed scale setting code for compatibility with LittleCompany.
- This update may cause some bugs when dropping items in elevators. Although it may be inconvenient, if a bug occurs, please put the item in the cabinet in the elevator. Most bugs related to elevators will be fixed in the v55 update.

## 1.1.29

- drastically increased brightness of the Halt tile's hanging light. Instead of using light to understand surroundings, use it as a direction indicator!

## 1.1.28

- Removed deprecated code.

## 1.1.27

- Fixed dungeon generate with doors open.

## 1.1.26

- Added something to the start room.

## 1.1.25

- Completely re-coded the Shrimp attack code and hope it works

## 1.1.24

- Improved Shrimp's attack code.

## 1.1.23

- Fixed a bug that caused extreme lag when attacking a satisfied Shrimp.

## 1.1.22

- Fixed a bug where elevator command help was being duplicated every time the lobby was recreated.

## 1.1.21

- Added a command to move the elevator to the terminal. ( elevator [1f, 2f, 3f] )
- Changed the sound the Shrimp makes when it is satisfied.

## 1.1.20

- Minor fixes.

## 1.1.19

- Changed the Y value of the Halt room spawn position.

## 1.1.18

- Minor fixes.

## 1.1.17

- Fixed a bug where props would spawn overlapping in some rooms.

## 1.1.16

- Start room now has multiple branches.

## 1.1.15

- Added another prop type to the Large Room.

## 1.1.14

- Added config to disable cameras inside offices.
- Added config to specify frame rate for cameras inside the office.

## 1.1.13

- Fixed some code.

## 1.1.12

- Now Shrimp is happier when he's full :D.

## 1.1.11

- Reupload due to Thunderstore error.

## 1.1.10

- Removed fire exit spawn in Halt's Hall.
- Minor fixes.

## 1.1.9

- Changed the code that drops all items during Halt encounters to network function.

## 1.1.8

- Fixed an issue where Halt spawn probabilities were reversed.

## 1.1.7

- Changed Halt's config to a spawn "probability" configuration.

## 1.1.6

- Fixed a bug where terminal could not be quit.

## 1.1.5

- Added config to determine whether to increase the brightness when encountering Halt, For compatibility with Diversity's Full Darkness config. This config requires Diversity to be installed to work.
- Minor fixes.

## 1.1.4

- Fixed an issue where Halt's eyes would not appear on screen when attacked.
- Increased the bounds of the start room.

## 1.1.3

- Increased brightness during Halt encounters (I had the game's gamma set to max lmao)

## 1.1.2

- Added a config to turn off custom camera shake for players with motion sickness.
- Lowered Halt's spawn rate.
- Increased "TURN AROUND" text size.
- Fixed an issue with reverb being applied to Halt's sound effects.
- Reanimated camera shake to be more intense.

## 1.1.1

- Made Halt's body darker.
- Stamina is no longer consumed during encounters with Halt.
- Removed vent in start room.

## 1.1.0

- Added a new enemy "Halt" (from DOORS).
- Added one room.

- Fixed some bugs.

## 1.0.25

- Now that the shrimp is satisfied, he won't look at the source of the noise.
- Shrimp's body now rotates as he looks at the source of the noise.

## 1.0.24

- Improved moons registration.

## 1.0.23

- Fixed some important bugs in Shrimp.

## 1.0.22

- Minor fixes.

## 1.0.21

- Fixed a bug where doors and blockers were spawned overlapping in a room.
- Fixed (probably) compatibility issue with CullFactory.

## 1.0.20

- Improved the AI of Shrimp.

## 1.0.19

- Increased the weight of relatively short corridors and decreased the weight of long corridors.
- Increased spawn weight in start room again.

## 1.0.18

- Increased spawn weight in start room.
- Removed render for elevator collider.

## 1.0.17

- Changed elevator panel to allow you to move by floor from up/down.
- Fixed a bug where the start room path panel was displayed incorrectly.

## 1.0.16

- Added a wait time to the Shrimp's reaction when it takes damage.

## 1.0.15

- Fixed a bug where walls in some rooms were generated strangely.... sry

## 1.0.14

- Removed LocalPropSet for intersections and created new ones for each room.
- Added simple feature to check if there is a path on each floor.

## 1.0.13

- Fixed a bug that caused some rooms to overlap.
- Fixed an issue where the door in the start room was assigned the wrong texture.
- Changed the color of the elevator's apparatus to avoid confusion with the apparatus item.

## 1.0.12

- Improved the AI of Shrimp:
- Shrimp will now move out of the way when it's in your way!
- Shrimp with no targets now have lower necks.
- The shrimp can now hear the sound and will look in the direction of the sound.

- Fixed an issue where the door in the start room was assigned the wrong texture.

## 1.0.11

- Reverted some changes to 1.0.4.

## 1.0.10

- Changed the default for scrap spawn configuration to disabled. I will change it later when it is completely fixed.

## 1.0.9

- Fixed a bug where more than 2 shrimp would spawn.
- Improved the item parent system.

## 1.0.8

- Changed the Dungeon Register again.
- Changed the Item Register.
- Fixed an issue with screwdrivers being assigned to mesh variants on laptops.
- Fixed incorrect item ID.
- Removed mesh variant from screwdriver.
- Assigned a main renderer for all scraps.

## 1.0.7

- Fixed a bug that caused infinite dungeon generation.

## 1.0.6

- Fixed a bug where items would sometimes not interact with elevators.

## 1.0.5

- Changed the Dungeon Register.
- Removed BigDoorSpawn due to a bug.

## 1.0.4

- Added 3 tiles (rooms).
- Added 5 scrap (Coin, Laptop, Screwdriver, Toolbox, Wrench).
- Fixed scrap spawning.
- Fixed an issue where maps were generated too large.
- Elevator Apparatus system has been temporarily removed (for my sanity).
- Changed the door model.
- Reanimated the lung place animation (although it doesn't work).
- Start room has been reworked and the elevator floors have been increased from 2 to 3 (It may be reworked again in the future).

- Revamped the entire elevator system.
- Improved map generation and added override dungeon scaling config.
- Fixed an issue with vents being misplaced in server rooms.
- Added a config to adjust the volume of elevator music.
- Fixed a bug where shrimp could not recognize players.

## 1.0.3

- Fixed an issue with overlapping start rooms.

- Shrimp has been nerfed:
- Shrimps will no longer chase players when they determine that they cannot chase them.
- Increased overall hunger reduction when eating item.

## 1.0.2

- Changed network variables to be initialized every time the moon is landed.

## 1.0.1

- Fixed a bug where elevator doors would not open after taking Apparatus.

## 1.0.0

- Out of beta.
- Fixed a bug where enemies could not enter staircase rooms.

## 0.2.8

- Fixed a bug that occurred when taking Apparatus from interiors other than Office... Again.

## 0.2.7

- Fixed a bug that occurred when taking Apparatus from interiors other than Office.

## 0.2.6

- Made some minor improvements to the elevator doors.

## 0.2.5

- Added little animation when placing Apparatus in an elevator.
- Fixed several bugs that occurred when lobby restarted.
- Fixed a bug where the doors would not open when the elevator automatically descended after taking Apparatus. again.

## 0.2.4

- Added a ladder in case the elevator is unavailable for some reason.
- Fixed a bug where the doors would not open when the elevator automatically descended after taking Apparatus.
- Fixed a bug where rooms would spawn overlapping due to forgetting to set the bounds override.

## 0.2.3

- Added auxiliary power units to elevators (Compatible with FacilityMeltdown mod).
- If an apparatus within the interior was unplugged, the elevator was changed to be inoperable without the use of auxiliary power.
- Shrimp desync issue is probably.. fixed.. again.. idk
- Fixed a bug where doors in some rooms were not positioned correctly.

## 0.2.2

- Fixed bug where turrets would penetrate some walls.

## 0.2.1

- Fixed bug where rooms would spawn overlapping.
- Fixed a bug where players who used the elevator even once would not respawn on takeoff.
- Fixed bug causing doors to be misplaced.
- Added exception handling for shrimp not syncing bug. Please let me know if you encounter this bug.

## 0.2.0

- This update uses a lot of network variables and may cause some frame drops. Please let me know if you are experiencing severe frame drops.
- Synced Shrimp's hunger value.
- Separated the stairs and hallway in the Staircase room.

- Fixed a bug where:
- Shrimp could not open doors.
- Shrimp would occasionally look in strange directions.
- Shrimp's position and orientation were out of sync.
- Shrimp would not react when hit.
- Fixed a bug where you could get stuck in the walls of the staircase room.
- Fixed a bug where some rooms would spawn incorrect doors/no doors.

## 0.1.9

- Fixed a bug where staircase rooms had incorrect colliders.

## 0.1.8

- Shrimp now spawns on a modded moon.
- Added configuration for Shrimp to set spawn weight on modded moon.

## 0.1.7

- Added a new facility style stair room to replace the old stair room.
- Added a small LED next to the elevator panel to make it easier to see if the elevator is up.
- Removed some rooms (two door rooms and rooms with stairs) because the original file was broken... sad
- Removed test room.
- Made some improvements to dungeon creation.
- Reduced the overall size of assets.
- (Probably) fixed the sliding glass door. Once again.
- Fixed a bug where spray paint/blood was not visible.
- Changed the name from ShrimpSpawnableMoons to ShrimpSpawnWeight, so you need to reconfigure it.
- Changed the text on the elevator panel.
- Fixed an issue where shrimp would not spawn properly in office interiors.

## 0.1.6

- Fixed some bugs.

## 0.1.5

- Fixed a bug where elevator safety doors would not open when starting the game.
- Fixed a bug that caused RoundMapSystem to consistently error out on loading.
- The emergency exit is a bug we can't fix at the moment due to an error in LLL (or a DungeonFlow error in Office), and I'm talking to the LLL developers - sorry!

## 0.1.4

- S m o l Update.
- Minor performance improvements for items.
- Fixed some bugs in the shrimp.

## 0.1.3

- Added two large rooms.
- Added a prop to several rooms.
- Remodeled the starting room.
- Improved dungeon generation.
- Added Lung apparatus socket machine spawn.
- Improved the elevator system, but haven't tested it yet.
- Fixed a bug where Shrimp's "Satisfaction" system did not work properly.

## 0.1.2

- Added “Satisfaction” value to Shrimp. Shrimp will now stop following the player for a while if they have enough food. (not tested)
- Fixed softlock occurring when shrimp spawn on all clients except the host.

## 0.1.1

- Added pictures to README.md.
- Fixed extreme frame drops occurring when an interior other than an office was created.
- Reduced the time it takes for an enraged Shrimp to reach its maximum speed (acceleration remains the same).
- Added configuration related to the spawning of Shrimp.

## 0.1.0

- Enemies can now spawn!
- Added a new enemy "Shrimp".
- Fixed an issue where navmesh agents could not navigate through staircase rooms (even though no enemies spawned).
- Compatible with LethalNetworkAPI 2.1.0.

## 0.0.9

- Fixed some issues.

## 0.0.8

- Fixed an issue with guaranteed spawns on every moon. now appears randomly on paid moons.
- Added configuration for interior spawns.
- Temporarily completed the unfinished start room.
- Created a staircase on the left side of the start room (ladder included, so two handed items cannot be moved).
- Fixed extreme frame drops in game before landing or when on a moon with no office interiors spawned.
- Revamped trap rooms (still unfinished, you won't see any changes in actual gameplay).
- Added [OdinSerializer](https://thunderstore.io/c/lethal-company/p/Lordfirespeed/OdinSerializer/) as a dependency in preparation for LethalNetworkAPI v2.0.0.

## 0.0.7

- Fixed issue with guaranteed spawns on any moons.
- Changed the layout of some room.
- Every song filters have been edited to make them less painful to the ears.
- Removed the cootie theme.
- Fixed the pitch of the shop theme the same as the original.

## 0.0.6

- Changed the layout of some room.

## 0.0.5

- Edited README.md.

## 0.0.4

- Moved to LethalLevelLoader.
- Changed the layout of the server racks.
- Fixed an issue where some items would not set the elevator as their parent object.
- Fixed some errors that occurred during interior generation.
- Lots of optimization work.
- Sadly no monster spawns yet :/

## 0.0.3

- Remove test item spawn.
