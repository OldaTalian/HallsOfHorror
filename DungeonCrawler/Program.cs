using static DungeonCrawler.Enemy;
using static DungeonCrawler.maps;
using static DungeonCrawler.menu;
using static DungeonCrawler.Fight;
using static DungeonCrawler.Player;
using static DungeonCrawler.Settings;
using static DungeonCrawler.Render;
using static DungeonCrawler.Sounds;
using static DungeonCrawler.Variables;
using static DungeonCrawler.IntroLore;

namespace DungeonCrawler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            // You need to have installed ffmpeg to use it
            LoadMusic();
            PlayMusic("main_menu.mp3");
            while (true)
            {
                int option = Menu();

                if (option == 1) {
                    StartNewGame();
                }
                else if(option == 2) {
                    SettingsMenu();
                }
                else if (option == 3)
                {
                    Environment.Exit(0);
                }
            }
        }


        public static void StartNewGame()
        {
            Console.Clear();
            getCursorToCenter(17);
            Console.WriteLine("Creating new game");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Thread.Sleep(300);
            Console.Clear();
            getCursorToCenter(17);
            Console.WriteLine("Creating new game");
            Console.ForegroundColor = ConsoleColor.White;
            RevertOriginalMaps();
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
            StartIntro();
            lastStepOn = '░';

            RenderScreen();
            playerHealth = defaultPlayerHealth;
            bool gameIsPlaying = true;
            while (gameIsPlaying) // Game:
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
                    gameIsPlaying = false;
                    break;
                }
                if (playerHealth < defaultPlayerHealth)
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
            Console.WriteLine("You ded ¯\\_☺_/¯\nPress ESC to continue...");
            bool ending = true;
            while (ending)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Enter:
                        ending = false; break;
                }
            }
        }
    }
}