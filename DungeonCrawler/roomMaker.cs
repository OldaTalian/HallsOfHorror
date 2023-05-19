using System;
using System.Configuration;
using System.Text;
using static DungeonCrawler.Settings;
using static DungeonCrawler.Variables;

namespace DungeonCrawler
{
    class roomMaker
    {
        static Dictionary<char, ConsoleColor> paletteColors = new Dictionary<char, ConsoleColor>()
        {
            { '█', ConsoleColor.White },    // Wall
            { '▓', ConsoleColor.White },    // Wall
            { '░', ConsoleColor.DarkGray },    // Floor
            { '▒', ConsoleColor.DarkGray },    // Floor
            { '*', ConsoleColor.Yellow },  // Spawn Point
            { '$', ConsoleColor.White },    // Walk Through Wall
            { '{', ConsoleColor.DarkRed },      // Closed Lever
            { '}', ConsoleColor.DarkGreen },    // Open Lever
            { 'ł', ConsoleColor.Red },      // Closed Door
            { 'Ł', ConsoleColor.Green },    // Open Door
            { '#', ConsoleColor.DarkRed },      // Magma (deals damage)
            { ' ', ConsoleColor.DarkCyan }, // Void (kills instantly)
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
            { '▒', "Messy Floor"},    // Floor
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
        private static bool paint = false;

        static string GetPalette()
        {
            StringBuilder paletteBuilder = new StringBuilder();
            foreach (char block in paletteColors.Keys)
            {
                paletteBuilder.Append(block);
            }
            return paletteBuilder.ToString();
        }
        static int cursorX = 0;
        static int cursorY = 0;
        static char selectedBlock = '█';
        static int mapHeight = 5;
        static int mapWidth = 5;
        static char[][] map = new char[0][];
        static string MapName = string.Empty;


        public static void CreateRoom()
        {
            getCursorToCenter(20, false);
            Console.WriteLine("Welcome to RoomMaker");
            getCursorToCenter(26, false);
            Console.WriteLine("What's your desired map...");
            getCursorToCenter(15, false);
            mapWidth = ConsoleReadInt("Width: ");
            getCursorToCenter(15, false);
            mapHeight = ConsoleReadInt("Height: ");
            Console.WriteLine();

            map = new char[mapHeight][];

            for (int i = 0; i < mapHeight; i++)
            {
                map[i] = new char[mapWidth];
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i][j] = '░';
                }
            }
            Drawing();
        }

        static void Drawing()
        {
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
                        SaveMapToConfig(map);
                        return;
                    case ConsoleKey.I:
                    case ConsoleKey.OemPlus:
                        ImportMap();
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        map[cursorY][cursorX] = selectedBlock;
                        break;
                    case ConsoleKey.Tab:
                    case ConsoleKey.NumPad0:
                        paint = !paint;
                        break;
                }
                if (paint == true)
                {
                    map[cursorY][cursorX] = selectedBlock;
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
            if (paint)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }
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
                        Console.ResetColor();
                        Console.ForegroundColor = paletteColors.ContainsKey(block) ? paletteColors[block] : ConsoleColor.White;
                    }
                    Console.Write(block);
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    
        static void ImportMap()
        {
            Console.WriteLine("How do you want to import the map?");
            int option = MenuWithOptions(new string[] { "Paste", "Config", "Go back"}, defaultOption: 0, centerMenu: true);
            switch (option)
            {
                case 0:
                    Console.WriteLine("Paste your map string here:");
                    map = Console.ReadLine().Split("\\n", StringSplitOptions.RemoveEmptyEntries)
                        .Select(row => row.Trim().ToArray())
                        .ToArray();
                    break;
                case 1:
                    string[] MapNames = new string[0];
                    for (int i = 0; i < ConfigurationManager.AppSettings.AllKeys.Length; i++)
                    {
                        if (ConfigurationManager.AppSettings.AllKeys[i].Contains("Room"))
                        {
                            Array.Resize(ref MapNames, MapNames.Length + 1);
                            MapNames[MapNames.Length - 1] = ConfigurationManager.AppSettings.AllKeys[i];
                        }
                    }
                    Console.Clear();
                    Console.WriteLine();
                    int option1 = verticalOptionMenu(MapNames, centerMenu: true);
                    map = GetConfigValue(MapNames[option1]).Replace("\r\n", "\n")
                        .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                        .Select(row => row.Trim().ToArray())
                        .ToArray();
                    MapName = MapNames[option1];
                    break;
            }
            mapHeight = map.Length;
            mapWidth = map[0].Length;
            cursorX = 0;
            cursorY = 0;
        }
        static void SaveMapToConfig(char[][] map)
        {
            getCursorToCenter(29, false);
            Console.WriteLine("Would you like to save the map?");
            Console.WriteLine();
            int option = MenuWithOptions(new string[] { "No", "Yes", "Go back"}, defaultOption: 1, centerMenu: true);
            Random random = new Random();
            if (option == 1)
            {
                string output = string.Empty;
                for (int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[y].Length; x++)
                    {
                        output += map[y][x];
                    }
                    output += "\n";
                }
                if (MapName == string.Empty)
                {
                    MapName = "CustomRoom" + random.Next(0,999999999);
                }
                SetConfigValue(MapName, output);
                Console.WriteLine("Map saved as " + MapName);
                Console.ReadKey();
                return;
            }
            else if (option == 2)
            {
                Drawing();
                return;
            }
        }
    }
}
