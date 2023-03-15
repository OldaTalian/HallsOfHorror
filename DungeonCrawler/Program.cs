using static DungeonCrawler.maps;
using static DungeonCrawler.Player;
using static DungeonCrawler.variables;
using static DungeonCrawler.Sounds;
using static DungeonCrawler.Enemy;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.Json;

namespace DungeonCrawler
{
    internal class Program
    {

        public static int[] MeasureDistance(int[] pos1, int[] pos2)//Měří vzdálenost mezi dvěma body (pos2 je obráceně FIXNOUT)
        {
            int x1 = pos1[0];
            int y1 = pos1[1];
            int x2 = pos2[1];
            int y2 = pos2[0];
            int[] distance = { (x2 - x1) , (y2 - y1) };
            return distance;
        } 

        public static bool CheckForTiles(int[] playerPos, char direction, char tile) // zjišťuje jestli je na dané straně od pozice nějaký Tile
        {
            bool output = false;
            int x = playerPos[0];
            int y = playerPos[1];
            if (direction== 'u') // UP
            {
                if(y != 0 && Map[y - 1][x] == tile)
                {
                    output = true;
                }
            }
            else if (direction == 'd') // DOWN
            {
                if (y != (Map.Length - 1) && Map[y + 1][x] == tile)
                {
                    output = true;
                }
            }
            else if (direction == 'l') // LEFT
            {
                if (x != 0 && Map[y][x - 1] == tile)
                {
                    output = true;
                }
            }
            else // RIGHT
            {
                if (x != (Map[y].Length - 1) && Map[y][x + 1] == tile)
                {
                    output = true;
                }
            }
            return output;
        }
        public static void Render() //Vyrenderuje mapu
        {
            //Console.Clear();
            if (Map == null) return;

            Console.WriteLine(ThisRoom+1 + " " + AllRooms().Length + " |  x:" + FindPlayer()[0] + " y:" + FindPlayer()[1] + 
                " D: " + MeasureDistance(FindPlayer(), locate)[0] + " " + MeasureDistance(FindPlayer(), locate)[1]); // Vypisuje v jaké místnosti z kolika hráč je (pro debug)
            
            // Vytiskne Mapu do konzole
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)//Zde se dají upravovat jednotlivé Tiles
                { 
                    if (Map[i][j] == '░' || Map[i][j] == '▒') { 
                        Console.ForegroundColor= ConsoleColor.DarkGray;
                        Console.Write(Map[i][j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Map[i][j] == '$')
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write(Map[i][j]);
                    }
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            // HUDBA:
            // potřebuje ffmpeg nainstalovaný
            // a potom se jenom použije ffplay a rozjede se ta hudba
            LoadMusic();
            PlaySound("muzika10.wav");

            Map[locate[0]][locate[1]] = 'E';
            AllPlayers();//změním všechny * na ☺
            Render();
            Console.SetWindowSize(100, 20);
            while (true) // Hra: 
            {
                //Console.SetWindowSize(Map[0].Length + 1,Map.Length + 2); // Změní velikost okna aby nešly vidět předchozí pohyby

                do
                {
                    Move();
                } while (!moved); //pokud se snaží jít do zdi tak to nic neudělá
                MoveEnemy();
                Render();
            }

        }
    
    }
}
