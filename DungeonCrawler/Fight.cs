using System.Security.Cryptography.X509Certificates;
using static DungeonCrawler.Enemy;
using static DungeonCrawler.Variables;

namespace DungeonCrawler
{
    internal class Fight
    {
        public static int enemyFightHealth = enemyHealth;
        public static int enemyFightHealthFull = enemyHealth;
        public static char[][] FightScene =
        {
            "################################################".ToCharArray(),
           $"# You've encountered a enemy #                 #".ToCharArray(),
              "# Health ÷ / ×          ###                  #".ToCharArray(),
            "##########################                     #".ToCharArray(),
            "#                                              #".ToCharArray(),
            "#                                              #".ToCharArray(),
            "#                                              #".ToCharArray(),
            "#                                              #".ToCharArray(),
            "#                                              #".ToCharArray(),
            "#                                              #".ToCharArray(),
            "#                                      #########".ToCharArray(),
            "#                                   ###   Đ  #".ToCharArray(),
            "################################################".ToCharArray()
        };

        public static byte option = 1;
        public static void BeginFight( string sides)
        {
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
            FightRender(enemy, enemyCount);
            FightMenu(enemyCount, sides);

        }

        public static void FightRender(char enemyType, int enemyCount) 
        {
            
            Console.Clear();
            for (int i = 0; i < FightScene.Length; i++)
            {
                for (int j = 0; j < FightScene[i].Length; j++)
                {
                    if (FightScene[i][j] == '×')
                    {
                        Console.Write(enemyFightHealthFull);
                    }
                    else if(FightScene[i][j] == '÷')
                    {
                        Console.Write(enemyFightHealth);
                    }
                    else if (FightScene[i][j] == 'Đ')
                    {
                        Console.Write(playerHealth);
                    }
                    else
                    {
                        Console.Write(FightScene[i][j]);
                    }
                }
                Console.WriteLine();
            }
        }
        public static string[] selectionMenu =
        {
            "Attack",
            "Defend",
            "Use items",
        };
        public static void SelectionMenu(byte option)
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
        public static void FightMenu(int enemyCount , string sides)
        {
            bool whileLoopGoBrrr = true;
            bool whileLoopGoBrrr2 = true;
            while (whileLoopGoBrrr) { 
                while (whileLoopGoBrrr2)
                {
                    FightRender(enemy, enemyCount);
                    SelectionMenu(option);
                    ConsoleKey keyPressed = Console.ReadKey(true).Key;

                    switch (keyPressed)
                    {
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.A:
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            if(option - 1 != 0)
                            {
                                option--;
                            }
                            break;

                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            if ((option + 1) <= selectionMenu.Length)
                            {
                                option++;
                            }
                            break;
                        case ConsoleKey.Enter:
                        case ConsoleKey.Spacebar:
                            whileLoopGoBrrr2 = false;
                            SelectOption(option , enemyCount);
                            break;
                    }
                }
                if (enemyFightHealth <= 0)
                {
                    whileLoopGoBrrr = false;
                    Console.Clear();
                    Console.WriteLine("\n\n\nYou WON the match");
                    Console.WriteLine("Press something to continue...");
                    Console.ReadKey();
                }
                else if ( playerHealth <= 0)
                {
                    whileLoopGoBrrr = false;
                    Console.Clear();
                    Console.WriteLine("\n\n\nYou LOST the match");
                    Console.ReadKey();
                }
                else
                {
                    whileLoopGoBrrr2 = true;
                }
            }
            if (sides[0] == 'u')
                Map[mainPlayerPos[1] - 1][mainPlayerPos[0]] = enemyLastTile[0]; //PESKY REPLACE BACKGROUND
            if (sides[1] == 'd')
                Map[mainPlayerPos[1] + 1][mainPlayerPos[0]] = enemyLastTile[0]; //PESKY REPLACE BACKGROUND
            if (sides[2] == 'l')
                Map[mainPlayerPos[1]][mainPlayerPos[0] - 1] = enemyLastTile[0]; //PESKY REPLACE BACKGROUND
            if (sides[3] == 'r')
                Map[mainPlayerPos[1]][mainPlayerPos[0] + 1] = enemyLastTile[0]; //PESKY REPLACE BACKGROUND 
        }
        public static void SelectOption(byte option, int enemyCount)
        {
            Random random = new Random();
            int damageMultiplier = random.Next(1,3);
            Console.WriteLine(damageMultiplier);
            if (option==1)
            {
                playerHealth -= ((((enemyAttack * enemyCount) / 4) * damageMultiplier));
                enemyFightHealth -= playerAttack * damageMultiplier;
            }
            else if (option == 2)
            {
                playerHealth -= ((enemyAttack * enemyCount) / 3) * damageMultiplier;
            }
            else
            {
                //INVENTORY LOOOOOL
                return; 
            }
        }
    }
}
