using static DungeonCrawler.Program;
using static DungeonCrawler.variables;
using static DungeonCrawler.Player;
using System;

namespace DungeonCrawler
{
    internal class Enemy
    {
        public static int AllEnemies(char[][] array, char target)
        {
            int count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == target)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int[][] FindEnemyPositions(char[][] array, char target)
        {
            int[][] result = new int[AllEnemies(array,target)][];
            int index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == target)
                    {
                        result[index] = new int[] { i, j };
                        index++;
                    }
                }
            }

            return result;
        }

        public static char[] enemyLastTile = new char[AllEnemies(Map, enemy)];
         

        public static void MoveEnemy()
        {
            int[] playerPos = FindPlayer();
            int playerX = playerPos[0];
            int playerY = playerPos[1];
            

            for (int i = 0; i < AllEnemies(Map, enemy); i++)
            {
                int direction;
                //string Debug;
                int[] enemyPos = FindEnemyPositions(Map, enemy)[i];
                int enemyX = enemyPos[1];
                int enemyY = enemyPos[0];
                if (Math.Abs(enemyX - playerX) > Math.Abs(enemyY - playerY))
                {
                    //Debug = "X";
                    if ((enemyX - playerX) > 0)
                    {
                        direction = -1;
                    }
                    else
                    {
                        direction = 1;
                    }
                    if (Map[enemyY][enemyX + direction] != '█' && Map[enemyY][enemyX + direction] != player && Map[enemyY][enemyX + direction] != enemy)
                    {
                        Map[enemyY][enemyX] = (enemyLastTile[i] != ' ') ? enemyLastTile[i] : '░';
                        enemyLastTile[i] = Map[enemyY][enemyX + direction];
                        Map[enemyY][enemyX + direction] = enemy;
                    }
                }
                else
                {
                    //Debug = "Y";
                    if((enemyY - playerY) > 0)
                    {
                        direction = -1;
                    }
                    else
                    {
                        direction = 1;
                    }
                    if(Map[enemyY + direction][enemyX] != '█' && Map[enemyY + direction][enemyX] != player && Map[enemyY + direction][enemyX] != enemy)
                    {
                        Map[enemyY][enemyX] = (enemyLastTile[i] != ' ') ? enemyLastTile[i] : '░';
                        enemyLastTile[i] = Map[enemyY + direction][enemyX];
                        Map[enemyY + direction][enemyX] = enemy;
                    }
                }
                //Console.WriteLine($"Enemy{i}: X{enemyX} Y{enemyY}; Distance {(enemyX - playerX)} {(enemyY - playerY)} Direction:{Debug}{direction} {(Math.Abs(enemyX - playerX) < Math.Abs(enemyY - playerY))}");

            }
        }
    }
}
