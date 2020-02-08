Portal Mario
============

![Mario Game Demo](https://media.giphy.com/media/SS2gAEiLehqv6DpKVw/giphy.gif)

Portal Mario was developed using the MonoGame game engine. The game was developed during
a semester long project course at the Ohio State Univerity amongst myself and 3 other students. 
For the project we strived to do a faithful remake of the original Mario game on the NES but with 
a slight twist in the mechanics. Outside of the regular game so many people are familiar with, 
we decided to imnplement the Portal gun, teleportation physics, and Companion Cubes from the 
popular game Portal by Valve.

Though this game is a remake of the NES Mario title, it does not implement every level. So, Portal
Mario allows the player to traverse only through world 1-1 from the original Mario game.

Portal Gun
==========

![Portal Gun Demo](https://media.giphy.com/media/h8177w3g42RDQ13JUu/giphy.gif)

To find the Portal gun, the second pipe in the game will allow the player to enter a puzzle area with
the gun inside of a question mark block. In the puzzle area, players will need to utilize portals
to retrive Companrion Cubes that will open up gates via their corresponging switch or switches. Though
portals can be shot onto a variety of obstacles in the environemt, there are some blocks that will not
allow portals to spawn.

Although the game is a single level, the Portal gun opens up more opportunity to experiment with
the physics, enemies, puzzles, and power-up projectiles.

Costume & Power-up System
==============

![Costume Selection](https://media.giphy.com/media/fAJk8YBNmMJcq5cfzC/giphy.gif)

To give the player a variety of power-ups in the game, we developed a costume system where each 
costume has a unique power-up projectile, melee attack, or both projectile and melee attacks.
There are 24 costumes in the game which are Caveman, Chef, Diddy Kong, Doctor, Bride's Dress,
Football, Gold Mario, King, Knight, Luigi, Mechanic, Metal Mario, Musician, Pirate, Samurai, 
Scientist, Skeleton, Snow Suit, Space Suit, Swim Wear, Fire Mario, Baseball, Resort, and Black Tuxedo.

When the game starts, it will randomly choose one of the 24 power-ups that will fill the question
blocks. However, the player can choose the power-up by pausing the game with "P", then using the
arrow keys to navigate to their desired power-up.

Portal Mario Windows Build
===================

Apart from the source code in the project files, there is a build of the game in the 
"Portal Mario Build.zip" file. When the zip file is extraced, there is an executable named
"MarioGame" that will launch the game. 

Game Controls
===========

## Mario Controls  

* W, Up Arrow, or Spacebar - Jump
* A or Left Arrow - Move left
* D or Right Arrow - Move Right
* S or Down Arrow - Crouch when Big Mario or go down pipe
* F - Spit projectile when using a power-up
* R - Perform melee attack when using a power-up

## Portal Mechanics

* Cursor - Use mouse cursor to aim Portal Gun, Hover over Companion Cubes
* E - Pick up Companion Cubes when mouse curor hovers over it. Drop when cursor is off of Companion Cube.
* Left Click - Shoot blue portal
* Right Click - Shoot orange portal

## Gameplay Controls

* Q - Quit Game
* P - Pause Game
* M - Mute Game
* C - Render hitboxes when not paused
* Tilde - Reset level when not paused

## Pause Screen Controls

* F - Fullscreen mode
* A - Navigate to prior costume
* D - Navigate to next costume

## Cheat Codes

* Y - Go to Small Mario
* U - Go to Big Mario
* I - Go to Power-up Mario
* O - Cause damage to Mario
* G - Invincibility mode
* 8 - Go to win screen
* 9 - Go to game over screen
