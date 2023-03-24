using static DungeonCrawler.maps;
using static DungeonCrawler.Player;
using static DungeonCrawler.variables;

namespace DungeonCrawler
{
    internal class render
    {
        public static void Render()
        {
            Console.Clear();
            if (Map == null) return;

            Console.WriteLine("Rooms: " + ThisRoom + 1 + "/" + AllRooms().Length + " |  x:" + FindPlayer()[0] + " y:" + FindPlayer()[1] +
                " Health: " + ((playerHealth > 0) ? playerHealth : 0));

            // Check if there is a circle to be drawn
            int maxDistance;
            if (Map[Map.Length - 1][Map[Map.Length - 1].Length - 2] == 'Đ' &&
                int.TryParse(Map[Map.Length - 1][Map[Map.Length - 1].Length - 1].ToString(), out maxDistance))
            {
                // Renders map with circle
                int[] playerPos = FindPlayer();

                for (int i = 0; i < Map.Length; i++)
                {
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        int[] tilePos = { j, i };
                        int distance = MeasureDistance(playerPos, tilePos);

                        if (distance <= maxDistance)
                        {
                            // Calculate the radius of the circle that should be drawn around the player position
                            int radius = maxDistance - distance + 3;

                            // Check if the tile is within the circle
                            if (radius * radius >= Math.Pow(playerPos[0] - tilePos[0], 2) + Math.Pow(playerPos[1] - tilePos[1], 2))
                            {
                                // Check if there is a wall between the player and the tile
                                bool isWallInBetween = false;
                                foreach (var wallPos in GetTilesOnLine(playerPos, tilePos))
                                {
                                    if (Map[wallPos[1]][wallPos[0]] == '█')
                                    {
                                        isWallInBetween = true;
                                        break;
                                    }
                                }

                                if (isWallInBetween)
                                {
                                    if (Map[i][j] == '█')
                                    {
                                        Console.Write('█'); // its the wall
                                    }
                                    else
                                    {
                                        Console.Write(' '); // tile is behind a wall
                                    }
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
            else
            {
                // Renders map without circle
                for (int i = 0; i < Map.Length; i++)
                {
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
        public static int[][] GetTilesOnLine(int[] startPos, int[] endPos)
        {
            int dx = Math.Abs(endPos[0] - startPos[0]);  // Calculate the absolute difference in x-coordinates.
            int dy = Math.Abs(endPos[1] - startPos[1]);  // Calculate the absolute difference in y-coordinates.
            int x = startPos[0];  // Initialize the current x-coordinate to the starting x-coordinate.
            int y = startPos[1];  // Initialize the current y-coordinate to the starting y-coordinate.
            int xStep = (endPos[0] > startPos[0]) ? 1 : -1;  // Calculate the step to be taken in the x-direction.
            int yStep = (endPos[1] > startPos[1]) ? 1 : -1;  // Calculate the step to be taken in the y-direction.
            int maxDistance = Math.Max(dx, dy) + 1;  // Calculate the maximum distance between the start and end points.
            int[][] tiles = new int[maxDistance][];  // Create a new array to store the tiles.

            int i = 0;  // Initialize the index variable.
            while (i < maxDistance)
            {
                tiles[i] = new int[] { x, y };  // Store the current tile in the array.
                i++;

                int error = dx - dy;  // Calculate the error term.

                // Take the appropriate step based on the error term.
                if (error > 0 || (error == 0 && xStep > 0))
                {
                    x += xStep;
                    error -= dy;
                }
                else
                {
                    y += yStep;
                    error += dx;
                }
            }

            return tiles;  // Return the array of tiles.
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
                else if (Map[y][x] == '#') // magma tile
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("#");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
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

    }
}