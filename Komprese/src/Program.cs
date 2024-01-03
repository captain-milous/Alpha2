using Komprese.src.CompressHandling;
using Komprese.src.FileHandling;
using Komprese.src.LogHandling;
using Komprese.src.UI;

namespace Komprese.src
{
    public class Program
    {
        static ConfigurationLoader config;
        static LogHandler log;
        static FileHandler fileHandler = new FileHandler();
        static Dictionary<string, string> compressDict = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            bool run = true;
            string text = string.Empty;

            #region Load Configuration
            try
            {
                config = new ConfigurationLoader();
                config.LoadConfiguration();
                log = new LogHandler(config.LogFilePath);
                compressDict = fileHandler.ReadDictFromXml(config.DictionaryFilePath);
                text = fileHandler.ReadFromFile(config.InputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba: {ex.Message}");
                run = false;
            }
            #endregion
            /*  
             * Empty Dict:
             * 
             * Dictionary<string, string> dict = new Dictionary<string, string>();
             * fileHandler.WriteDictToXml(dict, config.DictionaryFilePath);
             * 
             */
            
            Compression test = new Compression(text, compressDict);
            Console.WriteLine(test.CompressText);
            if (string.IsNullOrEmpty(test.CompressText))
            {
                log.Write("Text byl úspěšně zkomprimován.");
                try
                {
                    fileHandler.WriteToFile(config.OutputFilePath, test.CompressText);
                    log.Write("Zkomprimovaný text byl úspěšně uložen.");
                }
                catch 
                {
                    log.Write("Nastaly potíže při ukládání zkomprimovaného textu.");
                }
            }
            else
            {
                log.Write("Text se nepodařilo zkomprimovat.");
            }

            compressDict = test.CompressDict;
            /*
            foreach (var kvp in compressDict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }*/
            fileHandler.WriteDictToXml(compressDict, config.DictionaryFilePath);
            
            /*
            Console.WriteLine($"InputFilePath: {config.InputFilePath}");
            Console.WriteLine($"OutputFilePath: {config.OutputFilePath}");
            Console.WriteLine($"LogFilePath: {config.LogFilePath}");
            Console.WriteLine($"Dictionary: {config.DictionaryFilePath}");
            */
        }
    }
}