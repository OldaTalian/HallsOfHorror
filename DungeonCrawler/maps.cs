using System;
using System.Configuration; // (external)
using static DungeonCrawler.Variables;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonCrawler
{
    public class maps
    {
        private static char[][][] lastShuffledRooms;

        /// <returns>All maps combined into one char[][][]</returns>
        public static char[][][] AllRooms = new char[0][][];

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
                char[][][] allRooms = AllRooms;
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
            List<char[][]> output = new List<char[][]>();

            for (int i = 0; i < ConfigurationManager.AppSettings.AllKeys.Length; i++)
            {
                if (ConfigurationManager.AppSettings.AllKeys[i].Contains("Room"))
                {
                    // Get the room string from the config
                    string roomString = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings.AllKeys[i]];

                    // Convert the room string to char[][]
                    char[][] room = roomString.Replace("\r\n", "\n")
                        .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                        .Select(row => row.Trim().ToArray())
                        .ToArray();

                    // Add the room to the output list
                    output.Add(room);
                }
            }
            AllRooms = output.ToArray();
        }

        /// <summary>
        /// Changes all * to player
        /// CZ:
        /// Nastaví všechny * na hráče
        /// </summary>
        public static void RegisterSpawns()
        {
            for (int i = 0; i < AllRooms.Length; i++)
            {
                for (int j = 0; j < AllRooms[i].Length; j++)
                {
                    for (int k = 0; k < AllRooms[i][j].Length; k++)
                    {
                        if (AllRooms[i][j][k] == '*')
                        {
                            AllRooms[i][j][k] = player;
                        }
                    }
                }
            }
        }
    }
}
