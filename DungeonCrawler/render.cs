using static DungeonCrawler.maps;
using static DungeonCrawler.Variables;
using static DungeonCrawler.Player;

namespace DungeonCrawler
{
    internal class Render
    {
        public static void RenderScreen()
        {
            Console.Clear();
            if (Map == null) return; // Don't render nothing
            getCursorToCenter(34, false);
            Console.WriteLine("Rooms: " + (ThisRoom + 1) + "/" + AllRooms().Length + " |  x:" + mainPlayerPos[0] + " y:" + mainPlayerPos[1] +
                " Health: " + ((playerHealth > 0) ? playerHealth : 0)); //DEBUG message
            // Check if there is a circle to be drawn

            for (int i = 0; i < Console.BufferHeight / 2 - (Map.Length / 2 + 1); i++)
            {
                Console.WriteLine();
            }
            int maxDistance;
            if (Map[Map.Length - 1][Map[Map.Length - 1].Length - 2] == 'Đ' &&
                int.TryParse(Map[Map.Length - 1][Map[Map.Length - 1].Length - 1].ToString(), out maxDistance))
            {
                RenderCircle(maxDistance);
            }
            else
            {
                // Renders map without circle
                for (int i = 0; i < Map.Length; i++)
                {
                    getCursorToCenter(Map[0].Length, false);
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        convertToMap(Map, j, i);
                    }
                    Console.WriteLine();
                }
            }
        }

        // Returns an array of tiles between the start and end points, inclusive, on a line defined by those points.
        // using Bresenham's algorithm (https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm).
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

        public static void convertToMap(char[][] Map, int x, int y)
        {
            if (!skipNextOne)
            {
                if (Map[y][x] == '░' || Map[y][x] == '▒') // gray tiles
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(Map[y][x]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (Map[y][x] == '$') // walk through wall
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == '{') // walk through wall
                {
                    Console.Write("O");
                }
                else if (Map[y][x] == '}') // walk through wall
                {
                    Console.Write("X");
                }
                else if (Map[y][x] == 'ł') // walk through wall
                {
                    Console.Write("#");
                }
                else if (Map[y][x] == '#') // magma tile
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("#");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == ' ') // magma tile
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == '☻') // enemy tile
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("☻");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (Map[y][x] == 'Đ') // Đ blind mark
                {
                    skipNextOne = true; // set flag to skip rendering next tile
                }
                else // normal tiles
                {
                    Console.Write(Map[y][x]);
                }
            }
            else
            {
                Console.Write("");
                skipNextOne = false; // reset flag after skipping one tile
            }
        }

        public static void RenderCircle(int maxDistance)
        {
            // Renders map with circle
            int[] playerPos = mainPlayerPos;
            for (int i = 0; i < Map.Length; i++)
            {
                getCursorToCenter(Map[0].Length , false);
                for (int j = 0; j < Map[i].Length; j++)
                {
                    int[] tilePos = { j, i };
                    int distance = MeasureDistance(playerPos, tilePos);

                    // Calculate the radius of the circle that should be drawn around the player position
                    int radius = maxDistance - distance + 3;
                    if (maxDistance > distance)
                    {
                        // Check if the tile is within the circle
                        if (radius * radius >= Math.Pow(playerPos[0] - tilePos[0], 2) + Math.Pow(playerPos[1] - tilePos[1], 2))
                        {
                            // Check if there is a wall between the player and the tile
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
                                Console.Write(' '); // tile is behind a wall
                            }
                            else
                            {
                                convertToMap(Map, j, i); // render the tile normally
                            }
                        }
                        else
                        {
                            Console.Write(' '); // render space instead
                        }
                    }
                    else
                    {
                        Console.Write(' '); // render space instead
                    }
                }
                Console.WriteLine();
            }
        }
    }
}