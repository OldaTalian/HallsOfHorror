using static DungeonCrawler.maps;
namespace DungeonCrawler
{
    internal class Program
    {
        public static char player = '☺';
        public static char lastStepOn = '░';
        public static int ThisRoom = 0;
        public static char[][] Map = StartRoom;
        public static bool moved = false;
        public static int[] FindPlayer()
        {
            // Vyhledá hráče n mapě a potom returne jeho souřadnice
            int[] playerPos = { -1, -1 };
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)
                {
                    if (Map[i][j] == player)
                    {
                        playerPos[0] = j;
                        playerPos[1] = i;
                        return playerPos;
                    }
                }
            }
            return playerPos;
        }
        public static int MeasureDistance(int[] pos1, int[] pos2)
        {
            int x1 = pos1[0];
            int y1 = pos1[1];
            int x2 = pos2[0];
            int y2 = pos2[1];
            int distance = (x2 - x1)+(y2 - y1);
            return distance;
        }

        public static bool CheckForTiles(int[] playerPos, char direction, char tile)
        {
            bool output = false;
            int x = playerPos[0];
            int y = playerPos[1];
            if (direction== 'u') // UP
            {
                if(y != 0 && Map[y - 1][x] == tile)
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
        public static void Move()
        {
            int[] playerPos = FindPlayer(); // Zjistím  souřadnice hráče
            moved = true;
            int x = playerPos[0]; // z nich zjistím X
            int y = playerPos[1]; // a Y
            switch (Console.ReadKey(true).Key) // a zjistím klávesu, podle které potom posunu hráče
            {
                case ConsoleKey.UpArrow: // NAHORU
                case ConsoleKey.W:
                    if (CheckForTiles(playerPos, 'u', '-'))
                    {
                        ThisRoom++;
                        Map = AllRooms()[ThisRoom];
                        lastStepOn = '░';
                        break;
                    }
                    if (!CheckForTiles(playerPos,'u', '█'))
                    {
                        Map[y][x] = lastStepOn; // nastavuji to z čeho jsem přišel zpátky na to co tam bylo
                        lastStepOn = Map[y - 1][x]; // ukládám si na co šlapu
                        Map[y-1][x] = player; // posunu hráče
                    }
                    else
                    {
                        moved = false;
                    }
                    break;
                case ConsoleKey.DownArrow: // DOLŮ
                case ConsoleKey.S:
                    if (CheckForTiles(playerPos, 'd', '/'))
                    {
                        ThisRoom--;
                        Map = AllRooms()[ThisRoom];
                        lastStepOn = '░';
                        break;
                    }
                    else if (!CheckForTiles(playerPos, 'd', '█'))
                    {
                        Map[y][x] = lastStepOn;
                        lastStepOn = Map[y + 1][x];
                        Map[y + 1][x] = player;
                    }
                    else
                    {
                        moved = false;
                    }
                    break;
                case ConsoleKey.LeftArrow: // DOLEVA
                case ConsoleKey.A:
                    if (CheckForTiles(playerPos, 'l', '\\'))
                    {
                        ThisRoom--;
                        Map = AllRooms()[ThisRoom];
                        lastStepOn = '░';
                        break;
                    }
                    else if (!CheckForTiles(playerPos, 'l', '█'))
                    {
                        Map[y][x] = lastStepOn;
                        lastStepOn = Map[y][x - 1];
                        Map[y][x - 1] = player;
                    }
                    else
                    {
                        moved = false;
                    }
                    break;
                case ConsoleKey.RightArrow: // DOPRAVA
                case ConsoleKey.D:
                    if (CheckForTiles(playerPos, 'r', '|'))
                    {
                        ThisRoom++;
                        Map = AllRooms()[ThisRoom];
                        lastStepOn = '░';
                        break;
                    }
                    else if (!CheckForTiles(playerPos, 'r', '█'))
                    {
                        Map[y][x] = lastStepOn;
                        lastStepOn = Map[y][x + 1];
                        Map[y][x + 1] = player;
                    }
                    else
                    {
                        moved = false;
                    }
                    break;
            }
        }
            public static int[] locate = { 2, 15 };
        public static void Render()
        {
            Console.Clear();
            if (Map == null) return;

            Console.WriteLine(ThisRoom+1 + " " + AllRooms().Length + " |  x:" + FindPlayer()[0] + " y:" + FindPlayer()[1] + " D: " + MeasureDistance(FindPlayer(),locate)); // Vypisuje v jaké místnosti z kolika hráč je (pro debug)
            
            // Vytiskne Mapu do konzole
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)
                {
                    if (Map[i][j] == '░' || Map[i][j] == '▒') {
                        Console.ForegroundColor= ConsoleColor.DarkGray;
                        Console.Write(Map[i][j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(Map[i][j]);
                    }
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Map[locate[0]][locate[1]] = 'E';
            AllPlayers();//změním všechny * na ☺
            Render();
            while (true) // Hra: 
            {
                Console.SetWindowSize(Map[0].Length + 1,Map.Length + 2); // Změní velikost okna aby nešly vidět předchozí pohyby
                do
                {
                    Move();
                } while (!moved);
                Render();
            }

        }
    
    }
}
