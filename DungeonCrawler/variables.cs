namespace DungeonCrawler
{
    internal class variables
    {
        public static char player = '☺';
        public static char enemy = '☻';
        public static char lastStepOn = '░';

        public static int ThisRoom = 0;
        public static char[][] Map = new char[0][];
        public static bool moved = false;
        public static long currentTick = 0;
        public static byte playerHealth = 100;
        public static byte enemyAttack = 5;

        public static int[] locate = { 2, 15 }; //delete

        // Useful functions
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

        public static bool CheckForTiles(int[] playerPos, char direction, char tile) // zjišťuje jestli je na dané straně od pozice nějaký Tile
        {
            bool output = false;
            int x = playerPos[0];
            int y = playerPos[1];
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

        public static int MeasureDistance(int[] pos1, int[] pos2)
        {
            int x1 = pos1[0];
            int y1 = pos1[1];
            int x2 = pos2[0];
            int y2 = pos2[1];
            int distance = Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
            return distance;
        }

        public static bool IsNear(char search, int[] pos, int distance)
        {
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)
                {
                    if (Map[i][j] == search)
                    {
                        int[] tilePos = new int[] { j, i };
                        if (MeasureDistance(pos, tilePos) <= distance)
                        {
                            return true;
                        }
                    }
                }
            }
            return false; // no tiles found near pos
        }

    }
}