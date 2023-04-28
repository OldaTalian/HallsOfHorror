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
        private static int[] fightPlayerPos = { 4, 5 };

        private static char[][] fightMap =
        {
            "█████████████".ToCharArray(),
            "█           █".ToCharArray(),
            "█           █".ToCharArray(),
            "█           █".ToCharArray(),
            "█           █".ToCharArray(),
            "█           █".ToCharArray(),
            "█           █".ToCharArray(),
            "█████████████".ToCharArray(),
        };

        private static byte option = 1;
        private static string emotion = "Angry";

        public static void BeginFight( string sides)
        {
            StopMusic();
            PlaySound("fightStart.wav", 80 ); //////////////////// VOLUME
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
            Dialog enemyAngry = new Dialog("You've encountered an enemy", EnemyEmotions, emotion, 1);
            PlayMusic("fightScene.wav" , 30);
            enemyAngry.Write("I dont like you",1,1);


            Fighting(sides);


            StopMusic();
            PlayMusic("main_menu.mp3");
        }



        private static string[] selectionMenu =
        {
            "Attack",
            "Use items",
        };

        private static bool fightInProgress = true;
        public static void Fighting(string sides)
        {
            Console.Clear();
            Dialog angryEnemy = new Dialog("",EnemyEmotions, emotion, 0);
            angryEnemy.Clear(emotion);
            fightInProgress = true;
            while (fightInProgress)
            {
                // Render enemy stats
                FightEnemyStats();
                // Render menu
                FightMenu();
                if (enemyFightHealth <= 0)
                {
                    fightInProgress = false;
                    FightEnd(sides, true);
                    Console.ReadKey();
                }
                // Enemy attack
                FightEnemyAttack();
                if (playerHealth <= 0)
                {
                    fightInProgress = false;
                    FightEnd(sides, false);
                    Console.ReadKey();
                }
                if (enemyFightHealth <= 0)
                {
                    fightInProgress = false;
                    FightEnd(sides, true);
                    Console.ReadKey();
                }
            }
        }
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
                enemyFightHealth -= playerAttack * damageMultiplier;
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
                FightRender();
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
        private static bool enemyAttacks = true;
        private static bool isMenuActive = true;
        private static void FightEnemyAttack()
        {
            time = 0;
            timeAttack = 0;
            enemyAttacks = true;
            Console.Clear();

            fightMove = true;
            while (enemyAttacks)
            {
                Thread renderThread = new Thread(new ThreadStart(FightRender));
                Thread moveThread = new Thread(new ThreadStart(FightMove));
                FightEnemyCalcs();
                if (fightMap[fightPlayerPos[0]][fightPlayerPos[1]] == '#' || fightMap[fightPlayerPos[0]][fightPlayerPos[1]] == '*')
                {
                    playerHealth -= 1;
                }
                if (playerHealth <= 0)
                {
                    enemyAttacks = false;
                }
                renderThread.Start();
                moveThread.Start();
                Thread.Sleep(50);
                time += 50;
                timeAttack += 50;
            }
        }

        private static void FightEnemyCalcs()
        {
            Random rdn = new Random();
            int safe = rdn.Next(2, fightMap.Length - 2);
            int attackType = rdn.Next(1, 3);


            // SPAWN ATTACK 
            if (time < 50)
            {
                // FALLING #
                if (attackType == 1)
                {
                    for (int i = 1; i < fightMap[1].Length; i++)
                    {
                        if (i != fightMap[1].Length - 1 && i != safe)
                        {
                            fightMap[1][i] = '#';
                        }
                    }
                }
                // METEORS
                else if (attackType == 2)
                {
                    int column = rdn.Next(1, fightMap[0].Length - 1);
                    fightMap[1][column] = '*';
                }
                timeAttack = 0;
            }

            // Stop enemy turn
            if (time > 900)
            {
                enemyAttacks = false;
                fightMove = false;
            }

            // Calculate Attacks

            for (int i = 1; i < fightMap.Length - 1; i++)
            {
                for (int j = 1; j < fightMap[i].Length - 1; j++)
                {
                    if (fightMap[i - 1][j] == '*' && fightMap[i][j] == ' ')
                    {
                        fightMap[i][j] = '*';
                        fightMap[i - 1][j] = ' ';
                        i++;
                    }
                    else if (fightMap[i - 1][j] == '#' && fightMap[i][j] == ' ')
                    {
                        fightMap[i][j] = '#';
                        fightMap[i - 1][j] = ' ';
                        if(j == fightMap[i].Length - 2)
                        {
                            i++;
                        }
                    }
                    else if (fightMap[i - 1][j] == '#' && fightMap[i][j] == '#')
                    {
                        fightMap[i - 1][j] = ' ';
                    }
                }
            }

            // REMOVE LAST LAYER
            if(timeAttack < 300)
            {
                for (int i = 1; i < fightMap[fightMap.Length - 2].Length; i++)
                {
                    if (i != fightMap[fightMap.Length - 2].Length - 1 && i != safe)
                    {
                        fightMap[fightMap.Length - 2][i] = ' ';
                    }
                }
            }
        }
        private static bool fightMove = true;
        private static void FightMove()
        {
            if (Console.KeyAvailable && !isMenuActive)
            {
                ConsoleKey keyPressed;
                lock (inputLock)
                {
                    keyPressed = Console.ReadKey(true).Key;
                }
                switch (keyPressed)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                    if (fightMap[fightPlayerPos[0] - 1][fightPlayerPos[1]] != '█')
                        fightPlayerPos[0]--;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                    if (fightMap[fightPlayerPos[0] + 1][fightPlayerPos[1]] != '█')
                        fightPlayerPos[0]++;
                    break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                    if (fightMap[fightPlayerPos[0]][fightPlayerPos[1] - 1] != '█')
                        fightPlayerPos[1]--;
                    break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                    if (fightMap[fightPlayerPos[0]][fightPlayerPos[1] + 1] != '█')
                        fightPlayerPos[1]++;
                    break;
            }
            }
            else
            {
                return;
            }

        }
        private static void FightRender()
        {
            Console.Clear();
            FightEnemyStats();
            for (int i = 0; i < fightMap.Length; i++)
            {
                for (int j = 0; j < fightMap[i].Length; j++)
                {
                    if (i == fightPlayerPos[0] && j == fightPlayerPos[1])
                    {
                        Console.Write(player);
                    }
                    else if (fightMap[i][j] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(fightMap[i][j]);
                        Console.ForegroundColor= ConsoleColor.White;
                    }
                    else if (fightMap[i][j] == '*')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(fightMap[i][j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(fightMap[i][j]);
                    }
                }
                Console.WriteLine();
            }
        }
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
