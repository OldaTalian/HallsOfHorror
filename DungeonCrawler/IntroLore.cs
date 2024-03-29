﻿using static DungeonCrawler.Variables;

namespace DungeonCrawler
{
    internal class IntroLore
    {
        private static ConsoleColor DefaultColor = ConsoleColor.White;

        /// <summary>
        /// Plays the whole Intro sequence
        /// CZ:
        /// Přehraje intro
        /// </summary>
        public static void StartIntro()
        {
            for (int i = 0; i <= 33; i++)
            {
                Console.Clear();
                RenderFrame(i, 90);
                if (Console.KeyAvailable && (Console.ReadKey().Key == ConsoleKey.Enter))
                {
                    RenderIntroText();
                    return;
                }
            }
            Console.Clear();
            DefaultColor = ConsoleColor.Gray;
            RenderFrame(34, 150);
            Console.Clear();
            DefaultColor = ConsoleColor.DarkGray;
            RenderFrame(35, 200);
            Console.Clear();
            DefaultColor = ConsoleColor.White;
            RenderFrame(36, 300);
            Console.Clear();
            Console.ForegroundColor = DefaultColor;
            RenderIntroText();
        }

        /// <summary>
        /// Converts individual pixels represented by characters into their corresponding color and shape.
        /// CZ: 
        /// Převede jednotlivé obrazové body reprezentované znaky na jejich odpovídající barvu a tvar.
        /// </summary>
        /// <param name="input">The character to be rendered as a pixel.</param>
        private static void RenderPixel(char input)
        {
            if (input == '█'|| input == '▄' || input == '▀' || input == '▌' || input == '▐')
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(input);
                Console.ForegroundColor = DefaultColor;
            }
            else if (input == '#')
            {
                Console.Write('█');
            }
            else if (input == '_')
            {
                Console.Write('▄');
            }
            else if (input == '|')
            {
                Console.Write('▐');
            }
            else if (input == '2')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write('▄');
                Console.ForegroundColor = DefaultColor;
            }
            else if (input == '9')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write('█');
                Console.ForegroundColor = DefaultColor;
            }
            else if (input == '7')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write('▀');
                Console.ForegroundColor = DefaultColor;
            }
            else if (input == '5')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write('▌');
                Console.ForegroundColor = DefaultColor;
            }
            else if (input == '6')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write('▐');
                Console.ForegroundColor = DefaultColor;
            }
            else if (input == 'Đ')
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write('#');
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = DefaultColor;
            }
            else if (input == '☻')
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(input);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = DefaultColor;
            }
            else if (input == '0')
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write('☻');
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Write(input);
            }
        }

        /// <summary>
        /// Renders invidiual frame
        /// CZ:
        /// Vykreslí invidiuální snímek
        /// </summary>
        /// <param name="frameIndex">Index of frame</param>
        /// <param name="delay"></param>
        private static void RenderFrame(int frameIndex, int delay = 200)
        {
            if (Variables.OperatingSystem == "win11")
            {
                for (int i = 0; i < Console.BufferHeight / 2 - (Frames()[frameIndex].Length / 2); i++)
                {
                    Console.WriteLine();
                }
            }
            for (int i = 0; i < Frames()[frameIndex].Length; i++) 
            {
                getCursorToCenter(Frames()[frameIndex][0].Length, false);
                for (int j = 0; j < Frames()[frameIndex][i].Length; j++)
                {
                    RenderPixel(Frames()[frameIndex][i][j]);
                }
                Console.WriteLine();
            }
            Thread.Sleep(delay);
        }
        
        /// <summary>
        /// Renders the Intro Text
        /// CZ:
        /// Vypíše Intro text
        /// </summary>
        public static void RenderIntroText() 
        {
            Console.Clear();
            getCursorToCenter(15);
            Thread.Sleep(300);
            Console.WriteLine("HALLS OF HORROR");
            Thread.Sleep(1000);
            Console.Clear();
            getCursorToCenter(15);
            Console.WriteLine("HA LS OF H RROR");
            Thread.Sleep(100);
            Console.Clear();
            getCursorToCenter(15);
            Console.WriteLine("H L S O  H R  R");
            Thread.Sleep(100);
            Console.Clear();
            getCursorToCenter(15);
            Console.WriteLine(" ALL  NO  ERROR");
            Thread.Sleep(100);
            Console.Clear();
            getCursorToCenter(15);
            Console.WriteLine("  L    P     O ");
            Thread.Sleep(100);
            Console.Clear();
            Thread.Sleep(1000);
            Console.Clear();
            getCursorToCenter(10);
            Console.WriteLine("Loading...");
            Thread.Sleep(500);
        }

        public static char[][][] Frames()
        {
            char[][][] output = { 
                Frame1 , Frame2 , Frame3 , Frame4 ,
                Frame5 , Frame6 , Frame7 , Frame8 ,
                Frame9 , Frame10 , Frame11 , Frame12 ,
                Frame13 , Frame14 , Frame15 , Frame16 ,
                Frame17 , Frame18 , Frame19 , Frame20 ,
                Frame21 , Frame22 , Frame23 , Frame24 ,
                Frame25 , Frame26 , Frame27 , Frame28 ,
                Frame29 , Frame30 , Frame31 , Frame32 ,
                Frame33 , Frame34 , Frame35 , Frame36 ,
                Frame37 ,
            };
            return output;
        }

        private static char[][] FrameBase =
        {
            "                                                          ".ToCharArray(),
            "                                                     ▄▄▄▄▄".ToCharArray(),
            "      ▄▄▄                                ▄▄     ▄▄########".ToCharArray(),
            "    ▄█████▄       222                 ▄█████▀  ▐########██".ToCharArray(),
            "   ▐███████▌     999992               ▀█████▄ ▄########███".ToCharArray(),
            "    ▀█████▀     69999995                 ▀  ▀########█████".ToCharArray(),
            "      ▀██         7997                      ########██████".ToCharArray(),
            "       ██          99                      ########███████".ToCharArray(),
            "      ███▄        2999                    ▄███████########".ToCharArray(),
            "  ###############################ĐĐĐĐĐĐ###################".ToCharArray(),
            "###############################Đ##ĐĐĐ##Đ##################".ToCharArray(),
            "################################Đ#########################".ToCharArray(),
            "##########################################################".ToCharArray(),
        };
        private static char[][] Frame1 =
        {
            "                                          ".ToCharArray(),
            "                                          ".ToCharArray(),
            "      ▄▄▄                                ▄".ToCharArray(),
            "    ▄█████▄       222                 ▄███".ToCharArray(),
            "   ▐███████▌     999992               ▀███".ToCharArray(),
            "    ▀█████▀     69999995                 ▀".ToCharArray(),
            "      ▀██         7997                    ".ToCharArray(),
            "       ██          99                     ".ToCharArray(),
            "      ███▄        2999                    ".ToCharArray(),
            "  ###############################ĐĐĐĐĐĐ###".ToCharArray(),
            "###############################Đ##ĐĐĐ##Đ##".ToCharArray(),
        };
        private static char[][] Frame2 =
        {
            "                                          ".ToCharArray(),
            "                                          ".ToCharArray(),
            "      ▄▄▄                                ▄".ToCharArray(),
            "    ▄█████▄       222                 ▄███".ToCharArray(),
            "   ▐███████▌     999992               ▀███".ToCharArray(),
            "    ▀█████▀     69999995                 ▀".ToCharArray(),
            "      ▀██         7997                    ".ToCharArray(),
            "       ██          99                     ".ToCharArray(),
            "      ███▄        2999                    ".ToCharArray(),
            "☺ ###############################ĐĐĐĐĐĐ###".ToCharArray(),
            "###############################Đ##ĐĐĐ##Đ##".ToCharArray(),
        };
        private static char[][] Frame3 =
        {
            "                                          ".ToCharArray(),
            "                                          ".ToCharArray(),
            "      ▄▄▄                                ▄".ToCharArray(),
            "    ▄█████▄       222                 ▄███".ToCharArray(),
            "   ▐███████▌     999992               ▀███".ToCharArray(),
            "    ▀█████▀     69999995                 ▀".ToCharArray(),
            "      ▀██         7997                    ".ToCharArray(),
            "       ██          99                     ".ToCharArray(),
            "      ███▄        2999                    ".ToCharArray(),
            " ☺###############################ĐĐĐĐĐĐ###".ToCharArray(),
            "###############################Đ##ĐĐĐ##Đ##".ToCharArray(),
        };
        private static char[][] Frame4 =
        {
            "                                          ".ToCharArray(),
            "                                          ".ToCharArray(),
            "      ▄▄▄                                ▄".ToCharArray(),
            "    ▄█████▄       222                 ▄███".ToCharArray(),
            "   ▐███████▌     999992               ▀███".ToCharArray(),
            "    ▀█████▀     69999995                 ▀".ToCharArray(),
            "      ▀██         7997                    ".ToCharArray(),
            "       ██          99                     ".ToCharArray(),
            " ☺    ███▄        2999                    ".ToCharArray(),
            "  ###############################ĐĐĐĐĐĐ###".ToCharArray(),
            "###############################Đ##ĐĐĐ##Đ##".ToCharArray(),
        };
        private static char[][] Frame5 =
        {
            "                                          ".ToCharArray(),
            "                                          ".ToCharArray(),
            "     ▄▄▄                                ▄▄".ToCharArray(),
            "   ▄█████▄       222                 ▄████".ToCharArray(),
            "  ▐███████▌     999992               ▀████".ToCharArray(),
            "   ▀█████▀     69999995                 ▀ ".ToCharArray(),
            "     ▀██         7997                     ".ToCharArray(),
            "      ██          99                      ".ToCharArray(),
            " ☺   ███▄        2999                    _".ToCharArray(),
            " ###############################ĐĐĐĐĐĐ####".ToCharArray(),
            "##############################Đ##ĐĐĐ##Đ###".ToCharArray(),
        };
        private static char[][] Frame6 =
        {
            "                                          ".ToCharArray(),
            "                                          ".ToCharArray(),
            "    ▄▄▄                                ▄▄ ".ToCharArray(),
            "  ▄█████▄        222                ▄█████".ToCharArray(),
            " ▐███████▌      999992              ▀█████".ToCharArray(),
            "  ▀█████▀      69999995                ▀  ".ToCharArray(),
            "    ▀██          7997                     ".ToCharArray(),
            "     ██           99                     #".ToCharArray(),
            " ☺  ███▄         2999                   _#".ToCharArray(),
            "###############################ĐĐĐĐĐĐ#####".ToCharArray(),
            "#############################Đ##ĐĐĐ##Đ####".ToCharArray(),
        };
        private static char[][] Frame7 =
        {
            "                                          ".ToCharArray(),
            "    ▄▄▄                                ▄▄ ".ToCharArray(),
            "  ▄█████▄        222                ▄█████".ToCharArray(),
            " ▐███████▌      999992              ▀█████".ToCharArray(),
            "  ▀█████▀      69999995                ▀  ".ToCharArray(),
            "    ▀██          7997                     ".ToCharArray(),
            "     ██           99                     #".ToCharArray(),
            "  ☺ ███▄         2999                   _#".ToCharArray(),
            "###############################ĐĐĐĐĐĐ#####".ToCharArray(),
            "#############################Đ##ĐĐĐ##Đ####".ToCharArray(),
            "################################Đ#########".ToCharArray(),
        };
        private static char[][] Frame8 =
        {
            "                                          ".ToCharArray(),
            "   ▄▄▄                                ▄▄  ".ToCharArray(),
            " ▄█████▄        222                ▄█████▀".ToCharArray(),
            "▐███████▌      999992              ▀█████▄".ToCharArray(),
            " ▀█████▀      69999995                ▀  ▀".ToCharArray(),
            "   ▀██          7997                     #".ToCharArray(),
            "    ██           99                     ##".ToCharArray(),
            "  ☺███▄         2999                   _##".ToCharArray(),
            "##############################ĐĐĐĐĐĐ######".ToCharArray(),
            "############################Đ##ĐĐĐ##Đ#####".ToCharArray(),
            "###############################Đ##########".ToCharArray(),
        };
        private static char[][] Frame9 =
        {
            "                                          ".ToCharArray(),
            "  ▄▄▄                                ▄▄   ".ToCharArray(),
            "▄█████▄         222               ▄█████▀ ".ToCharArray(),
            "███████▌       999992             ▀█████▄ ".ToCharArray(),
            "▀█████▀       69999995               ▀  ▀#".ToCharArray(),
            "  ▀██           7997                    ##".ToCharArray(),
            "   ██            99                    ###".ToCharArray(),
            "  ☻██▄          2999                  _###".ToCharArray(),
            "#############################ĐĐĐĐĐĐ#######".ToCharArray(),
            "###########################Đ##ĐĐĐ##Đ######".ToCharArray(),
            "##############################Đ###########".ToCharArray(),
        };
        private static char[][] Frame10 =
        {
            "                                          ".ToCharArray(),
            " ▄▄▄                                ▄▄    ".ToCharArray(),
            "█████▄         222               ▄█████▀ |".ToCharArray(),
            "██████▌       999992             ▀█████▄ #".ToCharArray(),
            "█████▀       69999995               ▀  ▀##".ToCharArray(),
            " ▀██           7997                    ###".ToCharArray(),
            "  ██            99                    ####".ToCharArray(),
            " █☻█▄          2999                  _####".ToCharArray(),
            "############################ĐĐĐĐĐĐ########".ToCharArray(),
            "##########################Đ##ĐĐĐ##Đ#######".ToCharArray(),
            "#############################Đ############".ToCharArray(),
        };
        private static char[][] Frame11 =
        {
            "                                          ".ToCharArray(),
            "▄▄▄                                ▄▄    _".ToCharArray(),
            "████▄          222              ▄█████▀ |#".ToCharArray(),
            "█████▌        999992            ▀█████▄ ##".ToCharArray(),
            "████▀        69999995              ▀  ▀###".ToCharArray(),
            "▀██            7997                   ####".ToCharArray(),
            " ██             99                   #####".ToCharArray(),
            "██☻▄           2999                 _#####".ToCharArray(),
            "###########################ĐĐĐĐĐĐ#########".ToCharArray(),
            "#########################Đ##ĐĐĐ##Đ########".ToCharArray(),
            "############################Đ#############".ToCharArray(),
        };
        private static char[][] Frame12 =
        {
            "                                          ".ToCharArray(),
            "▄▄                                ▄▄    __".ToCharArray(),
            "███▄          222              ▄█████▀ |##".ToCharArray(),
            "████▌        999992            ▀█████▄ ###".ToCharArray(),
            "███▀        69999995              ▀  ▀####".ToCharArray(),
            "██            7997                   #####".ToCharArray(),
            "██☺            99                   ######".ToCharArray(),
            "██▄           2999                 _######".ToCharArray(),
            "##########################ĐĐĐĐĐĐ##########".ToCharArray(),
            "########################Đ##ĐĐĐ##Đ#########".ToCharArray(),
            "###########################Đ##############".ToCharArray(),
        };
        private static char[][] Frame13 =
        {
            "                                          ".ToCharArray(),
            "▄                                ▄▄    __#".ToCharArray(),
            "██▄          222              ▄█████▀ |###".ToCharArray(),
            "███▌        999992            ▀█████▄ ####".ToCharArray(),
            "██▀        69999995              ▀  ▀#####".ToCharArray(),
            "█            7997                   ######".ToCharArray(),
            "█ ☺           99                   #######".ToCharArray(),
            "█▄           2999                 _#######".ToCharArray(),
            "#########################ĐĐĐĐĐĐ###########".ToCharArray(),
            "#######################Đ##ĐĐĐ##Đ##########".ToCharArray(),
            "##########################Đ###############".ToCharArray(),
        };
        private static char[][] Frame14 =
        {
            "                                          ".ToCharArray(),
            "                                ▄▄    __##".ToCharArray(),
            "█▄          222              ▄█████▀ |####".ToCharArray(),
            "██▌        999992            ▀█████▄ #####".ToCharArray(),
            "█▀        69999995              ▀  ▀######".ToCharArray(),
            "            7997                   #######".ToCharArray(),
            "             99                   ########".ToCharArray(),
            "▄ ☺         2999                 _########".ToCharArray(),
            "########################ĐĐĐĐĐĐ############".ToCharArray(),
            "######################Đ##ĐĐĐ##Đ###########".ToCharArray(),
            "#########################Đ################".ToCharArray(),
        };
        private static char[][] Frame15 =
        {
            "                                          ".ToCharArray(),
            "                               ▄▄    __###".ToCharArray(),
            "▄          222              ▄█████▀ |#####".ToCharArray(),
            "█▌        999992            ▀█████▄ ######".ToCharArray(),
            "▀        69999995              ▀  ▀#######".ToCharArray(),
            "           7997                   ########".ToCharArray(),
            "            99                   #########".ToCharArray(),
            "  ☺        2999                 _#########".ToCharArray(),
            "#######################ĐĐĐĐĐĐ#############".ToCharArray(),
            "#####################Đ##ĐĐĐ##Đ############".ToCharArray(),
            "########################Đ#################".ToCharArray(),
        };
        private static char[][] Frame16 =
        {
            "                                         _".ToCharArray(),
            "                              ▄▄    __####".ToCharArray(),
            "          222              ▄█████▀ |######".ToCharArray(),
            "▌        999992            ▀█████▄ #######".ToCharArray(),
            "        69999995              ▀  ▀########".ToCharArray(),
            "          7997                   #########".ToCharArray(),
            "           99                   ##########".ToCharArray(),
            "  ☺       2999                 _##########".ToCharArray(),
            "######################ĐĐĐĐĐĐ##############".ToCharArray(),
            "####################Đ##ĐĐĐ##Đ#############".ToCharArray(),
            "#######################Đ##################".ToCharArray(),
        };
        private static char[][] Frame17 =
        {
            "                                        __".ToCharArray(),
            "                             ▄▄    __#####".ToCharArray(),
            "         222              ▄█████▀ |#######".ToCharArray(),
            "        999992            ▀█████▄ ########".ToCharArray(),
            "       69999995              ▀  ▀#########".ToCharArray(),
            "         7997                   ##########".ToCharArray(),
            "          99                   ###########".ToCharArray(),
            "  ☺      2999                 _###########".ToCharArray(),
            "#####################ĐĐĐĐĐĐ###############".ToCharArray(),
            "###################Đ##ĐĐĐ##Đ##############".ToCharArray(),
            "######################Đ###################".ToCharArray(),
        };
        private static char[][] Frame18 =
        {
            "                                       ___".ToCharArray(),
            "                            ▄▄    __######".ToCharArray(),
            "        222              ▄█████▀ |########".ToCharArray(),
            "       999992            ▀█████▄ #########".ToCharArray(),
            "      69999995              ▀  ▀##########".ToCharArray(),
            "        7997                   ###########".ToCharArray(),
            "  ☺      99                   ############".ToCharArray(),
            "        2999                 _############".ToCharArray(),
            "####################ĐĐĐĐĐĐ################".ToCharArray(),
            "##################Đ##ĐĐĐ##Đ###############".ToCharArray(),
            "#####################Đ####################".ToCharArray(),
        };
        private static char[][] Frame19 =
        {
            "                                      ____".ToCharArray(),
            "                           ▄▄    __#######".ToCharArray(),
            "       222              ▄█████▀ |#########".ToCharArray(),
            "      999992            ▀█████▄ ##########".ToCharArray(),
            "     69999995              ▀  ▀###########".ToCharArray(),
            "       7997                   ############".ToCharArray(),
            "  ☺     99                   #############".ToCharArray(),
            "       2999                 _#############".ToCharArray(),
            "###################ĐĐĐĐĐĐ#################".ToCharArray(),
            "#################Đ##ĐĐĐ##Đ################".ToCharArray(),
            "####################Đ#####################".ToCharArray(),
        };
        private static char[][] Frame20 =
        {
            "                                     _____".ToCharArray(),
            "                          ▄▄    __########".ToCharArray(),
            "      222              ▄█████▀ |##########".ToCharArray(),
            "     999992            ▀█████▄ ###########".ToCharArray(),
            "    69999995              ▀  ▀############".ToCharArray(),
            "      7997                   #############".ToCharArray(),
            "       99                   ##############".ToCharArray(),
            "  ☺   2999                 _##############".ToCharArray(),
            "##################ĐĐĐĐĐĐ##################".ToCharArray(),
            "################Đ##ĐĐĐ##Đ#################".ToCharArray(),
            "###################Đ######################".ToCharArray(),
        };
        private static char[][] Frame21 =
        {
            "                                     _____".ToCharArray(),
            "                          ▄▄    __########".ToCharArray(),
            "      222              ▄█████▀ |##########".ToCharArray(),
            "     999992            ▀█████▄ ###########".ToCharArray(),
            "    69999995              ▀  ▀############".ToCharArray(),
            "      7997                   #############".ToCharArray(),
            "       99                   ##############".ToCharArray(),
            "   ☺  2999                 _##############".ToCharArray(),
            "##################ĐĐĐĐĐĐ##################".ToCharArray(),
            "################Đ##ĐĐĐ##Đ#################".ToCharArray(),
            "###################Đ######################".ToCharArray(),
        };
        private static char[][] Frame22 =
        {
            "                                     _____".ToCharArray(),
            "                          ▄▄    __########".ToCharArray(),
            "      222              ▄█████▀ |##########".ToCharArray(),
            "     999992            ▀█████▄ ###########".ToCharArray(),
            "    69999995              ▀  ▀############".ToCharArray(),
            "      7997                   #############".ToCharArray(),
            "       99                   ##############".ToCharArray(),
            "    ☺ 2999                 _##############".ToCharArray(),
            "##################ĐĐĐĐĐĐ##################".ToCharArray(),
            "################Đ##ĐĐĐ##Đ#################".ToCharArray(),
            "###################Đ######################".ToCharArray(),
        };
        private static char[][] Frame23 =
        {
            "                                     _____".ToCharArray(),
            "                          ▄▄    __########".ToCharArray(),
            "      222              ▄█████▀ |##########".ToCharArray(),
            "     999992            ▀█████▄ ###########".ToCharArray(),
            "    69999995              ▀  ▀############".ToCharArray(),
            "      7997                   #############".ToCharArray(),
            "       99                   ##############".ToCharArray(),
            "     ☺2999                 _##############".ToCharArray(),
            "##################ĐĐĐĐĐĐ##################".ToCharArray(),
            "################Đ##ĐĐĐ##Đ#################".ToCharArray(),
            "###################Đ######################".ToCharArray(),
        };
        private static char[][] Frame24 =
        {
            "                                     _____".ToCharArray(),
            "                          ▄▄    __########".ToCharArray(),
            "      222              ▄█████▀ |##########".ToCharArray(),
            "     999992            ▀█████▄ ###########".ToCharArray(),
            "    69999995              ▀  ▀############".ToCharArray(),
            "      7997                   #############".ToCharArray(),
            "      ☺99                   ##############".ToCharArray(),
            "      2999                 _##############".ToCharArray(),
            "##################ĐĐĐĐĐĐ##################".ToCharArray(),
            "################Đ##ĐĐĐ##Đ#################".ToCharArray(),
            "###################Đ######################".ToCharArray(),
        };
        private static char[][] Frame25 =
        {
            "                                      ____".ToCharArray(),
            "                           ▄▄    __#######".ToCharArray(),
            "       222              ▄█████▀ |#########".ToCharArray(),
            "      999992            ▀█████▄ ##########".ToCharArray(),
            "     69999995              ▀  ▀###########".ToCharArray(),
            "       7997                   ############".ToCharArray(),
            "        09                   #############".ToCharArray(),
            "       2999                 _#############".ToCharArray(),
            "###################ĐĐĐĐĐĐ#################".ToCharArray(),
            "#################Đ##ĐĐĐ##Đ################".ToCharArray(),
            "####################Đ#####################".ToCharArray(),
        };   
        private static char[][] Frame26 =
        {
            "                                      ____".ToCharArray(),
            "                           ▄▄    __#######".ToCharArray(),
            "       222              ▄█████▀ |#########".ToCharArray(),
            "      999992            ▀█████▄ ##########".ToCharArray(),
            "     69999995              ▀  ▀###########".ToCharArray(),
            "       7997                   ############".ToCharArray(),
            "        90                   #############".ToCharArray(),
            "       2999                 _#############".ToCharArray(),
            "###################ĐĐĐĐĐĐ#################".ToCharArray(),
            "#################Đ##ĐĐĐ##Đ################".ToCharArray(),
            "####################Đ#####################".ToCharArray(),
        };
        private static char[][] Frame27 =
        {
            "                                      ____".ToCharArray(),
            "                           ▄▄    __#######".ToCharArray(),
            "       222              ▄█████▀ |#########".ToCharArray(),
            "      999992            ▀█████▄ ##########".ToCharArray(),
            "     69999995              ▀  ▀###########".ToCharArray(),
            "       7997                   ############".ToCharArray(),
            "        90                   #############".ToCharArray(),
            "       2990                 _#############".ToCharArray(),
            "###################ĐĐĐĐĐĐ#################".ToCharArray(),
            "#################Đ##ĐĐĐ##Đ################".ToCharArray(),
            "####################Đ#####################".ToCharArray(),
        };
        private static char[][] Frame28 =
        {
            "                                      ____".ToCharArray(),
            "                             ▄▄    __#####".ToCharArray(),
            "         222              ▄█████▀ |#######".ToCharArray(),
            "        999992            ▀█████▄ ########".ToCharArray(),
            "       69999995              ▀  ▀#########".ToCharArray(),
            "         7997                   ##########".ToCharArray(),
            "          99                   ###########".ToCharArray(),
            "         2999☺                _###########".ToCharArray(),
            "#####################ĐĐĐĐĐĐ###############".ToCharArray(),
            "###################Đ##ĐĐĐ##Đ##############".ToCharArray(),
            "######################Đ###################".ToCharArray(),
        };
        private static char[][] Frame29 =
        {
            "                           ▄▄     ___#####".ToCharArray(),
            "       2222              ▄████▄  |########".ToCharArray(),
            "      9999992            ██████  #########".ToCharArray(),
            "     699999995           ▀██████ #########".ToCharArray(),
            "     699999995                 ▀##########".ToCharArray(),
            "       79997                   ###########".ToCharArray(),
            "        999                   ############".ToCharArray(),
            "       99999 ☺               _############".ToCharArray(),
            "####################ĐĐĐĐĐĐ################".ToCharArray(),
            "##################Đ##ĐĐĐ##Đ###############".ToCharArray(),
            "#####################Đ####################".ToCharArray(),
        };
        private static char[][] Frame30 =
        {
            "        999992             ██████  #######".ToCharArray(),
            "       69999995            ▀██████ #######".ToCharArray(),
            "       69999995                  █########".ToCharArray(),
            "         7997                    #########".ToCharArray(),
            "          99                    ##########".ToCharArray(),
            "          992                   ##########".ToCharArray(),
            "         9999  ☺               ###########".ToCharArray(),
            "####################ĐĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "#####################ĐĐĐĐĐĐĐ##############".ToCharArray(),
            "###################Đ##ĐĐĐĐ##Đ#############".ToCharArray(),
            "######################ĐĐ##################".ToCharArray(),
        };
        private static char[][] Frame31 =
        {
            "       69999995                 █#########".ToCharArray(),
            "        799997                  ##########".ToCharArray(),
            "          99                   ###########".ToCharArray(),
            "          99                   ###########".ToCharArray(),
            "          999                  ###########".ToCharArray(),
            "         9999   ☺             ############".ToCharArray(),
            "####################ĐĐĐĐĐĐĐĐ##############".ToCharArray(),
            "#####################ĐĐĐĐĐĐĐ##############".ToCharArray(),
            "#####################ĐĐĐĐĐĐ###############".ToCharArray(),
            "###################Đ##ĐĐĐ##Đ##############".ToCharArray(),
            "##########################################".ToCharArray(),
        };
        private static char[][] Frame32 =
        {
            "       69999995                  █########".ToCharArray(),
            "        799997                   #########".ToCharArray(),
            "          99                    ##########".ToCharArray(),
            "          99                    ##########".ToCharArray(),
            "          999                   ##########".ToCharArray(),
            "         9999    ☺             ###########".ToCharArray(),
            "####################ĐĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "#####################ĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "#####################ĐĐĐĐĐĐĐ##############".ToCharArray(),
            "###################Đ###ĐĐĐ##Đ#############".ToCharArray(),
            "##########################################".ToCharArray(),
        };
        private static char[][] Frame33 =
        {
            "       69999995                  █########".ToCharArray(),
            "        799997                   #########".ToCharArray(),
            "          99                    ##########".ToCharArray(),
            "          99                    ##########".ToCharArray(),
            "          999                   ##########".ToCharArray(),
            "         9999     ☺            ###########".ToCharArray(),
            "####################ĐĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "#####################ĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "#####################ĐĐĐĐĐĐĐ##############".ToCharArray(),
            "###################Đ###ĐĐĐ##Đ#############".ToCharArray(),
            "##########################################".ToCharArray(),
        };
        private static char[][] Frame34 =
        {
            "       69999995                  █########".ToCharArray(),
            "        799997                   #########".ToCharArray(),
            "          99                    ##########".ToCharArray(),
            "          99                    ##########".ToCharArray(),
            "          999      ☺            ##########".ToCharArray(),
            "         9999                  ###########".ToCharArray(),
            "####################ĐĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "#####################ĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "#####################ĐĐĐĐĐĐĐ##############".ToCharArray(),
            "###################Đ###ĐĐĐ##Đ#############".ToCharArray(),
            "##########################################".ToCharArray(),
        };
        private static char[][] Frame35 =
        {
            "     99999                       #########".ToCharArray(),
            "      999                      ###########".ToCharArray(),
            "      999                      ###########".ToCharArray(),
            "      9992                     ###########".ToCharArray(),
            "     29999         ☺           ###########".ToCharArray(),
            "    2999992                  #############".ToCharArray(),
            "###################ĐĐĐĐĐĐĐĐ###############".ToCharArray(),
            "####################ĐĐĐĐĐĐĐ###############".ToCharArray(),
            "####################ĐĐĐĐĐĐ################".ToCharArray(),
            "##################Đ##ĐĐĐ##Đ###############".ToCharArray(),
            "##########################################".ToCharArray(),
        };
        private static char[][] Frame36 =
        {
            "      999999                    ##########".ToCharArray(),
            "       9999                     ##########".ToCharArray(),
            "       9999                     ##########".ToCharArray(),
            "       99992                    ##########".ToCharArray(),
            "      299999                    ##########".ToCharArray(),
            "     29999992        ☺         ###########".ToCharArray(),
            "##################ĐĐĐĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "###################ĐĐĐĐĐĐĐĐĐĐ#############".ToCharArray(),
            "####################ĐĐĐĐĐĐĐĐ##############".ToCharArray(),
            "###################Đ##ĐĐĐĐ##Đ#############".ToCharArray(),
            "##########################################".ToCharArray(),
        };
        private static char[][] Frame37 =
        {
            " ".ToCharArray(),
            " ".ToCharArray(),
            " ".ToCharArray(),
        };
    }
}
