using Komprese.src.CompressHandling;
using Komprese.src.FileHandling;
using Komprese.src.LogHandling;
using Komprese.src.UI;

namespace Komprese.src
{
    public class Program
    {
        public static ConfigurationLoader Config;
        public static LogHandler Log = new LogHandler("log\\EmergencyLog.txt");
        public static FileHandler FileHandler = new FileHandler();
        public static Dictionary<string, string> CompressDict = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            bool run = true;
            string text = string.Empty;

            Console.WriteLine(MainMenuUI.Oddelovac);
            Console.WriteLine("Vítejte v programu Alpha! (Generátor rozvrhů)");
            Console.WriteLine("Autor: Miloš Tesař C4b");
            Console.WriteLine(MainMenuUI.Oddelovac);

            #region Load Configuration
            try
            {
                Config = new ConfigurationLoader();
                Config.LoadConfiguration();
                Log = new LogHandler(Config.LogFilePath);
                CompressDict = FileHandler.ReadDictFromXml(Config.DictionaryFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba: {ex.Message}");
                Log.Write($"Chyba: {ex.Message}");
                run = false;
            }
            #endregion

            MainMenuUI.Start(run);
            /*  
             * Empty Dict:
             * 
             * Dictionary<string, string> dict = new Dictionary<string, string>();
             * fileHandler.WriteDictToXml(dict, config.DictionaryFilePath);
             * 
             */
            
            Compression test = new Compression(text);
            //Console.WriteLine(test.CompressText);
            if (!string.IsNullOrEmpty(test.CompressText))
            {
                Log.Write("Text byl úspěšně zkomprimován.");
                try
                {
                    FileHandler.WriteToFile(Config.OutputFilePath, test.CompressText);
                    Log.Write("Zkomprimovaný text byl úspěšně uložen.");
                }
                catch 
                {
                    Log.Write("Nastaly potíže při ukládání zkomprimovaného textu.");
                }
            }
            else
            {
                Log.Write("Text se nepodařilo zkomprimovat.");
            }

            /*
            foreach (var kvp in compressDict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }*/
            FileHandler.WriteDictToXml(CompressDict, Config.DictionaryFilePath);
            
            /*
            Console.WriteLine($"InputFilePath: {config.InputFilePath}");
            Console.WriteLine($"OutputFilePath: {config.OutputFilePath}");
            Console.WriteLine($"LogFilePath: {config.LogFilePath}");
            Console.WriteLine($"Dictionary: {config.DictionaryFilePath}");
            */
        }
    }
}