using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class menu
    {
        public static char[][] logo =
        {
            " ▄  ▄ ▄▄▄  ▄▄▌  ▄▄▌   ▄▄            ▄▄▄   ▄  ▄      ▄▄▄  ▄▄▄        ▄▄▄  ".ToCharArray(),
            "██ ▐█▐█ ▀█ ██   ██   ▐█ ▀     ▄█▀▄ ▐▄▄   ██ ▐█ ▄█▀▄ ▀▄ █ ▀▄ █  ▄█▀▄ ▀▄ █·".ToCharArray(),
            "██▀▀█▄█▀▀█ ██   ██    ▀▀▀█▄  ▐█▌ ▐▌█     ██▀▀█▐█▌ ▐▌▐▀▀▄ ▐▀▀▄ ▐█▌ ▐▌▐▀▀▄ ".ToCharArray(),
            "██▌▐▀▐█  ▐▌▐█▌ ▄▐█▌ ▄▐█▄ ▐█  ▐█▌ ▐▌██    ██▌▐▀▐█▌ ▐▌▐█ █▌▐█ █▌▐█▌ ▐▌▐█ █▌".ToCharArray(),
            "▀▀▀   ▀  ▀  ▀▀▀  ▀▀▀  ▀▀▀▀    ▀█▄▀ ▀▀▀   ▀▀▀   ▀█▄▀  ▀  ▀ ▀  ▀ ▀█▄▀  ▀  ▀".ToCharArray(),
        };

        public static void PrintLogo() {
            for (int i = 0; i < logo.Length; i++)
            {
                for (int j = 0; j < logo[i].Length; j++)
                {
                    Console.Write(logo[i][j]);
                }
                Console.WriteLine();
            }
        }

        public static void Menu()
        {
            PrintLogo();
            Console.WriteLine("\n\n\n");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Play game...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Press anything to enter..");
            Console.ReadKey();
        }
    }
}




