using static DungeonCrawler.Settings;
using static DungeonCrawler.Sounds;
using static DungeonCrawler.menu;
using Microsoft.VisualBasic;

namespace DungeonCrawler
{
    internal class Variables
    {
        // Main variables; CZ: Základní proměnné
        public static char player = '☺';
        public static char lastStepOn = '▒'; // Tile that is the player on; CZ: Dlaždice na které hráč stojí

        public static bool moved = false;
        public static int defaultPlayerHealth = 100;
        public static int playerHealth = defaultPlayerHealth;
        public static byte playerAttack = 3;
        public static int[] mainPlayerPos = new int[2];

        public static char[][] Map = new char[0][];

        public static int ThisRoom = 0;
        public static long currentTick = 0;

        public static bool DEBUG_ROOM = bool.Parse(GetConfigValue("roomDebugEnabled"));
        public static bool showDebug = bool.Parse(GetConfigValue("showDebug"));
        public static bool MusicEnabled = bool.Parse(GetConfigValue("musicEnabled"));
        public static int MusicVolume = int.Parse(GetConfigValue("musicVolume"));
        public static bool SoundsEnabled = bool.Parse(GetConfigValue("soundEnabled"));
        public static int SoundVolume = int.Parse(GetConfigValue("soundVolume"));
        public static string OperatingSystem = GetConfigValue("OS");

        // Functions; CZ: Funkce

        /// <summary>
        /// Adds char to char array
        /// CZ:
        /// Přidá novou hodnotu na konec pole a vrátí nově vytvořené pole.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private static char[] AddToArray(char[] array, char newValue)
        {
            char[] newArray = new char[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            newArray[newArray.Length - 1] = newValue;

            return newArray;
        }

        /// <summary>
        /// Checks if there is char in direction from some position
        /// CZ:
        /// Zjistí jestli je konkrétní charakter v specifickém směru od hráčovy pozice.
        /// </summary>
        /// <param name="pos">int[]{X,Y}</param>
        /// <param name="direction">char up=u; down=d; left=l; right=r</param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static bool CheckForTiles(int[] pos, char direction, char tile) // position (X,Y), direction u/d/l/r, tile [char]
        {
            bool output = false;
            int x = pos[0];
            int y = pos[1];
            if (y > Map.Length -1)
            {
                return false;
            }
            if (direction == 'u') // UP
            {
                if (y != 0 && Map[y - 1][x] == tile)
                {
                    output = true;
                }
            }
            else if (direction == 'd') // DOWN
            {
                if (y != (Map.Length - 1) && Map[y + 1][x] == tile)
                {
                    output = true;
                }
            }
            else if (direction == 'l') // LEFT
            {
                if (x != 0 && Map[y][x - 1] == tile)
                {
                    output = true;
                }
            }
            else // RIGHT
            {
                if (x != (Map[y].Length - 1) && Map[y][x + 1] == tile)
                {
                    output = true;
                }
            }
            return output;
        }

        /// <summary>
        /// Calculates distance between two points.
        /// CZ:
        /// Zpočítá Manhattonovu zdálenost mezi dvěmi body.
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns>Distance</returns>
        public static int MeasureDistance(int[] pos1, int[] pos2)
        {
            int x1 = pos1[0];
            int y1 = pos1[1];
            int x2 = pos2[0];
            int y2 = pos2[1];
            int distance = Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
            return distance;
        }

        /// <summary>
        /// Tries to find char in radius from the position.
        /// CZ:
        /// Zjistí jestli je charakter v určitém radiusu.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pos"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static bool IsNear(char search, int[] pos, int distance)
        {
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)
                {
                    if (Map[i][j] == search)
                    {
                        int[] tilePos = new int[] { j, i };
                        if (MeasureDistance(tilePos,pos) < distance)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Finds Tile on map
        /// CZ:
        /// Najde konkrétní charakter na určité mapě.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="TheMap"></param>
        /// <returns>Pozice int[]{X,Y}</returns>
        public static int[] FindOnMap(char character, char[][] TheMap)
        {
            int[] output = { -1,-1};
            for (int i = 0; i < TheMap.Length; i++)
            {
                for (int j = 0; j < TheMap[i].Length; j++)
                {
                    if (TheMap[i][j] == character)
                    {
                        output = new int[] { i , j };
                        return output;
                    }
                }
            }
            return output;
        }
    
        /// <summary>
        /// Moves the cursor to the center of the wíndow.
        /// CZ: 
        /// Vycentrování rendrované hry na střed.
        /// </summary>
        /// <param name="TextLenght"></param>
        /// <param name="Vertical"></param>
        public static void getCursorToCenter(int TextLenght, bool Vertical = true)
        {
            if (Vertical && OperatingSystem == "win11")
            {
                for (int i = 0; i < Console.BufferHeight / 2 - 1; i++)
                {
                    Console.WriteLine();
                }
            }
            for (int i = 0; i < Console.BufferWidth / 2 - (TextLenght / 2); i++)
            {
                Console.Write(' ');
            }
        }
        /// <summary>
        /// Menu with some options
        /// CZ:
        /// Meny s možnostmy
        /// </summary>
        /// <param name="options">Options in string[]</param>
        /// <param name="defaultOption"></param>
        /// <param name="centerMenu"></param>
        /// <returns>the option index</returns>
        public static int MenuWithOptions(string[] options,int defaultOption = 0, bool centerMenu = true)
        {
            int option = defaultOption;
            bool hasTyped = false;
            while (!hasTyped)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                if (centerMenu)
                {
                    getCursorToCenter(37, false);   
                }
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == option)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(options[i]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write(options[i]);
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (option > 0)
                        {
                            option--;
                        }
                        break;
                    case ConsoleKey.Escape:
                        hasTyped = true;
                        break;
                    case ConsoleKey.RightArrow:
                        if (option < options.Length - 1)
                        {
                            option++;   
                        }
                        break;
                    case ConsoleKey.Enter:
                        return option;
                        hasTyped = true;
                        break;
                }
            }
            return option;
        }

        public static int ConsoleReadInt(string message = "")
        {
            int output = 0;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out output));
            return output;
        }

