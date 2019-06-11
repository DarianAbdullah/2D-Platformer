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

## Movement/Physics - Cameron Brown

The platformer uses rigidbodies to determine its physics when objects collide with each other, and uses either box colliders or polygon colliders to determine the exact hitbox of the object in question. Hitboxes were mostly rectangles except for the skeletons which had a pentagonal hitbox to make it easier for the player to hurdle the skeletons.

One of the first changes made was to disable objects from rotating when either colliding with other objects or interacting with the terrain. This was a simple change: we just disabled z-axis rotation in the rigidbody component of the objects that weren't supposed to rotate. The player object was also given a higher mass than the basic mobs so that the player could in principle push the enemies when touching them instead of getting pushed by them.

The basic movement of the player was handled by an ADSR envelope. When moving either left or right, movement starts with a high initial attack curve (much like toad's movement) lasting 0.05 seconds up to a 1.1x multiplier at the peak of a movement "bump," before decaying down to a 1.0x over 0.05 seconds into a constant sustain, and then a linear release over 0.1 seconds to a halt. Since the ADSR envelope doesn't take rigidbody physics into account, the player would often clip a little into the wall and get pushed out by the rigidbody physics. To make this push-out process faster, I increased the baumgarte time of impact scale to 0.75 to speed up the pushout process.

Enemy movement is much simpler by contrast, as each simply moves at a constant speed towards the player when aggro. The exception is the Boss, which has a lerp movement in his second phase. After getting hit by the player, the Boss lerps back to a specified location.

The Boss also has a higher mass than the player so that the player won't be able to easily push the Boss should they come into contact with it.

Another minor addition was pass-through platforms that you could jump up through from below.

## Animation and Visuals - Darian Abdullah

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

## Input - Neil Natekar

We decided to let the players be able to use PS4 controllers or a mouse and keyboard. Although an Xbox controller can be used too, the game was not mapped for an Xbox controller. 

PS4 controls:
* Left joystick for movement
* X button to jump
* Square button to attack
* Triangle button to use a fireball
* Options button as enter (to move through the intro scenes)

Mouse and PC controls:
* WASD or arrow keys for movement
* Space for jump
* Left control for attack
* Left alt for fireball
* Enter/return for enter

## UI - Heping Lin

The first UI that was implmented was the HP bar. It was implemented using the UI Slider and some scripting to control it. The HP bar script references the hero in order to call the GetHealth() function from HeroController in order to update the bar. The HP bar is directly related to the heath mechanic that is implemented as a part of gameplay and is visually how the player interact with the health system when the player character isn't dead. 

The second UI that was implemented was the game over panel that shows up when the player dies from having no more health. The gameover screen says that the player has died and gives the player a button that restarts the level that they died in. This UI is the other way for players to interact with HP or the lack of it. The restart button is to allow the player to play the game more with ease, which we absolutely want. 

The 3rd UI that was implemnted was the Magic Bar that slowly fills up over time.This is similar to the HP bar except the Magic bar also had a fireball sprite attached to it that when charging is darkened and when ready to use is returned to its original color. This allows the player to interact with magic cooldown system that is within the game.

The final UI that was implemented was the Game Won screen. This screen is tied to the final boss. It is a UI panel with text that appears under a certain condition. When the final boss is defeated by the player this screen will show up congratulating the player for finishing the game. This is to also give the player a sense achievement by giving the player something tangible for beating the game.

## Game Logic - Shayan Mandegarian

Game Logic constitutes the back bone of the game, and mainly consisted of controlling the player, the enemies, how they interact with each other, and attacking. 

First, the HeroController.cs file is the main script for the player other than movement. This script contains all the variables that other ones rely on, such as flags for [ground](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/HeroController.cs#L12), which the jump animation is linked to. In addition, this script contains all of Hero's hit detection, including [how much damage](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/HeroController.cs#L158) to take depending on what enemy was hit, and [how much knockback](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/HeroController.cs#L171) too. Lastly, this script handles [Hero's death](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/HeroController.cs#L112), and calls SwordAttack.cs when the player [clicks their mouse](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/HeroController.cs#L77).

SwordAttack.cs uses the [IHeroCommand interface](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/IHeroCommand.cs#L6) which is derived from assignment 1's ICaptainCommand. The SwordAttack.cs script is also derived from assignment 1's motivation command, in that it activates a hitbox depending on [the direction player is facing](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/SwordAttack.cs#L32) or if they are in the air, then [detects contacts with the hitbox](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/SwordAttack.cs#L45), then depending on the tag of said contacts, calls their "hit" and "knockback" functions, like this [skeleton example](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/SwordAttack.cs#L53).

Enemy controllers, SkeletonController.cs, HoundController.cs, and FireSkullController.cs are all similar to each other.
Originally, I planned on using a "pub sub" design pattern like in assignment 3, but I determined that the number of enemies is small enough that it wouldn't be necessary. They all have "hit" and "knockback" functions (FireSkull doesn't have knock since it is stationary), [audio sources](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/SkeletonController.cs#L32), and the unique [movement](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/SkeletonController.cs#L58) for each of them.

Finally, the BossController.cs is the most complex of the enemy controllers. Most notably, the boss has [6 "phases"](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/BossController.cs#L21) that determine what the boss does. First, the boss waits for the player to approach, then runs to its position while shooting fireballs at the player (which uses [BossFireBallAttack.cs](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/BossController.cs#L51)), then switches back and forth between standing in place shooting fireballs, and approaching the player for a physical attack. Once the boss is killed, the game is won!

# Sub-Roles #

## Audio - Shayan Mandegarian ##
Various Fallout 1 songs from the official soundtrack, found [here](https://www.youtube.com/playlist?list=PLC0C2A6BCA6040BC8). These are placeholder songs that fit the tone the game is going for, and down the line original music in the same vein would be made instead. These songs are uniquely atmospheric, creepy, yet aesthetically pleasing all at once and fit the demonic theme of the game well. The implementation for the background music is simple, the camera in each scene has an audio source with that level's song in it, which plays on awake and loops.

The rest of the sound effects are sourced from [freesound.org](freesound.org), and all have creative commons licenses. 
Here is a list of each one can be found here: https://pastebin.com/Shepv8eH

The sounds are a mix of 8-bit sounds and some real life recordings. The main point is that the sound effects are simple, short, and non-intrusive. They are meant to just provide some satisfying sounds for player actions, and hurting/killing enemies. For the walking sound effects, events were placed on certain frames of the animations that called functions to play the effects, like [this example](https://github.com/DarianAbdullah/2D-Platformer/blob/02d968831f0d514bdc79cea64f70e81ef3acb534/Assets/Scripts/HeroController.cs#L237) in HeroController.cs. The rest of the sounds were added by adding multiple audio sources to the hero and enemy gameObjects, putting them all into arrays, then calling them based on their index when certain events happen.

## Gameplay Testing - Neil Natekar
A link to the results of the gameplay testing can be found [here](https://docs.google.com/document/d/1CDihAAKO6T8KLSo7eaiZjI9XjTiTHILCe1eM3EauQVI/edit?usp=sharing). 

Overall, it seemed many players were slightly confused by the Xbox controllers but got used to it quickly. Unfortunately, was meant to be played with a PS4 controller or a mouse and keyboard, but due to a miscommunication no mice or PS4 controllers were available during the gameplay testing. Therefore, we cannot be too sure whether the controls still felt off, though they felt great when the team was testing.
  
Almost all players seemed to enjoy the art, animations, and narration. One player said the narration was unoriginal since the story is similar to John Wick, but our intent was to make a parody.
  
Some players noticed the hitboxes of the swords were off, and because of this we found that the sword does not hit the enemies if the player turns around right after attacking, so we addressed this issue. We also increased the hitbox size.

Many players seemed to struggle with the final boss, so we made it a bit easier to defeat. However, as a result, some players were able to kill the boss too quickly, so some of the boss’s mechanics never triggered.
  
Many players encountered a bug with the hound; whenever the player would try to jump over the hound but instead jump on top of it, the player would get stuck over the hound, and the hound’s player chasing would glitch out, causing the hound to flip over or rotate around the map. We would not have found this bug without testing as none of the team members jumped over hounds. We managed to fix this bug. 

## Narrative Design - Darian Abdullah 

The narrative is first present by the five intro scenes that I created. I wanted to go with a dark story to fit with a dark, metroidvania aesthetic. I believe that the assets that I chose and the story are both dark.
I used the background of level 1, the blood moon, in the intro cards to set the transition from the intro to the main game. As explained earlier, I thought it would be a good idea to start the player in the cemetery because
of the mention of the main characters dead wife in the intro, and adding a giant demon boss at the end of level 2 because of the main characters goal to kill all demons.


## Game Feel - Cameron Brown

With game feel, the hardest part was getting the player control to feel smooth. I had to mess with the values for sustain speed, gravity scale, and jump strength to make basic player control feel good. Along with that, I had to make sure that collisions between the player and enemies felt fair. This is why I made the skeletons have a smaller, non-rectangular hitbox because it made it much easier for the player to hurdle the skeletons. I similarly shrank the player's hitbox so when they get hit it feels fair. For the Boss, I had to tweak the height of his hitbox as before players could shoot fireballs right over his head.

For attacks, I gave the sword swipe attack a kind of bulb-shaped hitbox to better match the shape of the swing. To make aerial attacks feel better and easier to connect, I increased the hitbox size when the player is in the air. The biggest shortcoming of the game feel is that attack hitboxes frequently didn't connect properly for no discernible reason, though it is likely that this could be due to the hitbox flipping when players turn around while attacking.

We briefly had an arena "door" that prevented the player from moving backwards beyond a certain point in phase 2, but we later removed it because the players would get confused when they suddenly couldn't move backwards.

## Press Kit and Trailer - Heping Lin

Press Kit and Trailer are in the repository.

The game trailer was recorded using OBS. What was recorded was the entire playthrough from beggining to beating the final boss. That footage was then edited to show parts of the game which I felt was important. The first scene shows fighting a hell hound and some rudimentary platforming which was put there as a way to pull in the audience. The second scene shows off the castle level and the skeleton enemies then we get to the boss. Some fireball action was also shown in the trailer to show that it is part of the gameplay. The final scene of the trailer is shows me beating the final boss with a fireball. This is to showcase what the player can expect from the game. 

The Press Kit contains basci info on the game. It starts off with a fact section that shows off facts which I felt was relevant. For example who was in charge of which main roles or when the game was "completed". Then comes a game description section which then leads to another section that describes how to play the game. This is so that whoever is interested in covering the game will know what they are getting into. Finally the press kit ends by showing off art from the game's assets. This is to generate interest toward the game.  
