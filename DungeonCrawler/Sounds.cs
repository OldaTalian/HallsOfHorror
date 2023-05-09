using System.Diagnostics;
using static DungeonCrawler.Variables;

namespace DungeonCrawler
{
    internal class Sounds
    {
        /// <summary>
        /// Loads ffmpeg so the music works
        /// CZ:
        /// Načte ffmpeg pro hudbu
        /// </summary>
        public static void LoadMusic()
        {
            if (Get_ffPlay_location() == "") // MUSIC
            {
                changeFFplay();
            }
            PlaySound("assets/loaded.wav");
        }

        /// <summary>
        /// Changes the ffmpeg location
        /// CZ:
        /// Mění cestu k ffmpeg
        /// </summary>
        public static void changeFFplay()
        {
            getCursorToCenter(35, false);
            Console.WriteLine("If you want to have music, then install ffmpeg");
            getCursorToCenter(35, false);
            Console.WriteLine("and write here the path to the /bin folder");
            getCursorToCenter(35, false);
            Console.WriteLine("example: C:\\Program Files (x86)\\ffmpeg\\bin");
            getCursorToCenter(35, false);
            File.WriteAllText("music.yaml", (Console.ReadLine()));
            Console.Clear();
        }

        /// <summary>
        /// Gets the location (path) of ffmpeg
        /// CZ:
        /// Najde cestu k ffmpeg
        /// </summary>
        /// <returns></returns>
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
                Console.WriteLine("Error occured when opening music.yaml");
                throw;
            }
        }
        private static Process musicProcess;


        /// <summary>
        /// Plays a music file using ffmpeg
        /// CZ:
        /// Přehrává hudbu ze souboru
        /// </summary>
        /// <param name="name">Name of the file</param>
        /// <param name="volume"></param>
        public static void PlayMusic(string name = "assets/Error.mp3")
        {
            if (MusicEnabled)
            {
                string fileLocation = $"{AppDomain.CurrentDomain.BaseDirectory}{name}";
                try
                {
                    musicProcess = new Process();
                    musicProcess.StartInfo.FileName = Get_ffPlay_location() + "\\ffplay"; // Set the ffplay executable file path
                    musicProcess.StartInfo.Arguments = $" {fileLocation} -volume {MusicVolume} -loop 9999 -loglevel quiet -nodisp";
                    musicProcess.Start();
                }
                catch (Exception e)
                {
                    e.ToString();
                    // Handle exception e
                }
            }
        }

        /// <summary>
        /// Plays sound effect from file
        /// CZ:
        /// Přehraje sound effekt ze souboru
        /// </summary>
        /// <param name="name"></param>
        /// <param name="volume"></param>
        public static void PlaySound(string name = "Error.mp3", double pitchVariation = 0)
        {
            if (SoundsEnabled)
            {
                string fileLocation = $"{AppDomain.CurrentDomain.BaseDirectory}{name}";
                try
                {
                    double pitch = Math.Pow(2, pitchVariation / 12.0);
                    string ffplayArgs = $"{fileLocation} -volume {SoundVolume} -loglevel quiet -nodisp -af \"atempo={pitch}\"";
                    System.Diagnostics.Process.Start('"' + Get_ffPlay_location() + "\\ffplay" + '"', ffplayArgs);
                }
                catch (Exception e)
                {
                    e.ToString();
                    // LOL it makes no crashes
                }
            }
        }

        /// <summary>
        /// Stops every music that is playing
        /// CZ:
        /// Zastaví všechnu hudbu
        /// </summary>
        public static void StopMusic()
        {
            try
            {
                if (musicProcess != null && !musicProcess.HasExited)
                {
                     musicProcess.Kill(); // Kill the ffplay process if it's running
                }
            }
            catch (Exception e)
            {
                e.ToString(); // Handle exception e
            }
        }


    }
}
