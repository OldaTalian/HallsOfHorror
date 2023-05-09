using System.Threading.Tasks;
using static DungeonCrawler.Enemy;

using static DungeonCrawler.Variables;
using static DungeonCrawler.Sounds;
using System.Text;
using System.Data.Common;

namespace DungeonCrawler
{
    internal class Fight
    {
        private static object inputLock = new object();

        private static int enemyFightHealth = enemyHealth;
        private static int enemyFightHealthFull = enemyHealth;
        private static int enemycount;
        private static byte option = 1;
        private static string emotion = "Angry";

        public static void BeginFight( string sides)
        {
            StopMusic();
            PlaySound("fightStart.wav");
            int enemyCount = 0;
            for (int i = 0; i < sides.Length; i++)
            {
                if (sides[i] != 'N')
                    enemyCount++;
            }
            enemyFightHealthFull = enemyHealth;
            enemyFightHealth = enemyHealth;
            enemyFightHealth *= enemyCount;
            enemyFightHealthFull *= enemyCount;
            enemycount = enemyCount;
            FightInProgress = true;
            PlayMusic("fightScene.wav");
            PlayerAttack();


            StopMusic();
            PlayMusic("main_menu.mp3");
        }

        public static bool FightInProgress = true;

        public static void PlayerAttack()
        {
            Console.Clear();
            while (FightInProgress)
            {
                AttackRender();
                PlayerAttackControl();
                Thread.Sleep(1);
            }
            Console.WriteLine(CalculateDamage(cursorposition));
            
            Console.ReadKey();
        }
        private static int CalculateDamage(int cursorPos)
        {
            if (cursorPos < 10)
            {
                return 3;
            }
            else if (cursorPos <17)
            {
                return 12;
            }
            else if(cursorPos == 18)
            {
                return 30;
            }
            else if (cursorPos < 25)
            {
                return 12;
            }
            else
            {
                return 3;
            }

        }
        public static int cursorposition = 0;
        public static bool cursormotion = true;
        private static string[] selectionMenu =
        {
            "Attack",
            "Use items",
        };
        public static void AttackRender()
        {
            Console.Clear();
            for(int i =0;i<5;i++)
            {
                for (int j = 0; j < 36; j++)
                {
                    if (j < 10)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (j < 17)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (j < 18)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (j < 25)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;  
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    if (j == cursorposition)
                    {
                        Console.SetCursorPosition(Console.CursorLeft-1, Console.CursorTop);
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            if (cursorposition == 36)
            {
                cursormotion = false;
            }
            else if (cursorposition == 0)
            {
                cursormotion = true;
            }
            if(cursormotion)
            {
                cursorposition++;
            }
            else
            {
                cursorposition--;
            }
        }
        public static void PlayerAttackControl()
        {
            if (Console.KeyAvailable)
            {
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        FightInProgress = false;
                        break;
                }
            }
        }

        private static bool fightInProgress = true;
        
        private static void FightEnemyStats()
        {
            Console.WriteLine();
            Console.WriteLine(enemycount > 1 ? $"{enemycount} enemies" : "Enemy" + $" | Health : {enemyFightHealth}/{enemyFightHealthFull}");
            Console.WriteLine($"Your health : {playerHealth}");
        }
        private static void SelectionMenu(byte option)
        {
            for (int i = 0; i < selectionMenu.Length; i++)
            {
                if(i + 1 == option)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(selectionMenu[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.Write(selectionMenu[i]);
                }
                Console.Write(" ");
            }
        }
        private static void SelectOption(byte option, int enemyCount)
        {
            Random random = new Random();
            int damageMultiplier = random.Next(1, 3);
            Console.WriteLine(damageMultiplier);
            if (option == 1)
            {
                PlayerAttack();
            }
            else
            {
                //INVENTORY LOOOOOL
                return;
            }
        }
        private static void FightMenu()
        {
            bool typed = false;
            isMenuActive = true;
            while (isMenuActive && !typed)
            {
                Console.Clear();
                EnemyFace();
                SelectionMenu(option);
                ConsoleKey keyPressed = Console.ReadKey(true).Key;
                switch (keyPressed)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (option - 1 != 0)
                        {
                            option--;
                        }
                        typed = false;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if ((option + 1) <= selectionMenu.Length)
                        {
                            option++;
                        }
                        typed = false;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        SelectOption(option, enemycount);
                        isMenuActive = false;
                        typed = true;
                        break;
                    default:
                        typed = false;
                        break;
                }
            }
        }

        private static int time = 0;
        private static int timeAttack = 0;
        private static bool isMenuActive = true;
        private static void FightEnd(string sides, bool won)
        {
            if (sides[0] == 'u')
                Map[mainPlayerPos[1] - 1][mainPlayerPos[0]] = enemyLastTile[0]; //PESKY REPLACE BACKGROUND
            if (sides[1] == 'd')
                Map[mainPlayerPos[1] + 1][mainPlayerPos[0]] = enemyLastTile[0]; //PESKY REPLACE BACKGROUND
            if (sides[2] == 'l')
                Map[mainPlayerPos[1]][mainPlayerPos[0] - 1] = enemyLastTile[0]; //PESKY REPLACE BACKGROUND
            if (sides[3] == 'r')
                Map[mainPlayerPos[1]][mainPlayerPos[0] + 1] = enemyLastTile[0]; //PESKY REPLACE BACKGROUND 
        }
       

        static Dictionary<string, char[][]> EnemyEmotions = new Dictionary<string, char[][]>
        {
            { "Angry", new char[][] {

                " #     # ".ToCharArray(),
                "░█#   #█░".ToCharArray(),
                "         ".ToCharArray(),
                "  █████  ".ToCharArray(),
                " █     █ ".ToCharArray(),
            }},
            { "Happy", new char[][] {
                " ░     ░ ".ToCharArray(),
                "░█     █░".ToCharArray(),
                "         ".ToCharArray(),
                " █     █ ".ToCharArray(),
                "  █████  ".ToCharArray(),
            }}
        };
        public static void EnemyFace()
        {
            for (int i = 0; i < EnemyEmotions[emotion].Length; i++)
            {
                for (int j = 0; j < EnemyEmotions[emotion][i].Length; j++)
                {
                    Console.Write(EnemyEmotions[emotion][i][j]);
                }
                Console.WriteLine();
            }
        }
    }
    
}
