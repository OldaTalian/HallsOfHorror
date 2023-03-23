using static DungeonCrawler.maps;
using static DungeonCrawler.menu;
using static DungeonCrawler.Player;
using static DungeonCrawler.variables;
using static DungeonCrawler.Sounds;
using static DungeonCrawler.Enemy;
using static DungeonCrawler.render;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.Json;

namespace DungeonCrawler
{
    internal class Program
    {

        public static int MeasureDistance(int[] pos1, int[] pos2)
        {
            int x1 = pos1[0];
            int y1 = pos1[1];
            int x2 = pos2[0];
            int y2 = pos2[1];
            int distance = Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
            return distance;
        }


        static void Main(string[] args)
        {
            // HUDBA:
            // potřebuje ffmpeg nainstalovaný
            // a potom se jenom použije ffplay a rozjede se ta hudba
            LoadMusic();

            // menu here                                      < -------- MENU ------- < -------


            PlaySound("muzika10.wav");

            for (int i = 0; i < AllEnemies( Map,enemy); i++)
            {
                enemyLastTile[i] = '░';
            }



            Map[locate[0]][locate[1]] = 'E';
            AllPlayers();//změním všechny * na ☺
            Render();
            //Console.SetWindowSize(100, 20);
            while (true) // Hra: 
            {
                if(lastStepOn == '#')
                {
                    playerHealth -= 5;
                }
                if (playerHealth <= 0)
                {
                    break;
                }


                do
                {
                    Move();
                } while (!moved); //pokud se snaží jít do zdi tak to nic neudělá
                enemyTick();
                Render();
            } 
            Console.Clear();
            Console.WriteLine("You ded ☺\nPress something to close the program...");
            Console.ReadKey();
        }
    
    }
}
