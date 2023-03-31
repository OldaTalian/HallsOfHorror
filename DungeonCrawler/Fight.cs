using System.Security.Cryptography.X509Certificates;
using static DungeonCrawler.Enemy;
using static DungeonCrawler.variables;

namespace DungeonCrawler
{
    internal class Fight
    {
        public static int enemyFightHealth = enemyHealth;
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
        public static void BeginFight()
        {
            FightRender(enemy);
            FightMenu();
        }

        public static void FightRender(char enemyType) 
        {
            Console.Clear();
            for (int i = 0; i < FightScene.Length; i++)
            {
                for (int j = 0; j < FightScene[i].Length; j++)
                {
                    if (FightScene[i][j] == '÷')
                    {
                        Console.Write(enemyHealth);
                    }
                    else if(FightScene[i][j] == '×')
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
        public static void FightMenu()
        {
            bool whileLoopGoBrrr = true;
            bool whileLoopGoBrrr2 = true;
            while (whileLoopGoBrrr) { 
                while (whileLoopGoBrrr2)
                {
                    FightRender(enemy);
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
                            SelectOption(option);
                            break;
                    }
                }
                if (enemyFightHealth <= 0)
                {
                    whileLoopGoBrrr = false;
                    Console.WriteLine("\nYou won the match");
                }
                else
                {
                    whileLoopGoBrrr2 = true;
                }
                if ( playerHealth <= 0)
                {
                    whileLoopGoBrrr = false;
                    Console.WriteLine("\nYou lost the match");
                }
                else
                {
                    whileLoopGoBrrr2 = true;
                }
            }
        }
        public static void SelectOption(byte option)
        {
            if(option==1)
            {
                playerHealth -= ((enemyAttack / 4) * 3);
                enemyFightHealth -= playerAttack;
            }
            else if (option == 2)
            {
                playerHealth -= (enemyAttack / 2);
            }
            else
            {
                //INVENTORY LOOOOOL
                return; 
            }
        }
    }
}
