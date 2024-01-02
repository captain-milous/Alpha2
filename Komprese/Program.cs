using Komprese.config;

namespace Komprese
{
    public class Program
    {
        static void Main(string[] args)
        {
            string xmlFilePath = "config.xml";

            ConfigurationLoader configLoader = new ConfigurationLoader(xmlFilePath);
            configLoader.LoadConfiguration();

            // Použití načtených hodnot
            Console.WriteLine($"InputFilePath: {configLoader.InputFilePath}");
            Console.WriteLine($"OutputFilePath: {configLoader.OutputFilePath}");
            Console.WriteLine($"LogFilePath: {configLoader.LogFilePath}");
        }
    }
}