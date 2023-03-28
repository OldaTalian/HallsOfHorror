using static DungeonCrawler.Enemy;
using static DungeonCrawler.maps;
using static DungeonCrawler.menu;
using static DungeonCrawler.Player;
using static DungeonCrawler.render;
using static DungeonCrawler.Sounds;
using static DungeonCrawler.variables;

namespace DungeonCrawler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Load map
            Map = AllRooms()[0];
            RegisterSpawns(); // Spawn locations
            RegisterEnemies(enemy); // Enemies

            // MUSIC:
            // You need to have installed ffmpeg to use it

            LoadMusic();

            // menu here                                      < -------- MENU ------- < -------
            PlaySound("muzika10.wav");

            Menu();
            
            Render();
            //Console.SetWindowSize(100, 20);

            while (true) // Game:
            {
                if (lastStepOn == '#')
                {
                    playerHealth -= 5;
                }
                if (lastStepOn == ' ')
                {
                    playerHealth = 0;
                }
                if (playerHealth <= 0)
                {
                    break;
                }

                do
                {
                    Move();
                } while (!moved); //pokud se snaží jít do zdi tak to nic neudělá
                mainPlayerPos = FindPlayerPos();
                enemyTick();
                Render();
            }
            Console.Clear();
            Console.WriteLine("You ded ¯\\_☺_/¯\nPress something to close the program...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}