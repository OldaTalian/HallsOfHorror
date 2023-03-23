using static DungeonCrawler.maps;
using static DungeonCrawler.Program;
using static DungeonCrawler.Player;
using static DungeonCrawler.variables;
using static DungeonCrawler.Sounds;
using static DungeonCrawler.Enemy;

namespace DungeonCrawler
{
    internal class render
    {
        public static void Render() //Vyrenderuje mapu
        {
            //Console.Clear();
            if (Map == null) return;

            Console.WriteLine(ThisRoom + 1 + " " + AllRooms().Length + " |  x:" + FindPlayer()[0] + " y:" + FindPlayer()[1] +
               " Health: " + ((playerHealth > 0) ? playerHealth : 0)); // Vypisuje v jaké místnosti z kolika hráč je (pro debug)

            // Vytiskne Mapu do konzole
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)//Zde se dají upravovat jednotlivé Tiles
                {
                    if (Map[0][0] == 'Đ')
                    {
                        int[] pos = { j, i };
                        if (MeasureDistance(FindPlayer(), pos) > 5){
                            Console.Write(' ');
                        }
                        else
                        {
                            convertToMap(Map,j,i);
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

        public static void convertToMap(char[][] Map,int x, int y) {
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
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("#");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Write(Map[y][x]);
            }
        }
    }
}