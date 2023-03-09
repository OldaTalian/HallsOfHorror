namespace DungeonCrawler
{
    internal class Program
    {
        public static char[][] Map =
        {
            // Test mapa.... # = zeď
            "          #     #    ".ToCharArray(),
            "                     ".ToCharArray(),
            "    #     X          ".ToCharArray(),
            "                     ".ToCharArray(),
            "                 #   ".ToCharArray(),
            "                     ".ToCharArray(),
            "   #                 ".ToCharArray(),
            "          #          ".ToCharArray(),
            "                     ".ToCharArray(),
        };
        public static int[] FindPlayer()
        {
            // Vyhledá hráče n mapě a potom returne jeho souřadnice
            int[] playerPos = { -1, -1 };
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)
                {
                    if (Map[i][j] == 'X')
                    {
                        playerPos[0] = j;
                        playerPos[1] = i;
                        return playerPos;
                    }
                }
            }
            return playerPos;
        }
        public static void Move()
        {
            int[] playerPos = FindPlayer(); // Zjistím  souřadnice hráče
            int x = playerPos[0]; // z nich zjistím X
            int y = playerPos[1]; // a Y
            switch (Console.ReadKey(true).Key) // a zjistím klávesu, podle které potom posunu hráče
            {
                case ConsoleKey.UpArrow: // NAHORU
                case ConsoleKey.W:
                    if (y != 0 && Map[y-1][x] != '#')
                    {
                        Map[y][x] = ' ';
                        Map[y-1][x] = 'X';
                    }
                    break;
                case ConsoleKey.DownArrow: // DOLŮ
                case ConsoleKey.S:
                    if (y != (Map.Length - 1) && Map[y + 1][x] != '#')
                    {
                        Map[y][x] = ' ';
                        Map[y + 1][x] = 'X';
                    }
                    break;
                case ConsoleKey.LeftArrow: // DOLEVA
                case ConsoleKey.A:
                    if (x != 0 && Map[y][x - 1] != '#')
                    {
                        Map[y][x] = ' ';
                        Map[y][x - 1] = 'X';
                    }
                    break;
                case ConsoleKey.RightArrow: // DOPRAVA
                case ConsoleKey.D:
                    if (x != (Map[y].Length) && Map[y][x + 1] != '#')
                    {
                        Map[y][x] = ' ';
                        Map[y][x + 1] = 'X';
                    }
                    break;
            }
        }
        public static void Render()
        {
            // Vytiskne Mapu do konzole
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)
                {
                    Console.Write(Map[i][j]);
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Render();
            Console.SetWindowSize(Map[0].Length + 2,Map.Length + 1); // Změní velikost okna aby nešly vidět předchozí pohyby
            while (true) // Hra: 
            {
                Move();
                Render();
            }

        }
    
    }
}
