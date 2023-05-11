using System.Configuration; // (external)
using static DungeonCrawler.Variables;

namespace DungeonCrawler
{
    public class maps
    {
        private static char[][][] lastShuffledRooms;

        /// <returns>All maps combined into one char[][][]</returns>
        public static char[][][] AllRooms()
        {
            char[][][] output = { DebugRoom, StartRoom, Room1, Room2, Room3, Room4, Room5, Room6, Room7, Room8, endRoom };
            return output;
        }

        /// <summary>
        /// Shufles maps
        /// CZ:
        /// Zamíchá mapy
        /// </summary>
        /// <param name="shuffle"></param>
        /// <returns>char[][][] of Room1-10 shuffled</returns>
        public static char[][][] RandomMaps(bool shuffle = false)
        {
            if (shuffle || lastShuffledRooms == null)
            {
                char[][][] allRooms = AllRooms();
                char[][][] middleRooms = new char[allRooms.Length - 3][][];

                Array.Copy(allRooms, 2, middleRooms, 0, middleRooms.Length); // Dont shuffle the start and end rooms; CZ: Nemíchá Startovací a končící místnosti

                // Fisher-Yates shuffle algorithm
                // source: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm
                Random random = new Random();
                for (int i = middleRooms.Length - 1; i > 0; i--)
                {
                    int j = random.Next(i + 1);
                    char[][] temp = middleRooms[i];
                    middleRooms[i] = middleRooms[j];
                    middleRooms[j] = temp;
                }

                lastShuffledRooms = new char[allRooms.Length][][];
                lastShuffledRooms[0] = allRooms[0];
                lastShuffledRooms[1] = allRooms[1];
                Array.Copy(middleRooms, 0, lastShuffledRooms, 2, middleRooms.Length); // Merges all rooms together
                lastShuffledRooms[lastShuffledRooms.Length - 1] = allRooms[allRooms.Length - 1];
            }
            return lastShuffledRooms;
        }

        /// <summary>
        /// Reverts the maps to original state before game (also reloads maps)
        /// CZ:
        /// Nastaví mapy do původního stavu
        /// </summary>
        public static void RevertOriginalMaps()
        {
            DebugRoom = ConfigurationManager.AppSettings["DebugRoom"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            StartRoom = ConfigurationManager.AppSettings["StartRoom"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room1 = ConfigurationManager.AppSettings["Room1"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room2 = ConfigurationManager.AppSettings["Room2"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room3 = ConfigurationManager.AppSettings["Room3"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room4 = ConfigurationManager.AppSettings["Room4"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room5 = ConfigurationManager.AppSettings["Room5"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room6 = ConfigurationManager.AppSettings["Room6"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room7 = ConfigurationManager.AppSettings["Room7"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room8 = ConfigurationManager.AppSettings["Room8"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            endRoom = ConfigurationManager.AppSettings["endRoom"].Replace("\r\n", "\n")
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
    }
        public static char[][] DebugRoom = ConfigurationManager.AppSettings["DebugRoom"].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] StartRoom = ConfigurationManager.AppSettings["StartRoom"].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room1 = ConfigurationManager.AppSettings["Room1"].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room2 =     ConfigurationManager.AppSettings["Room2"    ].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room3 =     ConfigurationManager.AppSettings["Room3"    ].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room4 =     ConfigurationManager.AppSettings["Room4"    ].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room5 = ConfigurationManager.AppSettings["Room5"].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room6 = ConfigurationManager.AppSettings["Room6"].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room7 = ConfigurationManager.AppSettings["Room7"].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room8 = ConfigurationManager.AppSettings["Room8"].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] endRoom =   ConfigurationManager.AppSettings["endRoom"  ].Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();

        /// <summary>
        /// Changes all * to player
        /// CZ:
        /// Nastaví všechny * na hráče
        /// </summary>
        public static void RegisterSpawns()
        {
            for (int i = 0; i < AllRooms().Length; i++)
            {
                for (int j = 0; j < AllRooms()[i].Length; j++)
                {
                    for (int k = 0; k < AllRooms()[i][j].Length; k++)
                    {
                        if (AllRooms()[i][j][k] == '*')
                        {
                            AllRooms()[i][j][k] = player;
                        }
                    }
                }
            }
        }
    }
}
