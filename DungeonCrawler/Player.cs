using static DungeonCrawler.maps;
using static DungeonCrawler.Variables;
using static DungeonCrawler.Enemy;

namespace DungeonCrawler
{
    internal class Player
    {
        /// <summary>
        /// The function that handles the player move
        /// CZ:
        /// Pohyb hráče
        /// </summary>
        public static void Move() 
        {
            int[] playerPos = mainPlayerPos; 
            moved = true;

            // Extract the X and Y coordinates of the player; CZ: Získá X,Y z pozice hráče
            int x = playerPos[0];
            int y = playerPos[1];

            // Main CONTROLS; CZ: Ovládání hry
            switch (Console.ReadKey(true).Key)
            {
                // If the up arrow key or W key was pressed; CZ: Nahoru
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    // If there are no obstacles, move the player up; CZ: Zjistí jestli nejsou nad hráčem nějáké zábrany
                    if (!CheckForTiles(playerPos, 'u', '█') && !CheckForTiles(playerPos, 'u', '☻') && !CheckForTiles(playerPos, 'u', 'ł'))
                    {
                        MoveUp(x, y); 
                    }
                    else
                    {
                        moved = false;
                    }
                    break;

                // If the down arrow key or S key was pressed; CZ: Dolů
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    // If there are no obstacles, move the player down; CZ: Zjistí jestli nejsou pod hráčem nějáké zábrany
                    if (!CheckForTiles(playerPos, 'd', '█') && !CheckForTiles(playerPos, 'd', '☻') && !CheckForTiles(playerPos, 'd', 'ł'))
                    {
                        MoveDown(x, y);
                    }
                    else
                    {
                        moved = false;
                    }
                    break;

                // If the left arrow key or A key was pressed; CZ: Doleva
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    // Check if the player can move left to the previous room; CZ: Zjistí jestli hráč se může pohnout do minulé místnosti
                    if (CheckForTiles(playerPos, 'l', '\\'))
                    {
                        PrevRoom(); 
                    }
                    // If there are no obstacles, move the player left; CZ: Zjistí jestli nalevo od hráče nejsou nějáké zábrany
                    else if (!CheckForTiles(playerPos, 'l', '█') && !CheckForTiles(playerPos, 'l', '☻') && !CheckForTiles(playerPos, 'l', 'ł'))
                    {
                        MoveLeft(x, y);
                    }
                    else
                    {
                        moved = false;
                    }
                    break;

                // If the right arrow key or D key was pressed; CZ: Doprava
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    // Check if the player can move right to the next room; CZ: Zjistí jestli hráč se může pohnout do další místnosti
                    if (CheckForTiles(playerPos, 'r', '|'))
                    {
                        NextRoom();
                    }
                    // If there are no obstacles, move the player right; CZ: Zjistí jestli napravo od hráče nejsou nějáké zábrany
                    else if (!CheckForTiles(playerPos, 'r', '█') && !CheckForTiles(playerPos, 'r', '☻') && !CheckForTiles(playerPos, 'r', 'ł'))
                    {
                        MoveRight(x, y);
                    }
                    else
                    {
                        moved = false;
                    }
                    break;
                case ConsoleKey.F3:
                    // Toggles Debug
                    showDebug = !showDebug;
                    break;
                default: moved = false; break;
            }
        }

        /// <summary>
        /// Moves the player Up
        /// CZ:
        /// Posune hráče nahoru
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MoveUp(int x, int y)
        {
            Map[y][x] = lastStepOn; 
            lastStepOn = Map[y - 1][x]; 
            Map[y - 1][x] = player;
        }

        /// <summary>
        /// Moves the player Down
        /// CZ:
        /// Posune hráče dolů
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MoveDown(int x, int y)
        {
            Map[y][x] = lastStepOn;
            lastStepOn = Map[y + 1][x];
            Map[y + 1][x] = player;
        }

        /// <summary>
        /// Moves the player to Left
        /// CZ:
        /// Posune hráče doleva
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MoveLeft(int x, int y)
        {
            Map[y][x] = lastStepOn; 
            lastStepOn = Map[y][x - 1]; 
            Map[y][x - 1] = player; 
        }

        /// <summary>
        /// Moves the player to Right
        /// CZ:
        /// Posune hráče doprava
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MoveRight(int x, int y)
        {
            Map[y][x] = lastStepOn; 
            lastStepOn = Map[y][x + 1]; 
            Map[y][x + 1] = player; 
        }

        /// <summary>
        /// Moves the player to the next room
        /// CZ:
        /// Posune hráče do další místnosti
        /// </summary>
        /// <param name="tile">last tile</param>
        public static void NextRoom(char tile = '░') 
        {
            ThisRoom++;
            playerHealth = defaultPlayerHealth; //heal player
            Map = RandomMaps()[ThisRoom];
            RegisterEnemies(enemy); // bad
            lastStepOn = tile;
        }

        /// <summary>
        /// Moves the player to the previous room
        /// CZ:
        /// Posune hráče do minulé místnosti
        /// </summary>
        /// <param name="tile">last tile</param>
        public static void PrevRoom(char tile = '░') 
        {
            ThisRoom--;
            Map = RandomMaps()[ThisRoom];
            lastStepOn = tile;
        }

        /// <summary>
        /// Finds the player cordinates on the Map
        /// CZ:
        /// Nalezne souřadnice hráče
        /// </summary>
        /// <returns>int {X,Y}</returns>
        public static int[] FindPlayerPos() 
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