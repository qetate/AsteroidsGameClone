# Asteroids Game Clone

## Objective
This project is a clone of the classic Asteroids game. 

## Technologies Used
- **Unity**: Game development engine.
- **C#**: Programming language used for scripting game mechanics.

## Skills Demonstrated
- **C# Programming**: Developed game mechanics using C# scripts.
- **Object-Oriented Programming (OOP)**: Applied OOP principles such as encapsulation, inheritance, and polymorphism in designing game objects and their behaviors.
- **Game Development**: Created a fully functional game.
- **Unity Physics**: Utilized Unity's physics engine to handle movement and collisions.
- **Procedural Generation**: Implemented dynamic spawning and splitting of asteroids.
- **User Interface Design**: Designed and implemented game UI elements.

## Features
- **Player Control**: Move and rotate the spaceship, and shoot bullets to destroy asteroids.
- **Asteroid Generation**: Randomly sized and directed asteroids spawn and split upon destruction.
- **Screen Wrapping**: The player and asteroids wrap around the screen edges.
- **Score and Lives**: Track the player's score and lives, with game-over conditions and respawn functionality.
- **Explosion Effects**: Visual effects for asteroid destruction and player death.

## Key Scripts Summary

### Player.cs
Handles player movement, shooting, screen wrapping, and collision detection.

### Asteroid.cs
Manages asteroid behavior, including random properties, movement, and splitting upon collision.

### AsteroidSpawner.cs
Controls the spawning of asteroids at regular intervals, with random sizes and trajectories.

### Bullet.cs
Defines the behavior of bullets shot by the player, including movement and collision detection.

### GameManager.cs
Manages the game state, including score, lives, player respawn, and game-over conditions.

## How to Play
- **Movement**: Use W/Up Arrow to thrust forward, A/Left Arrow to rotate left, and D/Right Arrow to rotate right.
- **Shooting**: Press Space or left-click to shoot bullets.
- **Objective**: Destroy asteroids to increase your score. Avoid collisions to preserve your lives.

## Credits
- [How to make Asteroids in Unity (Complete Tutorial)](https://youtu.be/cIeWhztKyAg?si=OfUJ7hO332W0JQRO)