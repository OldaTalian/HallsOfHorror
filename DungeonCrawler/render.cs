using static DungeonCrawler.maps;
using static DungeonCrawler.Variables;
using static DungeonCrawler.Player;

namespace DungeonCrawler
{
    internal class Render
    {
        /// <summary>
        /// Main rendering function
        /// CZ:
        /// Rendrování hlavní hry
        /// </summary>
        public static void RenderScreen()
        {
            Console.Clear();
            if (Map == null) return; // Dont try to render when there is no map; CZ: Nerendrovat, když není mapa
            getCursorToCenter(34, false);
            Console.WriteLine("Rooms: " + (ThisRoom) + "/" + (AllRooms().Length - 1) + " |  x:" + mainPlayerPos[0] + " y:" + mainPlayerPos[1] +
                " Health: " + ((playerHealth > 0) ? playerHealth : 0)); //DEBUG zpráva

            if(Variables.OperatingSystem == "win11")
            {
                for (int i = 0; i < Console.BufferHeight / 2 - (Map.Length / 2 + 1); i++)
                {
                    Console.WriteLine();
                }
            }
            // Tryies to find mark of Fog rendering; CZ: Zjistí jestli se má rendrovat fog of war
            int maxDistance;
            if (Map[Map.Length - 1][Map[Map.Length - 1].Length - 2] == 'Đ' &&
                int.TryParse(Map[Map.Length - 1][Map[Map.Length - 1].Length - 1].ToString(), out maxDistance))
            {
                RenderCircle(maxDistance);
            }
            else
            {
                // Default rendering; CZ: Normální rendrování
                for (int i = 0; i < Map.Length; i++)
                {
                    getCursorToCenter(Map[1].Length, false);
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        convertToMap(Map, j, i);
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Returns an array of tiles between the start and end points, inclusive, on a line defined by those points.
        /// using Bresenham's algorithm.
        /// CZ:
        /// Pomocí Bresenhamova algoritmu vrátí pole dlaždic mezi počátečním a koncovým bodem včetně na řádku definovaném těmito body.
        /// </summary>
        /// <source>https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm</source>
        /// <param name="startPos">Starting position {X,Y}</param>
        /// <param name="endPos">Ending position {X,Y}</param>
        /// <param name="maxTiles">Max lenght</param>
        /// <returns></returns>
        public static HashSet<int[]> GetTilesOnLine(int[] startPos, int[] endPos, int maxTiles)
        {
            HashSet<int[]> tiles = new HashSet<int[]>(); // An array of INT that removes duplicates
            int x = startPos[0];
            int y = startPos[1];
            int dx = Math.Abs(endPos[0] - startPos[0]);
            int dy = Math.Abs(endPos[1] - startPos[1]);
            int xStep = (endPos[0] > startPos[0]) ? 1 : -1;
            int yStep = (endPos[1] > startPos[1]) ? 1 : -1;
            int error = dx - dy;
            int i = 0;

            while (x != endPos[0] || y != endPos[1])
            {
                if (i >= maxTiles)
                {
                    break;
                }

                if (x >= 0 && x < Map[0].Length && y >= 0 && y < Map.Length)
                {
                    tiles.Add(new int[] { x, y });
                    i++;
                }

                int error2 = error * 2;
                if (error2 > -dy)
                {
                    error -= dy;
                    x += xStep;
                }
                if (error2 < dx)
                {
                    error += dx;
                    y += yStep;
                }
            }

            return tiles;
        }

        public static bool skipNextOne = false;

        /// <summary>
        /// Converts map to chars that the player can see
        /// CZ:
        /// Konvertuje charakter z mapy na charaktery z které vidí hráč.
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void convertToMap(char[][] Map, int x, int y)
        {
            if (!skipNextOne)
            {
                if (Map[y][x] == '░' || Map[y][x] == '▒') // floor; CZ: podlaha
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(Map[y][x]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (Map[y][x] == '$') // walk through wall; CZ: průchodná zed
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == '{') // open lever; CZ: otevřená páčka 
                {
                    Console.Write("O"); 
                }
                else if (Map[y][x] == '}') // closed lever; CZ: zavřená páčka
                {
                    Console.Write("X");
                }
                else if (Map[y][x] == 'ł') // closed lever door; CZ: zavřené dveře na páčku
                {
                    Console.Write("#");
                }
                else if (Map[y][x] == 'Ł') // open lever door; CZ: otevřené dveře na páčku
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == '#') // magma; CZ: láva
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("#");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == ' ') // void; CZ: díra
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == '☻') // enemy; CZ: zloduch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("☻");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (Map[y][x] == 'Đ') // Mark for fog; CZ: Jestli se nachází v uložené mapě, mapa je značena jako fog of war.
                {
                    skipNextOne = true; // skips the next char (number of distance); CZ: Přeskočí následující char (číslo vzdálenosti mlhy)
                }
                else // everything else; CZ: ostatní dlaždice
                {
                    Console.Write(Map[y][x]);
                }
            }
            else
            {
                Console.Write(""); // if skip NOT RENDER ANYTHING; CZ: pokud skipNextOne nerenderuj
                skipNextOne = false;
            }
        }
        /// <summary>
        /// Renders the fog around the player
        /// CZ:
        /// Rendruje mapu v určitém radiusu okolo hráče.
        /// Pracuje jako 2D fog of war.
        /// </summary>
        /// <param name="maxDistance">radius</param>
        public static void RenderCircle(int maxDistance)
        { 
            int[] playerPos = mainPlayerPos;
            for (int i = 0; i < Map.Length; i++)
            {
                getCursorToCenter(Map[1].Length , false);
                for (int j = 0; j < Map[i].Length; j++)
                {
                    int[] tilePos = { j, i };
                    int distance = MeasureDistance(playerPos, tilePos);

                    // Calculates the radius of the circle; CZ:  Vypočítá poloměr kruhu, který by měl být nakreslen kolem pozice hráče
                    int radius = maxDistance - distance + 3;
                    if (maxDistance > distance)
                    {
                        // Checks if the tile is in the circle; CZ: Zkontrolujte, zda je dlaždice v kruhu
                        if (radius * radius >= Math.Pow(playerPos[0] - tilePos[0], 2) + Math.Pow(playerPos[1] - tilePos[1], 2))
                        {
                            // Checks if there is wall between the player and the tile; CZ: Zkontrolujte, zda je mezi hráčem a dlaždicí zeď
                            bool isWallInBetween = false;
                            foreach (var wallPos in GetTilesOnLine(playerPos, tilePos, maxDistance))
                            {
                                if (wallPos[1] < Map.Length && wallPos[0] < Map[wallPos[1]].Length)
                                {
                                    if (Map[wallPos[1]][wallPos[0]] == '█')
                                    {
                                        isWallInBetween = true;
                                        break;
                                    }
                                }
                            }
                            if (isWallInBetween)
                            {
                                Console.Write(' '); // Tile is behind a wall; CZ: dlaždice je za zdí
                            }
                            else
                            {
                                convertToMap(Map, j, i); // Tile is not behind a wall -> renders it; CZ: vyrenderuje dlaždici normálně
                            }
                        }
                        else
                        {
                            Console.Write(' '); // IF outside of the circle render air; CZ: místo toho vyrenderuje prostor
                        }
                    }
                    else
                    {
                        Console.Write(' '); // outside of the MaxDistance; CZ: místo toho vyrenderuje prostor
                    }
                }
                Console.WriteLine();
            }
        }
    }
}