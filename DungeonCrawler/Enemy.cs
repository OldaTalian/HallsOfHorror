using static DungeonCrawler.Variables;
using static DungeonCrawler.Fight;
using static DungeonCrawler.Sounds;

namespace DungeonCrawler
{
    internal class Enemy
    {
        public static char enemy = '☻';
        public static byte enemyAttack = 10;
        public static byte enemyHealth = 50;

        /// <summary>
        /// Counts all enemies
        /// CZ:
        /// Získá počet všech nepřátelů.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int AllEnemies(char[][] map, char target)
        {
            int count = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == target)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Finds all enemy positons
        /// CZ?
        /// Získá pozice všech nepřátel.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target">enemyType</param>
        /// <returns>int[][] {{X,Y};{X,Y}}</returns>
        public static int[][] FindEnemyPositions(char[][] array, char target)
        {
            int[][] result = new int[AllEnemies(array, target)][];
            int index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == target)
                    {
                        result[index] = new int[] { i, j };
                        index++;
                    }
                }
            }
            return result;
        }

        public static char[] enemyLastTile = new char[AllEnemies(Map, enemy)];

        /// <summary>
        /// Ticks of enemies
        /// CZ:
        /// Updatuje od nepřátelů tiky, každých 5 tiků se posune nepřítel.
        /// </summary>
        public static void enemyTick()
        {
            EnemyAttack(enemy);
            if (currentTick % 5 == 0)
            {
                MoveEnemy(enemy);
            }
            currentTick++;
        }

        /// <summary>
        /// Enemy tries to attack the player
        /// CZ:
        /// Útok na hlavního hráče, podle typu nepřítele.
        /// </summary>
        /// <param name="enemyType"></param>
        public static void EnemyAttack(char enemyType)
        {
            string sidesWithEnemy = "";

            if (CheckForTiles(mainPlayerPos, 'u', enemyType))
            {
                sidesWithEnemy += ('u');
            }
            else
            {
                sidesWithEnemy += ('N');
            }
            if (CheckForTiles(mainPlayerPos, 'd', enemyType))
            {
                sidesWithEnemy += ('d');
            }
            else
            {
                sidesWithEnemy += ('N');
            }
            if (CheckForTiles(mainPlayerPos, 'l', enemyType))
            {
                sidesWithEnemy += ('l');
            }
            else
            {
                sidesWithEnemy += ('N');
            }
            if (CheckForTiles(mainPlayerPos, 'r', enemyType))
            {
                sidesWithEnemy += ('r');
            }
            else
            {
                sidesWithEnemy += ('N');
            }

            if (sidesWithEnemy != "NNNN")
            {
                BeginFight(sidesWithEnemy);
            }
        }


        /// <summary>
        /// Moves the enemy closer to the player (dumn way)
        /// CZ:
        /// Posune nepřítele směrem k hlavnímu hráči.
        /// </summary>
        /// <param name="enemyType"></param>
        public static void MoveEnemy(char enemyType)
        {
            // Find the current position of the player character.
            int[] playerPos = mainPlayerPos;
            int playerX = playerPos[0];
            int playerY = playerPos[1];

            // Loop through all the enemy characters of the specified type on the map.
            for (int i = 0; i < AllEnemies(Map, enemyType); i++)
            {
                int direction = 684665;
                string Debug = ""; // this variable is unused and can be removed.

                // Find the current position of the enemy character.
                int[] enemyPos = FindEnemyPositions(Map, enemyType)[i];
                int enemyX = enemyPos[1];
                int enemyY = enemyPos[0];
                //Console.WriteLine($"Enemy{i}: X{enemyX} Y{enemyY}; Distance {(enemyX - playerX)} {(enemyY - playerY)} Direction:{Debug}{direction} {(Math.Abs(enemyX - playerX) < Math.Abs(enemyY - playerY))}");
                if (IsNear(player, enemyPos, 42))
                {
                    // Check whether the distance between the player character and the enemy character is greater along the X-axis or the Y-axis.
                    if (Math.Abs(enemyX - playerX) > Math.Abs(enemyY - playerY))
                    {
                        Debug = "X";

                        // If the player character is to the left of the enemy character, move the enemy character left.
                        if ((enemyX - playerX) > 0)
                        {
                            direction = -1;
                        }
                        // Otherwise, move the enemy character right.
                        else
                        {
                            direction = 1;
                        }

                        // Check whether the tile to the left or right of the enemy character is available to move into.
                        if (Map[enemyY][enemyX + direction] != '█' && Map[enemyY][enemyX + direction] != player && Map[enemyY][enemyX + direction] != enemyType)
                        {
                            // If the enemy character was previously standing on a tile, replace it with that tile.
                            Map[enemyY][enemyX] =  enemyLastTile[i];

                            // Update the record of the tile the enemy character is now standing on.
                            enemyLastTile[i] = Map[enemyY][enemyX + direction];

                            // Move the enemy character to the new tile.
                            Map[enemyY][enemyX + direction] = enemyType;
                        }
                    }
                    else
                    {
                        Debug = "Y";
                        // If the player character is above the enemy character, move the enemy character up.
                        if ((enemyY - playerY) > 0)
                        {
                            direction = -1;
                        }
                        // Otherwise, move the enemy character down.
                        else
                        {
                            direction = 1;
                        }

                        // Check whether the tile to the left or right of the enemy character is available to move into.
                        if (Map[enemyY + direction][enemyX] != '█' && Map[enemyY + direction][enemyX] != player && Map[enemyY + direction][enemyX] != enemyType)
                        {
                            Map[enemyY][enemyX] = enemyLastTile[i];
                            enemyLastTile[i] = Map[enemyY + direction][enemyX];
                            Map[enemyY + direction][enemyX] = enemyType;
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// Registers all enemies on map
        /// CZ:
        /// Registruje nepřítele.
        /// </summary>
        /// <param name="enemyType"></param>
        public static void RegisterEnemies(char enemyType)
        {
            enemyLastTile = new char[AllEnemies(Map, enemyType)];
            for (int i = 0; i < AllEnemies(Map, enemyType); i++)
            {
                enemyLastTile[i] = '░';
            }
        }
    
        public static void EnterBossFight()
        {
            Console.Clear();
            StopMusic();
            Thread.Sleep(300);

            // display player's health; CZ: životy hráče
            getCursorToCenter(15, false);
            Console.WriteLine($"Your Health: {playerHealth}");
            Thread.Sleep(2000);

            // Display boss introduction
            Console.WriteLine("\n\n\n\n");
            getCursorToCenter(41, false);
            TypeWriterEffect("Boss: You think you can escape from here?", 50);
            Console.WriteLine();
            Thread.Sleep(1000);
            getCursorToCenter(14, false);
            TypeWriterEffect("Think again...", 50);
            Thread.Sleep(2000);

            // Begin fight with boss
            Console.ReadKey();
            PlaySound("assets/fightStart.wav");
            FightInProgress = true;
            enemyFightHealthFull = 250;
            enemyFightHealth = enemyFightHealthFull;
            PlayMusic("assets/bossfightloop.wav");
            PlayerAttack(15);
            if (playerHealth > 0)
            {
                GameWon();
            }
            StopMusic();
            PlayMusic("assets/main_menu.mp3");
            return;
        }
    }
}