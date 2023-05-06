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
            Console.Title = "Halls of Horror";
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

        /// <summary>
        /// Starts a new game
        /// CZ:
        /// Vytvoří novou hru
        /// </summary>
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
            RandomMaps(true);
            // Load map; CZ: Načte mapu
            if (DO_DEBUG)
            {
                Map = AllRooms()[0];
            }
            else
            {
                Map = AllRooms()[1];
                ThisRoom = 1;
            }
            RegisterSpawns();
            lastStepOn = '▓';
            mainPlayerPos = FindPlayerPos();
            RegisterEnemies(enemy);
            StartIntro(); // Plays the intro; CZ: Přehraje intro

            // The actual game ↓

            RenderScreen();
            playerHealth = defaultPlayerHealth;
            bool gameIsPlaying = true;
            while (gameIsPlaying) // Game tick: 
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
                        Map[FindOnMap('ł', Map)[0]][FindOnMap('ł', Map)[1]] = 'Ł';
                if (FindOnMap('}', Map)[0] == -1)
                    if (FindOnMap('Ł', Map)[0] != -1)
                        Map[FindOnMap('Ł', Map)[0]][FindOnMap('Ł', Map)[1]] = 'ł';
                do
                {
                    Move();
                } while (!moved);

                mainPlayerPos = FindPlayerPos();
                enemyTick();
                RenderScreen();

                while (Console.KeyAvailable) // Removes any holding key; CZ: vymaže klávesy které hráč drží
                {
                    Console.ReadKey(true);
                }
            }
            getCursorToCenter(42);
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