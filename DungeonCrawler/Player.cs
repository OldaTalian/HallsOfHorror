using static DungeonCrawler.Program;
using static DungeonCrawler.maps;
using static DungeonCrawler.variables;

namespace DungeonCrawler
{
    class Player
    {
        public static void Move() // posunutí hráče
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
                        NextRoom();
                        break;
                    }
                    else if (!CheckForTiles(playerPos, 'u', '█'))
                    {
                        MoveUp(x,y);
                    }
                    else
                    {
                        moved = false; // nepohnul se -> nic :D 
                    }
                    break;
                case ConsoleKey.DownArrow: // DOLŮ
                case ConsoleKey.S:
                    if (CheckForTiles(playerPos, 'd', '/'))
                    {
                        PrevRoom();
                        break;
                    }
                    else if (!CheckForTiles(playerPos, 'd', '█'))
                    {
                        MoveDown(x,y);
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
                        PrevRoom();
                        break;
                    }
                    else if (!CheckForTiles(playerPos, 'l', '█'))
                    {
                       MoveLeft(x,y);
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
                        NextRoom();
                        break;
                    }
                    else if (!CheckForTiles(playerPos, 'r', '█'))
                    {
                        MoveRight(x,y);
                    }
                    else
                    {
                        moved = false;
                    }
                    break;
            }
        }

        public static void MoveUp(int x, int y)
        {
            Map[y][x] = lastStepOn;
            lastStepOn = Map[y - 1][x];
            Map[y - 1][x] = player;
        }
        public static void MoveDown(int x, int y)
        {
            Map[y][x] = lastStepOn;
            lastStepOn = Map[y + 1][x];
            Map[y + 1][x] = player;
        }
        public static void MoveLeft(int x, int y)
        {
            Map[y][x] = lastStepOn;
            lastStepOn = Map[y][x - 1];
            Map[y][x - 1] = player;
        }
        public static void MoveRight(int x, int y)
        {
            Map[y][x] = lastStepOn;
            lastStepOn = Map[y][x + 1];
            Map[y][x + 1] = player;
        }

        public static void NextRoom(char tile = '░')//Další místnost wow
        {
            ThisRoom++;
            Map = AllRooms()[ThisRoom];
            lastStepOn = tile;
        }
        public static void PrevRoom(char tile = '░')//Předchozí místnost
        {
            ThisRoom--;
            Map = AllRooms()[ThisRoom];
            lastStepOn = tile;
        }

        public static int[] FindPlayer() // Vyhledá hráče n mapě a potom returne jeho souřadnice
        {
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

    }
}
