using Komprese.config;
using Komprese.data;

namespace Komprese
{
    public class Program
    {
        public static ConfigurationLoader config;
        public static FileHandler fileHandler = new FileHandler();
        public static Dictionary<string, string> compressionDict = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            try
            {
                config = new ConfigurationLoader("config.xml");
                config.LoadConfiguration();
                /*
                Console.WriteLine($"InputFilePath: {config.InputFilePath}");
                Console.WriteLine($"OutputFilePath: {config.OutputFilePath}");
                Console.WriteLine($"LogFilePath: {config.LogFilePath}");
                */
                Console.WriteLine($"Dictionary: {config.DictionaryFilePath}");
                compressionDict = fileHandler.ReadDictFromXml(config.DictionaryFilePath);
                foreach (var kvp in compressionDict)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Chyba: {ex.Message}");
            }
            /*
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Key1", "Value1");
            dict.Add("Key2", "Value2");
            dict.Add("Key3", "Value3");

            fileHandler.WriteDictToXml(dict, config.DictionaryFilePath);*/
        }
    }
}