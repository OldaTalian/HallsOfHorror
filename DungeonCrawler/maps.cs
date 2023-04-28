using System.Configuration;
using System.Text;

namespace DungeonCrawler
{
    public class maps
    {
        public static char[][][] AllRooms()
        {
            char[][][] output = { DebugRoom, StartRoom, Room2, Room3, Room4, endRoom };
            return output;
        }

        public static void RevertOriginalMaps()
        {
            DebugRoom = ConfigurationManager.AppSettings["DebugRoom"]
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            StartRoom = ConfigurationManager.AppSettings["StartRoom"]
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room2 = ConfigurationManager.AppSettings["Room2"]
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room3 = ConfigurationManager.AppSettings["Room3"]
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            Room4 = ConfigurationManager.AppSettings["Room4"]
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
            endRoom = ConfigurationManager.AppSettings["endRoom"]
              .Split('\n', StringSplitOptions.RemoveEmptyEntries)
              .Select(row => row.Trim().ToArray())
              .ToArray();
    }
        public static char[][] DebugRoom = ConfigurationManager.AppSettings["DebugRoom"]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] StartRoom = ConfigurationManager.AppSettings["StartRoom"]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room2 =     ConfigurationManager.AppSettings["Room2"    ]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room3 =     ConfigurationManager.AppSettings["Room3"    ]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] Room4 =     ConfigurationManager.AppSettings["Room4"    ]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();
        public static char[][] endRoom =   ConfigurationManager.AppSettings["endRoom"  ]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim().ToArray())
            .ToArray();

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
                            AllRooms()[i][j][k] = '☺';
                        }
                    }
                }
            }
        }
    }
}
