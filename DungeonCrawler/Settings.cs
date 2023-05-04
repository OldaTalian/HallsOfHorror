using static DungeonCrawler.Variables;
using System.Configuration;
using System.Web;
using Microsoft.Win32.SafeHandles;

namespace DungeonCrawler
{

    internal class Settings
    {
        public static byte OPTION = 1;
        public static void SettingsMenu()
        {
            bool escape = true;
            while (escape)
            {
                Console.Clear();
                getCursorToCenter(50, false);
                if (OPTION == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.Write("< ESCAPE");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("            SETTINGS                      ");
                getCursorToCenter(50, false);
                Console.WriteLine("==================================================");
                OptionMenu(OPTION);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                getCursorToCenter(30, false);
                Console.WriteLine("For changes to take effect,");
                getCursorToCenter(50, false);
                Console.WriteLine("it may be necessary to restart the application.");
                Console.ForegroundColor = ConsoleColor.White;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        if (OPTION != ListConfigValues().Length)
                        {
                            OPTION++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (OPTION > 0)
                        {
                            OPTION--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (OPTION == 0)
                        {
                            escape = false;
                            return;
                        }
                        changeConfig();
                        break;
                    case ConsoleKey.Escape:
                        escape = false; 
                        break;
                }
            }
        }

        private static void changeConfig()
        {
            getCursorToCenter(20, false);
            var key = ListConfigValues()[OPTION - 1];
            if (key.Split(" = ")[1].ToLower() == "true" || key.Split(" = ")[1].ToLower() == "false")
            {
                Console.WriteLine("Select one:");
                Console.WriteLine();
                int boolOption = 1;
                string[] options =
                {
                    "True",
                    "False",
                };
                bool hasTyped = false;
                while (!hasTyped)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    getCursorToCenter(20, false);
                    if (boolOption == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(options[0]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("  " + options[1]);
                    }
                    else
                    {
                        Console.Write(options[0] + "  ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine(options[1]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            boolOption = 1;
                            break;
                        case ConsoleKey.Escape:
                            hasTyped = true;
                            break;
                        case ConsoleKey.RightArrow:
                            boolOption = 2;
                            break;
                        case ConsoleKey.Enter:
                            SetConfigValue(key.Split(" = ")[0], options[boolOption - 1]);
                            hasTyped = true;
                            break;
                    }
                }
            }
            else if (int.TryParse(key.Split('=')[1].Trim(), out int value))
            {
                Console.WriteLine("Write <INT(number)>");
                int newValueInt;
                while (!int.TryParse(Console.ReadLine(), out newValueInt))
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
                SetConfigValue(key.Split(" = ")[0], newValueInt.ToString());
            }
            else
            {
                Console.WriteLine("Write [STRING(text)]");
                SetConfigValue(key.Split(" = ")[0], Console.ReadLine());
            }
        }

        private static void OptionMenu(byte option)
        {
            for (int i = 0; i < ListConfigValues().Length; i++)
            {
                Console.WriteLine();
                if (i + 1 == option)
                {
                    getCursorToCenter(50, false);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("> " + ListConfigValues()[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    getCursorToCenter(50, false);
                    Console.WriteLine("  " + ListConfigValues()[i]);
                }
            }
        }


        public static string GetConfigValue(string settingName)
        {
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings[settingName] ?? string.Empty;
        }
        public static void SetConfigValue(string settingName, string settingValue)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var appSettings = configFile.AppSettings.Settings;

            if (appSettings[settingName] == null)
            {
                appSettings.Add(settingName, settingValue);
            }
            else
            {
                appSettings[settingName].Value = settingValue;
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }


        public static string[] ListConfigValues()
        {
            var settings = ConfigurationManager.AppSettings;
            var roomConfigs = new List<string>();

            foreach (string key in settings.Keys)
            {
                if (!key.Contains("Room"))
                {
                    var value = settings[key];
                    roomConfigs.Add($"{key} = {value}");
                }
            }

            return roomConfigs.ToArray();
        }

    }
}
