﻿


namespace DungeonCrawler
{
    public class maps
    {
        public static char[][][] AllRooms() 
        {
            char[][][] output = { StartRoom, Room2, Room3 , Room4 ,endRoom };
            return output;
        }
        public static char[][] StartRoom =
        {
            "██████████████████████████████████████████████████████████  ".ToCharArray(),
            "█░░░░░░█░░░░░░░░░░☻░░░░░░░░░░░░░░░░░░░░░░░░░░░░☻░░$░░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░░░▒░░░░░░░░░▒░░░░░░░░░░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░░░░░░░░░░░░░░░░░░░░▒░░░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
           $"█░*░░▒░█░░░░░░░░░░░░░░░░☻░░░░░░░░░░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░░░░░░░░░░░░░░░░░░░░░░☻░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░░░░░░▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░☻░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░░☻░░░░░░░░░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░☻▒░░░░░░░░░░░░░░▒░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░░░░░░░░░░░░░░░░░☻░░░░░░░░░░░░░░░░░░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░█░▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒░░░░█░░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░░░░░░░░░☻░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█░░░░░░|".ToCharArray(),
            "█░░░░░░█░░░░░░░░░░░░░░░░░░░░░░░░░░░☻░░░░░░░░░░░☻░░$░░░░░░█".ToCharArray(),
            "██████████████████████████████████████████████████████████Đ9".ToCharArray(),
        };
        public static char[][] Room2 =
        {
            "██████████████-██████████████  ".ToCharArray(),
            "█░░░░░░░░░▒░░░░░░░░░░░░░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░▒░░░░░░░░▒░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░░░░░█".ToCharArray(),
           "\\*░░░░░░░░░░░░░░░░░░░░░░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░##░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░▒░░░░░###░░█".ToCharArray(),
            "█░░░▒░░░░░░░░░░░░░░░░░░░▒░░░█".ToCharArray(),
            "█████████████████████████████Đ3".ToCharArray(),
        };
        public static char[][] Room3 =
        {
            "████████████████████".ToCharArray(),
            "█▒░░░░░░░░░░░░░░░░░█".ToCharArray(),
            "█░░░░▒░░░░░░░░▒░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░|".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░█".ToCharArray(),
            "█░░░░░░░▒░░░░░░░░░░█".ToCharArray(),
            "█░░░*░░░░░░░░░░▒░░░█".ToCharArray(),
            "████/███████████████".ToCharArray(),
        };
        public static char[][] Room4 =
        {
            "████████████████████████████████████████████  ".ToCharArray(),
            "█░░░░░░▒░$$$$$$$$$$$$$$$$$$$$$$$$$$$$$░░░▒░█".ToCharArray(),
            "█░░▒░░░░░█████████████████████████████░▒░░░█".ToCharArray(),
            "█░░░░░░░░#░░░#░░░#░░░#░░░#░░░#░░░#░░░#░░░░░█".ToCharArray(),
            "█░▒░░░▒░░#░#░#░#░#░#░#░#░#░#░#░#░#░#░#░░▒░░|".ToCharArray(),
            "█░░░░░░░░░░#░░░#░░░#░░░#░░░#░░░#░░░#░░░░░░░█".ToCharArray(),
            "█░░▒░░░░▒█████████████████████████████░░░░▒█".ToCharArray(),
            "█░░░*░░░░$$$$$$$$$$$$$$$$$$$$$$$$$$$$$▒░░░░█".ToCharArray(),
            "████/███████████████████████████████████████Đ9".ToCharArray(),
        };
        public static char[][] endRoom =
{
            "████████████████████████████████████████████████████████████████████████████████████████████".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ░░░░░░░░░░░░░░░░░░░░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░█░░░░█░░░░█░░░░█░░░░█░░░░█░░░░█░░░░█░░░░░░░##░░░░░░░░░ ░░░░░░░░░░░░░#####░░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░####░░░░░░░░ ░░░░░░░░░░░░#░░░░░#░░░░░█".ToCharArray(),
           "\\*░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░######░░░░░░░░░░░░░░░░░░░#░░#░#░░#░░Ł░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░####░░░░░░░░ ░░░░░░░░░░░░#░░░░░#░░░░░█".ToCharArray(),
            "█░░░░░░█░░░░█░░░░█░░░░█░░░░█░░░░█░░░░█░░░░█░░░░█░░░░░░░##░░░░░░░░░ ░░░░░░░░░░░░#######░░░░░█".ToCharArray(),
            "█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ░░░░░░░░░░░░#░#░#░#░░░░░█".ToCharArray(),
            "████████████████████████████████████████████████████████████████████████████████████████████".ToCharArray(),
        };


        public static void AllPlayers()
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
