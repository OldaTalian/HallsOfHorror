using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonCrawler.Variables;

namespace DungeonCrawler
{
    class roomMaker
    {
        static Dictionary<char, ConsoleColor> paletteColors = new Dictionary<char, ConsoleColor>()
        {
            { '█', ConsoleColor.White },    // Wall
            { '▓', ConsoleColor.White },    // Wall
            { '░', ConsoleColor.White },    // Floor
            { '▒', ConsoleColor.White },    // Floor
            { '*', ConsoleColor.Magenta },  // Spawn Point
            { '$', ConsoleColor.White },    // Walk Through Wall
            { '{', ConsoleColor.Red },      // Closed Lever
            { '}', ConsoleColor.Green },    // Open Lever
            { 'ł', ConsoleColor.Red },      // Closed Door
            { 'Ł', ConsoleColor.Green },    // Open Door
            { '#', ConsoleColor.Red },      // Magma (deals damage)
            { ' ', ConsoleColor.DarkBlue }, // Void (kills instantly)
            { '@', ConsoleColor.DarkRed },  // BOSS
            { '☻', ConsoleColor.Red },      // Enemy
            { '|', ConsoleColor.Cyan },     // Door to the next room
            { '\\', ConsoleColor.Cyan }     // Door to the previous room
        };

        static Dictionary<char, string> paletteDescriptions = new Dictionary<char, string>()
        {
            { '█', "Wall"},    // Wall
            { '▓', "Light"},    // Wall
            { '░', "Floor"},    // Floor
            { '▒', "messy Floor"},    // Floor
            { '*', "Player spawn point"},  // Spawn Point
            { '$', "Walk through wall"},    // Walk Through Wall
            { '{', "Closed Lever"},      // Closed Lever
            { '}', "Open Lever"},    // Open Lever
            { 'ł', "Closed Door"},      // Closed Door
            { 'Ł', "Open Door"},    // Open Door
            { '#', "Magma"},      // Magma (deals damage)
            { ' ', "Void"}, // Void (kills instantly)
            { '@', "Boss"},  // BOSS
            { '☻', "Enemy"},      // Enemy
            { '|', "Door to the next room"},     // Door to the next room
            { '\\',"Door to the previous room" }     // Door to the previous room
        };

        static string GetPalette()
        {
            StringBuilder paletteBuilder = new StringBuilder();
            foreach (char block in paletteColors.Keys)
            {
                paletteBuilder.Append(block);
            }
            return paletteBuilder.ToString();
        }

        public static void CreateRoom()
        {
            int mapWidth = ConsoleReadInt("Width: ");
            int mapHeight = ConsoleReadInt("Height: ");
            Console.WriteLine();

            char[][] map = new char[mapHeight][];

            for (int i = 0; i < mapHeight; i++)
            {
                map[i] = new char[mapWidth];
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i][j] = ' ';
                }
            }

            int cursorX = 0;
            int cursorY = 0;
            char selectedBlock = '█';

            while (true)
            {
                Console.Clear();
                DisplayPalette(selectedBlock);
                Console.WriteLine();
                DisplayMap(map, cursorX, cursorY);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        cursorY = Math.Max(0, cursorY - 1);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        cursorY = Math.Min(mapHeight - 1, cursorY + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        cursorX = Math.Max(0, cursorX - 1);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        cursorX = Math.Min(mapWidth - 1, cursorX + 1);
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.OemComma:
                    case ConsoleKey.Oem4: // Left bracket
                        selectedBlock = ChangeSelectedBlock(selectedBlock, -1);
                        break;
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.OemPeriod:
                    case ConsoleKey.Oem6: // Right bracket
                        selectedBlock = ChangeSelectedBlock(selectedBlock, 1);
                        break;
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        map[cursorY][cursorX] = selectedBlock;
                        break;
                }
            }
        }

        static char ChangeSelectedBlock(char currentBlock, int direction)
        {
            string palette = GetPalette();
            int currentIndex = palette.IndexOf(currentBlock);
            int newIndex = (currentIndex + direction + palette.Length) % palette.Length;
            return palette[newIndex];
        }

        static void DisplayPalette(char selectedBlock)
        {
            string palette = GetPalette();
            getCursorToCenter(palette.Length * 2, false);
            for (int i = 0; i <= palette.Length * 2; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
            getCursorToCenter(palette.Length * 2, false);
            for (int i = 0; i < palette.Length; i++)
            {
                if (palette[i] == selectedBlock)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("(" + palette[i] + ")");
                }
                else
                {
                    Console.Write(" " + palette[i]);
                }
                Console.ResetColor();
            }
            Console.WriteLine();
            getCursorToCenter(palette.Length * 2, false);
            for (int i = 0; i <= palette.Length * 2; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
            getCursorToCenter(paletteDescriptions[selectedBlock].Length, false);
            Console.WriteLine(paletteDescriptions[selectedBlock]);
            Console.ResetColor();
        }

        static void DisplayMap(char[][] map, int cursorX, int cursorY)
        {
            int mapHeight = map.Length;
            int mapWidth = map[0].Length;
            for (int y = 0; y < mapHeight; y++)
            {
                getCursorToCenter(mapWidth, false);
                for (int x = 0; x < mapWidth; x++)
                {
                    char block = map[y][x];
                    if (x == cursorX && y == cursorY)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = paletteColors.ContainsKey(block) ? paletteColors[block] : ConsoleColor.White;
                    }
                    Console.Write(block);
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
