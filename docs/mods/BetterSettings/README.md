# Better Settings

Replaces the game's settings screen (main menu and pause menu) with a tabbed one in the game's own style. Only changes your own game: nobody else needs it.

## Audio
- **Master**, **Sound effects**, and three **Music** sliders: Menu, In game (landing and ambient music), From mods (LC Office elevator, Facility Meltdown and others).
- **Voice chat: friends** (up to 200%, with a limiter so a boost doesn't crackle) and **Voice chat: your mic** (up to 200%).
- **Microphone**: pick your input device, a mic test bar.
- **Voice mode**: Toggle (mic on, the voice key mutes / unmutes), Push to talk, Push to mute, Mic off.

## Display
- Resolution (the game's "Pixel Resolution"; with ResolutionAdjust its real resolutions are listed) and display mode (Apply shows up when you change it; goes back after 10 seconds if you don't confirm).
- **Limit frame rate** with any number you type, V-Sync, Show FPS, Gamma.
- Terrain / grass detail, pixel resolution, motion blur, indirect lighting, head bobbing, arachnophobia mode.

## Controls
- Look sensitivity, invert Y, invert item scroll, toggle sprint, **toggle crouch** (off = hold to crouch).

## Keybinds
- The game's keys and a section for every mod that has keys (through InputUtils), in the same style. Click a key, press the new one; Reset puts one key back.
- Switch between **Keyboard / Mouse** and **Controller** at the top (each has its own keys), and search for a key or action.
- Mod keys are saved the way InputUtils saves them, so the mods keep working.

## Mods (needs LethalConfig installed)
- Every mod's settings in this menu: a list of mods with their icons on the left (with its own **Search a mod** box), the selected mod's settings on the right, one line per setting, grouped by section. Sections start folded; what you unfold stays open until you close the game.
- **?** next to a setting shows its description, default and allowed values when you point at it.
- Changes go to the mod right away, the same way LethalConfig does it. Each setting has a **Reset**.
- **Search a setting** looks through **All mods** or only the **Selected mod** (the box next to it).
- Long texts: **...** opens a big editor, **as text** or **as a list** (the value split at its commas, with add / remove).
- Colors get a swatch that opens a color picker: shade square with a hue strip, red / green / blue sliders, hex box, and a table of ready-made colors.
- Keys that mods keep as text (KeyCode, key path like `<Keyboard>/r`, keyboard shortcut): click, then press the key or mouse button.
- From LethalConfig: buttons that mods add, **NEEDS A RESTART** marks and **LOCKED** settings (when a mod tells LethalConfig a setting can't be changed right now, with its reason under ?).
- Mods with more than one config file get a **Config file** box at the top right.
- Config files that no loaded mod has open (sound packs like LethalResonance, extra files of a mod) are shown under their mod, with its name and icon; pick the file in the **Config file** box. Those are written to the file and apply after a restart. Files that match no installed mod end up in **Other config files** at the end of the list.
- **Hide LethalConfig's buttons** removes its button from the main menu and the pause menu (on by default, switch at the bottom of the mod list).
- Lists scroll one step per wheel notch and have a bar to drag.

## Good to know
- Everything applies right away; there is no "Confirm changes".
- Mod music is found by rule: a sound the game itself doesn't have that is at least 20 seconds long, or has music / theme / song in its name. The log lists every sound it counts as mod music. Fix wrong guesses with **Always music** / **Never music** in the config.
- `General > Use the new settings menu = false` gives the game's own screen back.
- Push to mute / Toggle mute use the game's own mic switch, so HUD mods (EladsHUD+'s red mic icon) show when you're muted.
- With SkipIntro's menu colors turned on (and "SubMenus"), this menu uses the same colors.
- A mic plugged in after the game started only shows up after restarting the game (the game's engine reads the list once).
