# Game Basic Information #

## Summary ##

In Vassal Cania, the player controls an anguished demon slayer, thirsty for revenge. The player starts off at a cemetary, paying respects to his dead wife before he embarks on his quest to slay all demons.
The game is intended to be challenging, but not overwhelmingly difficult. The game currently featurs four different types of monsters, skeletons, Hell hounds, fire skulls, and a demon. Do you have what it
takes to purge the world from demons?

## Gameplay explanation ##

For keyboard and mouse, A and D controls left and right movement, spacebar controls the jump, left mouse button controls attacks, and the right mouse button controls casting a fireball.
The optimal way to play the game is to release an attack and try to "kite" backwards to avoid being hit while waiting for attack cooldown to wear off. If you turn around too fast after you
attempt an attack, your attack will be canceled and you will not do any damage. I recommend to cast your fireball every time it's available (a five second cool down). This ensures maximum
damage and the fireballs take no resources away from you.

# Main Role #

## Animation and Visuals

[Gothicvania Pateron's Collection](https://ansimuz.itch.io/gothicvania-patreon-collection) and [Gothicvania Cemetery](https://ansimuz.itch.io/gothicvania-cemetery) - 
License (both asset packs include the same license): 
"Artwork created by Luis Zuno @ansimuz

This is an old collection (2016-17) of Pixel Art Assets from my patreon page https://www.patreon.com/ansimuz

License for Everyone. 

Public domain and free to use on whatever you want, personal or commercial. Credit is not required but appreciated."



We wanted to go for a metroidvania style game with crisp movements. To have crisp movements, you must have crisp animations. I implemented the player's animation controller to have clean transitions
to every state that he can be in (idle, running, jumping, jump attacking, attacking, and hurt). I set the animations to be quite fast and the result is seamless transitions in most movement choices. 
The jump animation is 7 frames in total. I set it to where when the player jumps, it will play the first five frames. After the jump is over, it transitions to jump_end, which plays the last two frames of the jump.
I made this choice to ensure that jumping does not loop while in the air. 

I also set up all of the animations and animation controllers for all of the enemies in the game. 

To fit the metroidvania aesthetic, I found a collection of sprites that I felt were perfect for our choice.

I designed all of level 1 and part of level 2 to go with the design choice of our game. I also created the menu and the intro, which I felt like fit the story well. I chose to start the player in the cemetery
as a nod to paying respects to the main characters dead wife. 

To help distinguish if an enemy has gotten hit by the player, I added a red flash that is a material placed on the enemy that displays for 0.1 seconds, which I believe gives the player a good feel of when they inflict damage.
To let the player know when an enemy is dead, I set up the animation controller on all enemies to play a death animation. 

To help balance out the work out, I helped create the FireSkullController script, added the transition from level 1 to level 2 by creating the script NextLevel, and helped with some gameplay design changes.


## Narrative Design

The narrative is first present by the five intro scenes that I created. I wanted to go with a dark story to fit with a dark, metroidvania aesthetic. I believe that the assets that I chose and the story are both dark.
I used the background of level 1, the blood moon, in the intro cards to set the transition from the intro to the main game. As explained earlier, I thought it would be a good idea to start the player in the cemetery because
of the mention of the main characters dead wife in the intro, and adding a giant demon boss at the end of level 2 because of the main characters goal to kill all demons.
