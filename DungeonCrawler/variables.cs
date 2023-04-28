namespace DungeonCrawler
{
    internal class Variables
    {
        public static char player = '☺';
        public static char lastStepOn = '░';

        public static bool moved = false;
        public static int playerHealth = 100;
        public static byte playerAttack = 3;
        public static int[] mainPlayerPos = new int[2];

        public static char[][] Map = new char[0][];

        public static int ThisRoom = 0;
        public static long currentTick = 0;

        public static bool DO_DEBUG = false;
        public static bool DISABLE_AUDIO = true;
        // Useful functions

        // Adds a new value to the end of an array and returns the new array
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

        // Checks if there is a specific tile in a specific direction from the player's position
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

        // Calculates the Manhattan distance between two positions
        public static int MeasureDistance(int[] pos1, int[] pos2)
        {
            int x1 = pos1[0];
            int y1 = pos1[1];
            int x2 = pos2[0];
            int y2 = pos2[1];
            int distance = Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
            return distance;
        }

        // Checks if a specific tile is within a certain distance of a position
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