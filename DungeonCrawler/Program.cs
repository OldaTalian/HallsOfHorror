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
                if (lastStepOn == '{')
                {
                    lastStepOn = '}';
                }
                else if (lastStepOn == '}')
                {
                    lastStepOn = '{';
                }
                else if (lastStepOn == ' ')
                {
                    playerHealth = 0;
                }
                if (playerHealth <= 0)
                {
                    break;
                }
                if (playerHealth < 100)
                {
                    playerHealth++;
                }
                if (FindOnMap('{', Map)[0] == -1)
                    if (FindOnMap('ł', Map)[0] != -1)
                        Map[FindOnMap('ł', Map)[0]][FindOnMap('ł', Map)[1]] = '░';
                do
                {
                    Move();
                } while (!moved); //pokud se snaží jít do zdi tak to nic neudělá
                mainPlayerPos = FindPlayerPos();
                enemyTick();
                RenderScreen();
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
            }
            Console.Clear();
            Console.WriteLine("You ded ¯\\_☺_/¯\nPress ESC to close the program...");
            bool ending = true;
            while (ending)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        ending = false; break;
                }
            }
            Environment.Exit(0);
        }
    }
}