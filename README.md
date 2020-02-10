# BAABBEB

Made in Unity.

WIP roguelike that requires players to quickly memorize procedurally-generated level layouts before the screen goes black.

Two level themes, using different generation algorithms. Each level is guaranteed to be solveable, and generates in O(n) time, relative to the number of game objects.

AI-controlled enemies use BFS-based pathfinding to pursue the player. 

Sound effects made in famitracker respond to player performance. 

Controls:

"m": Generate and play ocean level.

"o": Generate and play ocean level.

Left and right keys change direction to move on clock tick.

Down key counters enemies entering player's next space on clock tick. Using the counter when there are no enemies damages the player.



