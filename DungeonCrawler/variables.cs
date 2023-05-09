using static DungeonCrawler.Settings;
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

        public static bool DO_DEBUG = bool.Parse(GetConfigValue("roomDebugEnabled"));
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
    }


    public class Dialog
    {
        // Properties
        public string Text { get; set; }
        public string StartingText { get; set; }
        public string CurrentEmotion { get; set; }


        public Dictionary<string, char[][]> EnemyData { get; set; }

        public Dialog(string startingText, Dictionary<string, char[][]> enemyData, string emotion = "Happy", int aftertime = 3)
        {
            StartingText = startingText; EnemyData = enemyData; CurrentEmotion = emotion;
            Clear(emotion, afterTime:aftertime);
        }
        public void Write(string text, int writeTime, int afterTime = 3, string emotion = "")
        {
            if (emotion != "")
            {
                CurrentEmotion = emotion;
                Clear(emotion: emotion);
            }
            char[] charMap = text.ToCharArray();
            double threadTime = Convert.ToDouble(writeTime) / Convert.ToDouble(charMap.Length);
            for (int i = 0; i < charMap.Length; i++)
            {
                Console.Write(charMap[i]);
                Thread.Sleep(Convert.ToInt32(threadTime * 1000));
            }
            Text = Text + text;
            Thread.Sleep(afterTime * 1000);
        }
        public void Clear(string emotion = "", int afterTime = 0)
        {
            Text = "";
            if (emotion == "")
            {
                emotion = CurrentEmotion;
            }
            Console.Clear();
            char[] charMap = StartingText.ToCharArray();
            for (int i = 0; i < charMap.Length; i++)
            {
                Console.Write(charMap[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < EnemyData[emotion].Length; i++)
            {
                for (int j = 0; j < EnemyData[emotion][i].Length; j++)
                {

                    Console.Write(EnemyData[emotion][i][j]);
                }
                Console.WriteLine();
            }
            Thread.Sleep(afterTime * 1000);
        }
    }
}