using static DungeonCrawler.maps;

namespace DungeonCrawler
{
    internal class variables
    {
        public static char player = '☺';
        public static char lastStepOn = '░';
        public static int ThisRoom = 0;
        public static char[][] Map = StartRoom;
        public static bool moved = false;

        public static int[] locate = { 2, 15 }; //delete

    }
}
