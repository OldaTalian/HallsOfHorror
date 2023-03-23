using static DungeonCrawler.maps;
using static DungeonCrawler.Player;
using static DungeonCrawler.variables;

namespace DungeonCrawler
{
    internal class render
    {
        public static void Render() //Vyrenderuje mapu
        {
            Console.Clear();
            if (Map == null) return;

            Console.SetWindowSize(Map[0].Length + 3, Map.Length + 1);

            Console.WriteLine(ThisRoom + 1 + " " + AllRooms().Length + " |  x:" + FindPlayer()[0] + " y:" + FindPlayer()[1] +
               " Health: " + ((playerHealth > 0) ? playerHealth : 0) + " D: " + IsNear('E',FindPlayer(),3) ); // Vypisuje v jaké místnosti z kolika hráč je (pro debug)

            // Vytiskne Mapu do konzole
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)//Zde se dají upravovat jednotlivé Tiles
                {
                    if (Map[Map.Length - 1][Map[0].Length - 2] == 'Đ')
                    {
                        int maxDistance = Convert.ToInt32(new string(Map[Map.Length - 1][Map[Map.Length - 1].Length - 1], 1));
                        int[] pos = { j, i };
                        if (MeasureDistance(FindPlayer(), pos) > maxDistance)
                        {
                            Console.Write(' ');
                        }
                        else
                        {
                            convertToMap(Map, j, i);
                        }
                    }
                    else
                    {
                        convertToMap(Map, j, i);
                    }
                }
                Console.WriteLine();
            }
        }

        public static bool skipNextOne = false;

        public static void convertToMap(char[][] Map, int x, int y)
        {
            if (!skipNextOne)
            {
                if (Map[y][x] == '░' || Map[y][x] == '▒')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(Map[y][x]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (Map[y][x] == '$')
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == '#')
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("#");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (Map[y][x] == 'Đ')
                {
                    skipNextOne = true;
                }
                else
                {
                    Console.Write(Map[y][x]);
                }
            }
            else
            {
                Console.Write("");
                skipNextOne = false;
            }
        }
    }
}