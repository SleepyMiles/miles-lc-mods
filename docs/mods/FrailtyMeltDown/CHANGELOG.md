## 0.5.1 (Beta)
- Fixed: when everyone died the ship stayed until the meltdown ended. The "ship doesn't leave on its own" rule also stopped the game's "everyone is dead" departure; it now only holds the midnight departure, and only while the holder is alive.
- The last stage starts at **15 seconds** (was 10), and its line is "I'M RUNNING OUT OF TIME!! I MUST LEAVE THIS PLACE NOW!!". Settings renamed: "Boost at 15 seconds %", "Line at 15 seconds" (the old 10 seconds entries in the config file do nothing now).
- In the last 15 seconds melee hits deal **5 times** their damage ("Melee multiplier at 15 seconds", host). Before that it stays the melee bonus (+1, so twice for the shovel and the knife).
- The two lines are said under **your player name** instead of "YOU" (LAN and online).
- Glass Shard for other players: the hold you set for yourself was in your own view only, so others saw the kitchen knife's grip. Your game now tells the others how the shard sits on your hand and they see it the same way in your fist. Each viewer also has "Others rotate X / Y / Z" to turn what they see.
- Everyone needs this version.

## 0.5.0 (Beta)
- Armed With Death now grows as the meltdown runs out (on the holder's game):
  - **30 seconds left:** the tip box says "YOU: 30 seconds left. I must hurry!". Run speed, jump, stamina regen, sprint drain, climb and interact bonuses grow (by half, setting "Boost at 30 seconds %"). The frame turns blue (Drinkable's). Inside the facility the nearest exit is marked on the compass, like Cheesy Soda's Cheese Sense without the scrap.
  - **10 seconds left:** "YOU: NO TIME LEFT!!! I MUST LEAVE THIS PLACE NOW!!". The bonuses grow again (doubled, "Boost at 10 seconds %"). The frame turns purple. Critically injured you can sprint, at the 30 seconds speed.
  - Settings in "Final seconds": the two boosts (host), the two lines, Show the nearest exit.
- Critically injured with Armed With Death: the red on the screen is turned down (Display / "Critical red tint %", 40). Health still never comes back (no regen to 20), as before.
- Glass Shard:
  - The hold is really still now. It was put back on its still frame about 5 times a second, which showed as a shake; now the arm is kept on one pose every frame and the animator is left alone.
  - Every stab restarts the stab animation, so a faster "Stab speed %" can be spammed (before, the next stab had to wait for the old one to blend out and often showed nothing).
  - Free spin and move in your own view: Hold rotate X / Y / Z (degrees, around the shard's own axes, the fist stays in place) and Hold move X / Y / Z (cm on screen). The old Hold tilt / turn / depth still work.
- Everyone needs this version (the host's settings message has two more numbers).

## 0.4.1 (Beta)
- Last Stand: a hit that would kill you while you're above critical now leaves you critically injured at 10 HP instead (so the "every hit deals 1" part can still save you). Same conditions as before: Armed With Death, the Glass Shard in your hand, an apparatus in your inventory.
- New Last Stand icon: drawn from the Glass Shard model's own texture (steel blue glass, its dark cracks and edges) instead of the smooth white horn.
- The apparatus on the back: with Armed With Death, an apparatus put away in the inventory hangs on the player's back (upright, behind the air tank), for everyone with the mod. On AdvancedCompany's Default, Female and Thicc models. Settings "Apparatus on the back": Show it, Height, Tilt, Depth Default / Female / Thicc.
- Fixed: with Armed With Death "hands full" only went away for the apparatus. Now no item takes both hands (a teammate's body still does).
- When Armed With Death ends and you're alive, big items in your inventory drop at your feet (the apparatus too), like FrailtyMedical's Steroids wearing off. One in your hands stays.
- The day clock no longer stops. It runs on to 11:59 PM and stays there; the ship doesn't leave on its own while Armed With Death is armed, so the meltdown is the only timer left.

## 0.4.0 (Beta)
- New buff, Last Stand: with Armed With Death on, the Glass Shard in your hand and an apparatus in your inventory. Once you're critically injured (10 HP or less), every hit deals only 1, also the instant kills Armed With Death turns into hits (dog, Bracken, Barber, ...), lethal falls, the Cruiser, bleeding out and the Shy Guy (it gets stunned again). Still deadly: pits, the meltdown explosion, being left behind, drowning, and the grabs (Forest Giant, Old Bird, Kidnapper Fox, Masked) until they get their struggle.
- Its icon is the Glass Shard drawn like FrailtyMedical's pen icons, teal frame like Bandaged.
- Without EladsHUD+, Armed With Death and Last Stand now show in their own icon row next to FrailtyPlus' (after TZPPlus', Drinkable's and FrailtyMedical's), same size, tilt and animated frame as theirs. Settings in Display (same as FrailtyMedical's): Show buff icons without EladsHUD+, offset X / Y, scale.

## 0.3.8 (Beta)
- Icon: back to the 0.3.6 placement (big skulls behind the head, smaller darker ones behind the shoulders), moved in so no skull goes past the frame.

## 0.3.7 (Beta)
- Icon: all four skulls are the same size now and none of them overlaps another; the whole icon stays inside the frame, in the same area as FrailtyPlus' Concussed and Paralysis (the person is the same size as on those).

## 0.3.6 (Beta)
- FacilityMeltdown is now required, like FrailtyPlus (an add-on for both, the way FrailtyMedical is for FrailtyPlus). The setting "Delay without FacilityMeltdown" is gone.
- The Armed With Death tooltip no longer gives exact numbers, like the other mods' tooltips: "You have nothing left to lose: you take much less damage and a hit that would kill you at once only wounds you, but you can't heal. You're faster and stronger, you sense danger nearby, fear can't touch you, and every monster that hits you gets hit back."
- Icon: two more skulls, a little darker, at the lower left and right behind the person.

## 0.3.5 (Beta)
- New Armed With Death icon, drawn like the FrailtyPlus / FrailtyMedical / TZPPlus / Drinkable icons: the Frailty person (as on Concussed and Paralysis) in white because it's a buff, no stars, two red skulls behind it, and the glitch the debuffs have. Teal frame like Bandaged.
- Its tooltip is written like the others: "You have nothing left to lose: you take 60% less damage and a hit that would kill you deals 50 instead, but you can't heal. You're faster and stronger, you sense danger nearby, fear can't touch you, and every monster that hits you gets hit back." (with the host's numbers).

## 0.3.4 (Beta)
- Glass Shard: taking it out or picking it up no longer plays the kitchen knife's re-grip (the blade turns about 28 degrees in the fist, and the wide shard went through the fingers). The hand blends straight into the still hold.

## 0.3.3 (Beta)
- Fixed: with the Glass Shard the stab animation (and throwing / putting it away) often didn't play until you switched to another slot and back. The still hold kept the hand's state locked every frame, which kept the game from changing it; now it only nudges the hold back about 5 times a second (you can't see the difference) and lets every stab, throw and drop through.

## 0.3.2 (Beta)
- Glass Shard in the hand: it stays still (the kitchen knife's hold keeps turning the blade in the hand), and after a stab the hand comes straight back to it (the knife restarts its hold and twists the blade back). Taking it out still plays one short re-grip. Also in other players' view of you. Setting Glass Shard / Still hold.
- Its point tilts a little more forward in your own view (12 degrees). Settings Glass Shard / Hold tilt, Hold turn, Hold depth (your own view).
- Saving the config file while the game runs applies it at once (to tune the hold without restarting).
- New inventory icon that matches the shard's look.

## 0.3.1 (Beta)
- Glass Shard fixed: the model was inside out (black on the floor, odd in the hand). Its texture now lines up with the shape (the ink outline runs along its edges), and it's held by its wide end in the fist like the knife's handle, point forward like the blade.
- The scan shows its value (it said only "Value: ").
- Stronger than the knife: reaches almost as far as a shovel (1.4 m, knife 0.75) with a wider sweep, but stabs half as fast (every 0.86 s, knife 0.43). Settings Glass Shard / Reach and Stab speed % (each player's own). Damage is the knife's (Armed With Death's melee bonus still adds to it).

## 0.3.0 (Beta)
- New: the Glass Shard. When the apparatus pull arms Armed With Death, it drops at the puller's feet. A copy of the Butler's kitchen knife (stab, damage, sounds, value) with the glass shard model, texture, icon and name; saved with the ship like other items. Setting Glass Shard / Drop it (host). Model files next to the DLL (GlassShard.mesh, GlassShard.png, GlassShardIcon.png).

## 0.2.5 (Beta)
- Ghost Girl: if she haunts the one with Armed With Death, she's removed (the host despawns her for everyone). On that player's game she stops chasing at once and the muffled chase sound goes back to normal.

## 0.2.4 (Beta)
- AdvancedCompany perks stack with Armed With Death again (0.2.3 switched them off by mistake): sprint speed, jump height, endurance, stamina regen, climbing, Strong arms on top of its own buffs; Protective skin now also lowers Armed With Death's (reduced) hits, the way AdvancedCompany does it (not falls, not the fixed hits like the dog's 30).
- EladsHUD+'s detailed stamina (rate, time to empty / full) includes Armed With Death: its stamina change now happens early in the frame (like Drinkable), before AdvancedCompany's perks and the HUD's measurement.

## 0.2.3 (Beta)
- The white layer is fully solid (it looked gray) and sits much closer (a thin edge), so the red words are easy to read. The red is fully solid too.

## 0.2.2 (Beta)
- Fixed: "RECEIVING SIGNAL" still showed (in green). The game's animation switches those lines on every frame, so now they just get no text during our message.
- The message gets a white layer behind the red that glitches like the game's magenta copy of "RECEIVING SIGNAL" (same jitter, flicker and opacity, scaled to the bigger letters).

## 0.2.1 (Beta)
- The red message is easier to read: brighter red, about twice as opaque (the game's green is only 43% opaque, too dark in red) and bold.
- The "RECEIVING SIGNAL" line is gone from our message (it comes back for normal signal translator messages).

## 0.2.0 (Beta)
- Just before it starts, the signal translator display opens in red for everyone and types "NO TIME LEFT!" (setting Signal / Message). The "ARMED WITH DEATH" pop-up tips are gone.
- Music: Armed With Death now starts at 70 s left (was 90) with its own music (ArmedWithDeath.ogg next to the DLL), timed so its last hit lands on the meltdown explosion. Everyone hears it; FacilityMeltdown's music fades out under it. Setting Music / Volume.

## 0.1.0 (Beta)
- First build: the trigger (apparatus pull while alone), day clock stop, the ship waits, Armed With Death and its rules for every monster, trap and item (see the README).
- Not yet: the struggle for grabs (Forest Giant, Old Bird, Kidnapper Fox, Masked, SnatchinBracken, Snare Flea), flipper swimming, the apparatus on your back, music.
