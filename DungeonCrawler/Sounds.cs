using System.Text.Json;
using static DungeonCrawler.Program;

namespace DungeonCrawler
{
    internal class Sounds
    {
        public static void LoadMusic()
        {
            if (Get_ffPlay_location() == "") // MUSIC
            {
                changeFFplay();
            }
            PlaySound("loaded.wav",30);
        }
        public static void changeFFplay()
        {
            Console.SetWindowSize(100, 20);
            Console.WriteLine("If you want to have music, then install ffmpeg\n and write here the path to the /bin folder\n example: C:\\Program Files (x86)\\ffmpeg\\bin");
            File.WriteAllText("music.yaml", (Console.ReadLine()));
            Console.Clear();
        }

        public static string Get_ffPlay_location()
        {
            try
            {
                string filename = "music.yaml";
                if (File.Exists(filename))
                {
                    return File.ReadAllText(filename);
                }
                else
                {
                    File.WriteAllText(filename, (""));
                    return File.ReadAllText(filename);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured when opening config");
                throw;
            }
        }
        public static void PlaySound(string name = "Error.mp3", int volume = 100)
        {
            string fileLocalition = $"{ AppDomain.CurrentDomain.BaseDirectory }{name}";
            try
            {
                System.Diagnostics.Process.Start('"'+Get_ffPlay_location() + "\\ffplay"+'"', $"{fileLocalition} -volume {volume} -loglevel quiet -nodisp");
            }
            catch (Exception e)
            {
                
                // LOL how to make no crashes
            }
        }
        
    }
}
