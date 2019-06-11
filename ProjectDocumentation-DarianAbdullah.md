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

## Movement/Physics

The platformer uses rigidbodies to determine its physics when objects collide with each other, and uses either box colliders or polygon colliders to determine the exact hitbox of the object in question. Hitboxes were mostly rectangles except for the skeletons which had a pentagonal hitbox to make it easier for the player to hurdle the skeletons.

One of the first changes made was to disable objects from rotating when either colliding with other objects or interacting with the terrain. This was a simple change: we just disabled z-axis rotation in the rigidbody component of the objects that weren't supposed to rotate. The player object was also given a higher mass than the basic mobs so that the player could in principle push the enemies when touching them instead of getting pushed by them.

The basic movement of the player was handled by an ADSR envelope. When moving either left or right, movement starts with a high initial attack curve (much like toad's movement) lasting 0.05 seconds up to a 1.1x multiplier at the peak of a movement "bump," before decaying down to a 1.0x over 0.05 seconds into a constant sustain, and then a linear release over 0.1 seconds to a halt. Since the ADSR envelope doesn't take rigidbody physics into account, the player would often clip a little into the wall and get pushed out by the rigidbody physics. To make this push-out process faster, I increased the baumgarte time of impact scale to 0.75 to speed up the pushout process.

Enemy movement is much simpler by contrast, as each simply moves at a constant speed towards the player when aggro. The exception is the Boss, which has a lerp movement in his second phase. After getting hit by the player, the Boss lerps back to a specified location.

The Boss also has a higher mass than the player so that the player won't be able to easily push the Boss should they come into contact with it.

Another minor addition was pass-through platforms that you could jump up through from below.

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


## Game Feel

With game feel, the hardest part was getting the player control to feel smooth. I had to mess with the values for sustain speed, gravity scale, and jump strength to make basic player control feel good. Along with that, I had to make sure that collisions between the player and enemies felt fair. This is why I made the skeletons have a smaller, non-rectangular hitbox because it made it much easier for the player to hurdle the skeletons. I similarly shrank the player's hitbox so when they get hit it feels fair. For the Boss, I had to tweak the height of his hitbox as before players could shoot fireballs right over his head.

For attacks, I gave the sword swipe attack a kind of bulb-shaped hitbox to better match the shape of the swing. To make aerial attacks feel better and easier to connect, I increased the hitbox size when the player is in the air. The biggest shortcoming of the game feel is that attack hitboxes frequently didn't connect properly for no discernible reason, though it is likely that this could be due to the hitbox flipping when players turn around while attacking.

We briefly had an arena "door" that prevented the player from moving backwards beyond a certain point in phase 2, but we later removed it because the players would get confused when they suddenly couldn't move backwards.
