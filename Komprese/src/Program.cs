using Komprese.src.CompressHandling;
using Komprese.src.FileHandling;
using Komprese.src.LogHandling;
using Komprese.src.UI;

namespace Komprese.src
{
    public class Program
    {
        public static string Oddelovac = "\n----------------------------------------------------------------------------------\n";
        public static LogHandler Log = new LogHandler();
        public static ConfigurationLoader Config;

        static void Main(string[] args)
        {
            bool run = true;

            Console.WriteLine(Oddelovac);
            Console.WriteLine("Vítejte v programu Alpha! (Generátor rozvrhů)");
            Console.WriteLine("Autor: Miloš Tesař C4b");
            Console.WriteLine(Oddelovac);

            #region Load Configuration
            try
            {
                Config = new ConfigurationLoader("config\\config.xml");
                Config.LoadConfiguration();
                Log = new LogHandler(Config.LogFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při načítání: {ex.Message}");
                Log.Write($"Chyba: {ex.Message}");
                run = false;
            }
            #endregion

            MainMenuUI.Start(run);

            Console.WriteLine("\nKonec programu");
            Console.WriteLine(Oddelovac);

            /*  
             * Empty Dict:
             * 
             * Dictionary<string, string> dict = new Dictionary<string, string>();
             * fileHandler.WriteDictToXml(dict, config.DictionaryFilePath);
             * 
             */


            /*
            Console.WriteLine($"InputFilePath: {config.InputFilePath}");
            Console.WriteLine($"OutputFilePath: {config.OutputFilePath}");
            Console.WriteLine($"LogFilePath: {config.LogFilePath}");
            Console.WriteLine($"Dictionary: {config.DictionaryFilePath}");
            */
        }
    }
}