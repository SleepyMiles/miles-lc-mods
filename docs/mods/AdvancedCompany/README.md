# AdvancedCompany

![Logo](https://iili.io/dftztqX.jpg)

> **Unofficial compatible update for v.81 by SleepyMiles**

The most stable and solid lobby expansion mod including many quality of life improvements and new items. Fully configurable in-game with presets.

It's about time the company supplies you with the latest tech created from all the scrap you've collected.

## Key features
* Expanded lobby size of **32** players.
  - Enables late joining (Even on the moons surface. Puts players into spectator mode)
  - Better sync for late joiners (Syncing animation states of objects, doors, lights of the ship etc.)
  - Synchronization framework for other mods to work with. (Synchronizing data at handshake)
  - According to testers offers the most stable and least lagging experience even in big lobbies.
  - Adding custom UI to make 32 players fit.
* Fully configurable lobby rules including item conditions, properties, scrap value, moon spawn rates etc.
* Supports most cosmetics created for MoreCompany. If you have MoreCompany installed it will get automatically deactivated but the standard cosmetics will get added.
  - Cosmetics can be changed while in-game.
* A **clothing system** consisting of helmet, body and boots slots which actually display on the player models.
  - Clothing slots can be removed by deactivating all items which can go in them in the lobby config.
* Adding **new items** to the store:
  - Missile launcher
  - Lightning rod
  - Vision enhancer
  - Flippers
  - Bulletproof vest
  - Rocket boots
  - Helmet lamp
  - Headset
  - Tactical helmet
* Adds a **portable terminal** to level your perks and buy items.
* Adding **new cursed items** which change the way you experience the game when picked up.
* A comprehensive **perk system** including ship perks (saved on servers savegame) and player perks (saved locally)
* An intelligent **flashlight key** and rework of how flashlights work:

  You can only have one flashlight on at any given time. If you pick up an active flashlight while having one active, the picked up one will automatically be turned off for example.
* **Hotkeys** for inventory slots.
* **Battery indicators** beneath your items in your inventory.
* The ability to **extend your deadline** by one day per quota. (For the price of the current quota)
* **Balancing changes** regarding weather effects. You will get more loot in more dangerous conditions.
* Increases max capacity of ship to 999 items.
* All functionality can be **fully customized**.
  - Expansive in-game config options with presets.
* **More scrap!**

## License
This mod is licensed under CC0 1.0 and part of world-wide public domain.

## Configuration

All configuration is done in-game. You can create presets for your lobby rules and adjust the game to your liking. To see and modify modded moons, items and scrap you need to go into the game once at least for them to show up.

![Lobby config](https://iili.io/dfZpRft.gif)

## Expanded lobby

AdvancedCompany offers the ability to play with up to 32 players. AdvancedCompany also aims at increasing the sync between players by custom sync code performed at handshake with the lobby. This includes stuff like animation states, ship lights and locked doors. You can also activate the ability to join a session while players are on a moon. This will put the newly joined players into spectator mode.

You might be able to change that behaviour in later version.

## Clothing system & Hotbar
The clothing system consists of three new slots in your inventory. This mod heavily changes the hotbar and gives you options to adjust it. It also adds energy bars to all slots.

![Clothing slots](https://iili.io/dftJN6B.jpg)

All equipped items will be shown on the player. You can press and hold Alt to access the equipment inventory.

Equipment slots can be hidden by deactivating all items which go into a certain slot.

![Equipped clothing](https://iili.io/dftqkib.md.gif)

## Missile launcher
The missile launcher will launch firework. The perfect and most stylish way to celebrate a new quota... or to distract enemies from a mile away. One missile launcher comes with 3 preloaded missiles. Empty its even more worthless than scrap.

![Fireworks](https://iili.io/dftBc7V.gif)

## Lightning rod
It's stormy outside and some of your team mates dont want to go out of the landing ship? Take the initiative and place a lightning rod outside. After being deployed you can't pick it up again but at least it is working. No excuses to not go to stormy planets!

![Lightning rod](https://iili.io/dftBOkN.gif)

## Vision enhancer
Afraid of the dark? The company has the perfect solution for you. But beware. The battery life is limited. If mapped to the same button as the flashlight the night vision will be prioritized. When empty you can use the flashlight button for flashlights again.

![Vision enhancer](https://iili.io/dftBmCu.gif)

## Flippers
With flippers you can finally swim like a fish. Be a good asset and go to the flooded planets! You will swim in the direction you are looking at. Press the jump key to ascend or the crouch key to descend.

![Flippers](https://iili.io/dftCx3v.gif)

## Bulletproof vest
Tired of dying to turrets? Protect yourself with this bulletproof vest. It **ONLY** protected from projectiles. Depending on the origin of the projectile the vest will take different amounts of damage. You can fully configure the vest to your liking. With standard configuration a vest has 90HP. Turrets will reduce the HP by 5, a shotgun shot by 30. If reaching 0HP the vest will get destroyed. At 100% HP it will reduce 100% of the incoming damage. At 0% only 50%. Damage is shown on the item itself in 3 stages.

![Bulletproof vest](https://iili.io/dftCYnn.gif)

## Rocket boots
You need a little extra height? Then the rocket boots are perfect for you. They allow you to perform a double jump.

![Rocket boots](https://iili.io/dftCE8l.gif)

## Helmet lamp
If your hands need to be free or you want to lend your friends some extra light, use this slim head lamp.

![Helmet lamp](https://iili.io/dftCUAB.gif)

## Headset
Tired of pressing buttons on your walkie-talkie? Use this comfy headset!

![Headset](https://iili.io/dftCptt.gif)

## Tactical helmet
In need for light and communication? The tactical helmet is the right choice for you!

![Tactical helmet](https://freeimage.host/i/dftn1ig)

## Portable terminal

To bring up your portable terminal you can press X (default key). In your portable terminal you have different options. For example you can use the new store by typing "store":

![Store](https://iili.io/dftnPbS.gif)

You can also extend your deadline by a single day per quota if the lobby configuration has this option activated:

![Extend deadline](https://iili.io/dftouLX.jpg)

The portable terminal might have conflict with some emote mods. Please report compability issues on the Issues tab on Github.

## Perks

The perks system gives you something to progress in beyond a single session. You can open the perks system on your portable terminal with the "**perks**" command. There currently is no alternative for the ship terminal. So if you deactivate the portable terminal, you won't be able to access your perks.

You can deactivate any perk you don't like in the lobby settings. You can still use the base values to tweak your game. So if you want everyone to have 6 inventory slots and not be able to upgrade that, you can deactivate the perk and set the base value to 600% (100% = 1 inventory slot).

![Perks overview](https://iili.io/dftx2yl.gif)

### Player perks

Player perks are saved locally on every clients PC when the lobby hasn't deactivated "Save progress". So earned XP will stay as long as you dont reset them.

If "Save progress" is activated however, your progress will be saved in the server savegame and will be reset once you dont meet the quota.

![Player perks](https://freeimage.host/i/dftxvSf)

- **Sprint speed**

  Increases the speed of sprint.
- **Jump height**

  Increases the height of your jumps. (Also affects the rocket boots double jump)
- **Jump endurance**

  Reduces the stamina usage for jumping.
- **Sprint endurance**

  Reduces the stamina usage for sprinting.
- **Reinforced legs**

  Increases the heights for the damage and death zone of falls.
- **Protective skin**

  Increases your protection against damage from enemies. (Wont protect you from instant kills like mines or dogs)
- **Bodybuilder**

  Reduces the stamina cost for carrying weight while sprinting.
- **Heavy runner**

  Reduces the speed decrese for carrying weight while sprinting.
- **Carry bags**

  Increases the inventory size.
- **Strong arms**

  Increases the chance for a critical attack (one-hit) on enemies.
- **Climbing speed**

  Increases the speed you can climb on ladders.

### Ship perks

Ship perks and XP are saved in the hosts save file for the session. So if the host removes the savefile, the ship XP and perks are lost.

![Ship perks](https://iili.io/dftzfHv.jpg)

- **Scanner distance**

  Increases the distance from which items can be scanned.
- **Batterypack**

  Increases the size of batteries for all items.
- **Deadline discount**

  Reduces the price to extend your deadline by a day.
- **Landing speed**

  Reduces the time needed for landing and taking off.
- **Express delivery**

  Reduces the time needed for items to arrive with the dropship.
- **Loot saver**

  Increases the chance for loot items to stay on your ship when all players die.
- **Travel discount**

  Reduces the price to travel to moons.

### Respec

If you join a lobby with a configuration in which your player perks would result in a negative XP or you having levelled a skill which is deactivated or has fewer levels in the current lobby, you will get auto respecced. If you want to manually respec your character or reset the progress you can do so in the respec menu. If you are hosting the lobby you can also respec or reset the ship.

![Respec](https://iili.io/dftzVUb.jpg)

## Keybinds and intelligent flashlight key

You can bind a key for toggle flashlight (standard: 'F'). The flashlight toggle mechanics is rewritten completely.

When you press F what happens is determined by multiple factors which are chosen to ensure it always does what you want it to do:
- If you have bound night vision to the same key, prioritize night vision. If night vision is empty go to next step:
- Check if any flashligh is currently activated. If so: Deactivate it. Otherwise:
- Select flashlight with highest tier and battery charge in inventory. Pro flashlight is prioritized over normal flashlights. Activate this flashlight.

Activation of a flashlight, either by pressing the flashlight key or by pressing the activate key when holding it will result in the following behaviour:
- Search for all other flashlights in the inventory and deactivate them.

If you pick up a flashlight which is turned on it will get deactivated when you already have an activated flashlight in your inventory.

You can also set keybinds for your hotbar to select items quickly.

All keybinds are using LethalInputUtility, so you can change or unbind them in-game.

# Credits
**Unofficial Update (v.81):**
- SleepyMiles

**Programming:**
- [Redacted due to Thunderstore Lethal Company modding moderator team policy]

**Graphics:**
- Pixel art of LCGuy by Velitow

**3D Models:**
- [Redacted due to Thunderstore Lethal Company modding moderator team policy]

- Rocket boots are based on the model of Shao-Khan

  https://www.cgtrader.com/free-3d-models/character/clothing/dirty-shoes

- Warplane toy based on 3D scan of adekvat
  
  https://www.cgtrader.com/free-3d-models/scanned/various/old-ussr-soviet-metal-toy-airplane

- Tank toy based on 3D scan of adekvat

  https://www.cgtrader.com/3d-models/scanned/various/old-ussr-soviet-metal-toy-tank-scan-high-poly

- Toy car based on 3D scan of adekvat

  https://www.cgtrader.com/3d-models/scanned/various/old-ussr-soviet-metal-toy-car-ural-juices-water

- WML-H2 by Mayess

  https://sketchfab.com/3d-models/wml-g2-04f05da3b9a8400f904f70500081402d
  
- PBR Tactical Helmet by Simon Coenen

  https://sketchfab.com/3d-models/pbr-tactical-helmet-ad5de2de22af429cbcaf2a96e3520b80
 
- Sport Shoes by Francesco Coldesina

  https://sketchfab.com/3d-models/sport-shoes-cb341fb20baf4bb6b7811008b8a6a8b8

**Music:**

License:

https://pixabay.com/service/license-summary/

- Light shoes music:

  Ghost Coast 2030 by Eldunn
  
  https://pixabay.com/music/synthwave-ghost-coast-2030-20822/
  
- Bunny ears music:

  who are you by Sekuora
  
  https://pixabay.com/music/synthwave-who-are-you-175179/
  
**Sounds:**

- Various CC0 from freesound.org

  Edited by [Redacted due to Thunderstore Lethal Company modding moderator team policy]

**Tester:**
- Zesicion
- xxXDerJokerXxx
- xPapaWolfix
