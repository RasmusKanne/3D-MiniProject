# 3D-MiniProject
Repository for making a Unity project for the course "Programming of Interactive 3D Worlds" on third semester at AAU

![Visual](Images/3D_Gif1.gif)

## Overview of the Game
The idea of the project is of a stealth game centered around the player being a small mouse in a big scary city. The objective of the game is to collect enough cheeses without dying by getting caught by the enemies or falling into traps. This is done by using the environment to stay out of sight and climbing objects to reach dangerous areas. Cheeses are easy to find at first but gets harder afterwards where the player must get in precarious situations to get the final cheeses.

The main parts of the game are:
-	Player - Mouse, moved by the player via WASD or arrow keys
-	Camera - Third person camera rotated with the mouse and decides the direction of the player characters movement
-	Cheese - The main goal is to collect enough cheeses which are scattered around the game’s environment
-	Enemies - Large enemies patrol the streets on specific routes searching for the player, where the player must avoid their large moving spotlights or they will attack
-	Environment - The mouse is small in this world, which changes your perspective. Use the environment to avoid the lights and stay out of vision of the enemies.
-	Win / Lose - The player wins by collecting at least 10 cheeses and returns to their mousehole, without getting caught and losing all their three lives

Game features:
- Stealth system gives the player opportunity to decide their own path and makes use of the environment
- The darkness stark contrasting spotlights makes it easy to distinquish light from dark
- See how many of the cheeses you can get, some more difficult than others

## Running It
1. Download Unity >= 2021.3.11f1
2. Clone or Download the project 
3. The game requires a computer with a mouse and keyboard

## Project Parts

### Scripts
- PlayerMovement - Makes the player move using RigidBody physics by user input
- PlayerCamera - Controls the third person cinemachine camera making the player rotate with the camera’s transform
- PlayerStats - Controls player interaction with the environment and holds track of player statistics like health and objectives
- PatrollingEnemy - Uses NavMesh to move the enemy to search for the player, by making the enemy follow defined patrol points and attack the player if they get in the enemy’s field of vision
- UIController - Controls all the UI elements via functions called in other scripts

### Models & Prefabs
- All models and prefabs created using the in engine ProBuilder

| **Task**                                                                | **Time it Took (in hours)** |
|--------------------------------------------------------------------------------|------------------------------------|
|     Ideation (Sketching / Brainstorming)										 |     2                              |
|     Setting up the Project (Unity and GitHub)									 |     1                              |
|     Writing Fundamental Scripts												 |     7.5                            |
|     -	Movement															     |     - 2							  |
|     -	Camera																	 |     - 1                            |
|     -	Objectives (Pick-Ups)                                                    |     - 0.5                          |
|     -	Enemies Navigation (FOV detection)										 |     - 4                            |
|     Creating the Environment													 |     8                              |
|     -	Layout																	 |     - 2                            |
|     -	Lighting																 |     - 1                            |
|     - 3D Models Using ProBuilder						                         |     - 3                            |
|     - Populating the Environment										         |     - 3                            |
|     User Interface (UI)													     |     2                              |
|     Animations (Movement, Cheese Pick Ups)                                     |     2                              |
|     Code Documentation                                                         |     0.5                            |
|     Making readme                                                              |     0.5                            |
|     **All**                                                                        |     **24.5**                           |

## References
- [THIRD  PERSON MOVEMENT in 11 MINUTES – Unity Tutorial – Dave / GameDevelopment](https://youtu.be/UCwwn2q4Vys )
- [Patrolling AI in Unity with C# - Lofi Dev](https://youtu.be/c8Nq19gkNfs )
- [How to Add a Field of View for Your Enemies [Unity Tutorial] – Comp-3 Interactive:](https://youtu.be/j1-OyLo77ss ) 




