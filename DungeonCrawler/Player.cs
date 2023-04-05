using static DungeonCrawler.maps;
using static DungeonCrawler.Variables;
using static DungeonCrawler.Enemy;

namespace DungeonCrawler
{
    internal class Player
    {
        public static void Move() // Define a function to move the player
        {

            int[] playerPos = mainPlayerPos; // Get the current position of the player
            moved = true; // Set a flag indicating that the player has moved

            // Extract the X and Y coordinates of the player
            int x = playerPos[0];
            int y = playerPos[1];

            // Check which arrow key or WASD key was pressed
            switch (Console.ReadKey(true).Key)
            {
                // If the up arrow key or W key was pressed
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    // If there are no obstacles, move the player up
                    if (!CheckForTiles(playerPos, 'u', '█') && !CheckForTiles(playerPos, 'u', '☻'))
                    {
                        MoveUp(x, y); // Move the player up
                    }
                    break;

                // If the down arrow key or S key was pressed
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    // If there are no obstacles, move the player down
                    if (!CheckForTiles(playerPos, 'd', '█') && !CheckForTiles(playerPos, 'd', '☻'))
                    {
                        MoveDown(x, y); // Move the player down
                    }
                    
                    break;

                // If the left arrow key or A key was pressed
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    // Check if the player can move left to the previous room
                    if (CheckForTiles(playerPos, 'l', '\\'))
                    {
                        PrevRoom(); // Move to the previous room
                    }
                    // If there are no obstacles, move the player left
                    else if (!CheckForTiles(playerPos, 'l', '█') && !CheckForTiles(playerPos, 'l', '☻'))
                    {
                        MoveLeft(x, y); // Move the player left
                    }
                    
                    break;

                // If the right arrow key or D key was pressed
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    // Check if the player can move right to the next room
                    if (CheckForTiles(playerPos, 'r', '|'))
                    {
                        NextRoom(); // Move to the next room
                    }
                    // If there are no obstacles, move the player right
                    else if (!CheckForTiles(playerPos, 'r', '█') && !CheckForTiles(playerPos, 'r', '☻'))
                    {
                        MoveRight(x, y);// Move the player right
                    }
                    
                    break;
            }
        }

        public static void MoveUp(int x, int y)
        {
            Map[y][x] = lastStepOn; // Set the tile the player was on to the last tile stepped on
            lastStepOn = Map[y - 1][x]; // Update the last tile stepped on to the one the player is moving to
            Map[y - 1][x] = player; // Set the new tile to the player symbol
        }

        public static void MoveDown(int x, int y)
        {
            Map[y][x] = lastStepOn; // Set the tile the player was on to the last tile stepped on
            lastStepOn = Map[y + 1][x]; // Update the last tile stepped on to the one the player is moving to
            Map[y + 1][x] = player; // Set the new tile to the player symbol
        }

        public static void MoveLeft(int x, int y)
        {
            Map[y][x] = lastStepOn; // Set the tile the player was on to the last tile stepped on
            lastStepOn = Map[y][x - 1]; // Update the last tile stepped on to the one the player is moving to
            Map[y][x - 1] = player; // Set the new tile to the player symbol
        }

        public static void MoveRight(int x, int y)
        {
            Map[y][x] = lastStepOn; // Set the tile the player was on to the last tile stepped on
            lastStepOn = Map[y][x + 1]; // Update the last tile stepped on to the one the player is moving to
            Map[y][x + 1] = player; // Set the new tile to the player symbol
        }

        public static void NextRoom(char tile = '░') // Go to the next room
        {
            ThisRoom++;
            Map = AllRooms()[ThisRoom];
            RegisterEnemies(enemy); // !!! ---- !!! This will have to be changed
            lastStepOn = tile;
        }

        public static void PrevRoom(char tile = '░') // Go to the previous room
        {
            ThisRoom--;
            Map = AllRooms()[ThisRoom];
            lastStepOn = tile;
        }

        public static int[] FindPlayerPos() //  Finds the player on the map and returns their coordinates
        {
            int[] playerPos = { -1, -1 };
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)
                {
                    if (Map[i][j] == player)
                    {
                        playerPos[0] = j;
                        playerPos[1] = i;
                        return playerPos;
                    }
                }
            }
            return playerPos;
        }
    }
}