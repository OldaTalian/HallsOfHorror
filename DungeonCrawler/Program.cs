namespace DungeonCrawler
{
    internal class Program
    {
        public static void Move()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow: // NAHORU
                case ConsoleKey.W:
                    Console.WriteLine("up");
                    break;
                case ConsoleKey.DownArrow: // DOLŮ
                case ConsoleKey.S:
                    Console.WriteLine("down");
                    break;
                case ConsoleKey.LeftArrow: // DOLEVA
                case ConsoleKey.A:
                    Console.WriteLine("left");
                    break;
                case ConsoleKey.RightArrow: // DOPRAVA
                case ConsoleKey.D:
                    Console.WriteLine("right");
                    break;
            }
        }
        static void Main(string[] args)
        {

                
            while(true)
            {
                Move();
            }
            
        }
    
    }
}