        /// <summary>
        /// This creates the effect of each character being typed out one by one.
        /// CZ:
        /// Udělá effekt psaní písmen po sobě
        /// </summary>
        /// <param name="text"></param>
        /// <param name="delay"></param>
        public static void TypeWriterEffect(string text, int delay)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        /// <summary>
        /// When player Dies
        /// CZ:
        /// Když hráč umře
        /// </summary>
        public static void PlayerDeath()
        {
            Console.Clear();
            StopMusic();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n\n\n");
            getCursorToCenter(9, false);
            Console.WriteLine("GAME OVER\n\n\n");
            Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n");
            getCursorToCenter(28, false);
            Console.WriteLine("Press any key to continue...");
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

        /// <summary>
        /// When player wons
        /// CZ:
        /// Když hráč vyhraje
        /// </summary>
        public static void GameWon()
        {
            Console.Clear();
            StopMusic();
            Thread.Sleep(300);
            getCursorToCenter(32, false);
            TypeWriterEffect("Congratulations, you have won the game!",50);
            Thread.Sleep(2000);
            Console.WriteLine();
            getCursorToCenter(42, false);
            TypeWriterEffect("You have successfully escaped the halls of horror.", 50);
            Thread.Sleep(2000);
            ShowCredits();
            Thread.Sleep(1000);
            Console.Clear() ;
            getCursorToCenter(25, true);
            Console.WriteLine("Thank you for playing.");
            Thread.Sleep(2000);
            getCursorToCenter(30, false);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays creators and makers of this game
        /// CZ:
        /// Ukáže všechny vývojáře této hry
        /// </summary>
        public static void ShowCredits()
        {
            Console.Clear();
            for (int i = 0; i <= Console.WindowHeight; i++)
            {
                Console.WriteLine();
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        return;
                    }
                }
            }
            int delay = 500;
            PrintLogo(delay);
            string[] credits = {"","","","made by:", "Olda Talián", "Lukáš Kotek", "Patrik Rychtařík","","",  "music by:", "Olda Talián", "Letorin#9895" };

            Console.WriteLine();
            Thread.Sleep(delay);

            for (int i = 0; i < credits.Length; i++)
            {
                getCursorToCenter(credits[i].Length, false);
                Console.WriteLine(credits[i]);
                Thread.Sleep(delay);
                Console.WriteLine();
                Thread.Sleep(delay);
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        return;
                    }
                }
            }
            for (int i = 0; i <= Console.WindowHeight; i++)
            {
                Console.WriteLine();
                Thread.Sleep(delay);
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        return;
                    }
                }
            }
        }
    }
}