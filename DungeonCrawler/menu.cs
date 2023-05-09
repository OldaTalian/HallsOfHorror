using System.Reflection;
using static DungeonCrawler.Program;
using static DungeonCrawler.Variables;

namespace DungeonCrawler
{
    internal class menu
    {
        /// <summary>
        /// Main game logo
        /// CZ:
        /// Logo hry
        /// </summary>
        public static char[][] logo =
        {
            "   ▄  ▄ ▄▄▄  ▄▄▌  ▄▄▌   ▄▄      ".ToCharArray(),
            "  ██ ▐█▐█ ▀█ ██   ██   ▐█ ▀     ".ToCharArray(),
            "  ██▀▀█▄█▀▀█ ██   ██    ▀▀▀█▄   ".ToCharArray(),
            "  ██▌▐▀▐█  ▐▌▐█▌ ▄▐█▌ ▄▐█▄ ▐█   ".ToCharArray(),
            "  ▀▀▀   ▀  ▀  ▀▀▀  ▀▀▀  ▀▀▀▀    ".ToCharArray(),
            "               OF               ".ToCharArray(),
            " ▄  ▄      ▄▄▄  ▄▄▄        ▄▄▄  ".ToCharArray(),
            "██ ▐█ ▄█▀▄ ▀▄ █ ▀▄ █  ▄█▀▄ ▀▄ █ ".ToCharArray(),
            "██▀▀█▐█▌ ▐▌▐▀▀▄ ▐▀▀▄ ▐█▌ ▐▌▐▀▀▄ ".ToCharArray(),
            "██▌▐▀▐█▌ ▐▌▐█ █▌▐█ █▌▐█▌ ▐▌▐█ █▌".ToCharArray(),
            "▀▀▀   ▀█▄▀  ▀  ▀ ▀  ▀ ▀█▄▀  ▀  ▀".ToCharArray()
        };

        /// <summary>
        /// Writes the logo to the console center
        /// CZ:
        /// Vypíše logo hry do středu konzole
        /// </summary>
        public static void PrintLogo(int delay = 0) {
            for (int i = 0; i < logo.Length; i++)
            {
                getCursorToCenter(32,false);
                for (int j = 0; j < logo[i].Length; j++)
                {
                    Console.Write(logo[i][j]);
                }
                Console.WriteLine();
                Thread.Sleep(delay);
            }
        }
        private static string[] menuOptions =
        {
            "New Game",
            "Settings",
            "Credits",
            "Quit app",
        };
        private static byte option = 1;

        /// <summary>
        /// GUI of main menu
        /// CZ:
        /// Grafické rozhraní meny
        /// </summary>
        /// <param name="option"></param>
        private static void OptionMenu(byte option)
        {
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i + 1 == option)
                {
                    getCursorToCenter(70, false);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("> " + menuOptions[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    getCursorToCenter(70, false);
                    Console.WriteLine("  " + menuOptions[i]);
                }
            }
        }
        /// <summary>
        /// Main menu controlls
        /// CZ:
        /// Ovládání hlavního meny
        /// </summary>
        /// <returns></returns>
        public static int Menu()
        {
            bool didSelect = false;
            while (!didSelect)
            {
                Console.Clear();
                PrintLogo();
                Console.WriteLine("\n\n");
                OptionMenu(option);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        if(option != menuOptions.Length)
                        {
                            option++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (option > 1)
                        {
                            option--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (option >= 1 && option <= 4)
                        {
                            didSelect = true;
                        }
                        break;
                }
            }
            return option;
        }
    }
}
