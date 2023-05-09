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

        public static int enemyFightHealth = enemyHealth;
        public static int enemyFightHealthFull = enemyHealth;
        private static int allEnemyDamage = enemyAttack;
        private static int enemycount = 1;
        private static byte option = 1;
        private static string emotion = "Angry";

        public static void BeginFight( string sides, int healthMultiplier = 1)
        {
            StopMusic();
            PlaySound("assets/fightStart.wav");
            int enemyCount = 0;
            for (int i = 0; i < sides.Length; i++)
            {
                if (sides[i] != 'N')
                    enemyCount++;
            }
            enemyFightHealthFull = enemyHealth * healthMultiplier;
            enemyFightHealth = enemyFightHealthFull;
            enemyFightHealth *= enemyCount;
            enemyFightHealthFull *= enemyCount;
            allEnemyDamage *= enemyCount;
            enemycount = enemyCount;
            PlayMusic("assets/fightScene.wav");
            FightInProgress = true;
            PlayerAttack(allEnemyDamage);
            FightEnd(sides, playerHealth > 0);

            StopMusic();
            PlayMusic("assets/main_menu.mp3");
        }

        public static bool FightInProgress = true;
        public static bool playerTurn = true;

        public static void PlayerAttack(int damage = 10)
        {
            Console.Clear();
            playerTurn = true;
            FightInProgress = true;
            while (FightInProgress)
            {
                while (playerTurn)
                {
                    Console.Clear();
                    FightEnemyStats();
                    AttackRender();
                    PlayerAttackControl();
                    Thread.Sleep(Math.Abs(CalculateDamage(cursorposition) - 35));
                }
                Thread.Sleep(100);
                Console.Clear();
                enemyFightHealth -= CalculateDamage(cursorposition);
                FightEnemyStats();
                Console.WriteLine("\n\n\n");
                getCursorToCenter(42, false);
                Console.WriteLine($"You've hit enemy with {CalculateDamage(cursorposition)} points of damage");
                cursormotion = true;
                cursorposition = 0;
                Console.ReadKey();
                if (enemyFightHealth <= 0)
                {
                    return;
                }
                // ENEMY TURN
                Console.Clear();
                Random random = new Random();
                int enemyHit = random.Next(2*enemycount,damage);
                playerHealth -= enemyHit;
                FightEnemyStats();
                Console.WriteLine("\n\n\n");
                PlaySound("assets/hitHurt.wav", random.Next(-10, 10));
                getCursorToCenter(42, false);
                Console.WriteLine($"Enemy hit you with {enemyHit} points of damage");
                Console.ReadKey();
                playerTurn = true;
                if (playerHealth <= 0)
                {
                    return;
                }
            }
            
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

        public static void AttackRender()
        {
            for(int i =0;i<5;i++)
            {
                getCursorToCenter(36,false);
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
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
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
                        playerTurn = false;
                        break;
                }
            }
        }
        
        private static void FightEnemyStats()
        {
            getCursorToCenter(25,false);
            Console.WriteLine(enemycount > 1 ? $"{enemycount} enemies" : "Enemy" + $" - Health : {enemyFightHealth}/{enemyFightHealthFull}");
            getCursorToCenter(15,false);
            Console.WriteLine($"Your health : {playerHealth}");
        }
        
        public static void FightEnd(string sides, bool won)
        {
            if (won)
            {
                Console.Clear();
                getCursorToCenter(20);
                Console.WriteLine("You won the FIGHT");
                Console.ReadKey();
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
    }
    
}
