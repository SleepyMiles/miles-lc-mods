# SafeQuit

With a lot of mods, Lethal Company often shows a **Unity crash window when you close it**. The crash happens in Unity's own clean-up, after the game has already saved. Lots of people close the BepInEx console window instead, which skips that clean-up.

SafeQuit does the same thing for you, safely, whenever you quit (Quit button, Alt+F4 or the window's X):

1. Leaves the lobby if you're still in one (the host saves the game, like normal).
2. Lets the game and every mod save their things (game save, mod data, settings).
3. Closes the game before Unity's clean-up, so there's no crash window.

It takes about a second longer than normal.

## Settings

| Setting | Default | What it does |
|---|---|---|
| Enabled | true | Off: the game closes the normal way. |

## Good to know

- Only the person who has SafeQuit installed is affected. It doesn't need to be on everyone's game.
- Works with LethalPerformance (it writes LethalPerformance's waiting saves and settings before closing).
