using static DungeonCrawler.maps;

namespace DungeonCrawler
{
    internal class variables
    {
        public static char player = '☺';
        public static char enemy = '☻';
        public static char lastStepOn = '░';
        public static int ThisRoom = 0;
        public static char[][] Map = StartRoom;
        public static bool moved = false;

        public static int[] locate = { 2, 15 }; //delete


        static char[] AddToArray(char[] array, char newValue)
        {
            char[] newArray = new char[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            newArray[newArray.Length - 1] = newValue;

            return newArray;
        }
    }
}
