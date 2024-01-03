using Komprese.src.FileHandling;
using Komprese.src.LogHandling;
using Komprese.src.UI;

namespace Komprese.src
{
    public class Program
    {
        public static ConfigurationLoader config;
        public static LogHandler log;
        public static FileHandler fileHandler = new FileHandler();
        public static Dictionary<string, string> compressDict = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            bool run = true;
            #region Load Configuration
            try
            {
                config = new ConfigurationLoader();
                config.LoadConfiguration();
                log = new LogHandler(config.LogFilePath);
                compressDict = fileHandler.ReadDictFromXml(config.DictionaryFilePath);
                foreach (var kvp in compressDict)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba: {ex.Message}");
                run = false;
            }
            #endregion



            /*
            Console.WriteLine($"InputFilePath: {config.InputFilePath}");
            Console.WriteLine($"OutputFilePath: {config.OutputFilePath}");
            Console.WriteLine($"LogFilePath: {config.LogFilePath}");
            Console.WriteLine($"Dictionary: {config.DictionaryFilePath}");
            */
            /*
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Key1", "Value1");
            dict.Add("Key2", "Value2");
            dict.Add("Key3", "Value3");

            fileHandler.WriteDictToXml(dict, config.DictionaryFilePath);*/
        }
    }
}