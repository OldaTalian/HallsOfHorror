using static DungeonCrawler.Enemy;
using static DungeonCrawler.maps;
using static DungeonCrawler.menu;
using static DungeonCrawler.Fight;
using static DungeonCrawler.Player;
using static DungeonCrawler.Render;
using static DungeonCrawler.Sounds;
using static DungeonCrawler.Variables;

namespace DungeonCrawler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Load map
            if (DO_DEBUG)
            {
                Map = AllRooms()[0];
            }
            else
            {
                Map = AllRooms()[1];
            }
            RegisterSpawns(); // Spawn locations
            mainPlayerPos = FindPlayerPos();
            RegisterEnemies(enemy); // Enemies

            // MUSIC:
            // You need to have installed ffmpeg to use it

            LoadMusic();

            // menu here                                      < -------- MENU ------- < -------

            PlayMusic("main_menu.mp3");


            Menu();
            
            RenderScreen();
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
                if(playerHealth < 100)
                {
                    playerHealth++; 
                }
           
                do
                {
                    Move();
                } while (!moved); //pokud se snaží jít do zdi tak to nic neudělá
                mainPlayerPos = FindPlayerPos();
                enemyTick();
                RenderScreen();
            }
            Console.Clear();
            Console.WriteLine("You ded ¯\\_☺_/¯\nPress something to close the program...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}