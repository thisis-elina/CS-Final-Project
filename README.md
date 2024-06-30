# C# Final Project
 Project submission for Igor and Elina


# Sokoban Console Game

## Table of Contents
1. [Introduction](#introduction)
2. [Game Rules](#game-rules)
3. [Controls](#controls)
4. [Installation](#installation)
5. [How to Play](#how-to-play)
6. [Levels](#levels)
7. [License](#license)

## Introduction
Welcome to Sokoban Game! This is a classic puzzle game where you need to push crates to designated storage locations in a warehouse. The challenge is to complete each level with the fewest possible moves.

## Game Rules
1. The player can move up, down, left, or right using arrow keys ⬆️ ⬇️ ⬅️ ➡️.
2. The player pushes crates but cannot pull them.
3. The goal is to move all crates to the designated storage locations.
4. The game is won when all crates are on the storage locations.

## Controls
- Use the arrow keys ⬆️ ⬇️ ⬅️ ➡️  to move your character.
- Press Ctrl + Z to undo a move ↩️.
- Ctrl + Y to redo a move ↪️.
- Press Ctrl + S to save your progress 💾.
- Press Ctrl + R to reload the current level 🔄.
- Press Ctrl + X to close the game ❌.

## Installation
1. Ensure you have .NET installed on your machine. You can download it from the [.NET download page](https://dotnet.microsoft.com/download).
2. Clone the repository:
   ```bash
   git clone https://github.com/thisis-elina/CS-Final-Project
   ```
3. Navigate to game directory:
   ```bash
   cd game
   ```
4. Build the project:
   ```bash
   dotnet build
   ```
5. Run the game:
   ```bash
   dotnet run
   ```
## Levels
The game includes multiple levels with varying difficulty. Each level is a new puzzle where you need to strategically push crates to their designated spots. Levels are stored in a `Setup.json` file and can be customized or expanded by editing this file.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

Enjoy playing Sokoban Game!