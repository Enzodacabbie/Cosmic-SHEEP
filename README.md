# Cosmic-SHEEP
3D space shooter game made by Team Sleepy Sleepless Sheep
Uses Unity version 2021.3.18

November 2nd, 2021

Notes for the Prototype
#################################################################################################################################################################################

These are the notes for the prototype for November 2nd, 2021

The game currently contains most of its main features. These include: rail movement, ship feel, boost and break, shooting, dodge (phases through projectiles),  enviromental obstacles, asteroids (the spheres that home in on you), geometric enemies, shooting patterns for the enemies, respawning

The features that are still currently being worked on/fixed are: lives (we are trying to figure out how to increment the lives through scenes using a static variable), fixing the camera in the boost (it is still very work in progress), fixing player collision (will sometimes collide without dying, which leads to ship spinning into space), adding a few more enemy designs, getting the projectiles to be more accurate, adding sound effects, implementing UI, creating actual levels (this should be simple after we iron out the rest of the issues)

One of the major gameplay features that we had to change was the dodge/roll. Initially, we wanted it to translate to the side while rolling. While we were able to do this, our implementation had inherent flaws for when the player was moving along the rail. We decided to reduce it to a simple roll like in Star Fox that ignores collision on projectiles through the course of the roll. 

Controls:
W : Up
S : Down
A : Left 
D : Right
Q : Roll Left
E : Roll Right
Space: Shoot
Shift: Boost forward
Left Ctrl: Break

Enemies: 
Asteroids : Spheres that move towards the player but slow down when in close proximity to the player
Squares : Move in a square pattern, shoot 4 bullets every few seconds
Triangles : Move in triangle pattern, shoot 3 bullets every few seconds
Hexagon : Do not move, but shoot 6 bullets every few seconds
Square Group : Group of 4 squares that move and shoot like the singular counterpart
Triangle Group : Group of 3 triangles that move and shoot like the singular counterpart 

