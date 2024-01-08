using Komprese.src.CompressHandling;
using Komprese.src.FileHandling;
using Komprese.src.LogHandling;
using Komprese.src.UI;

namespace Komprese.src
{
    /// <summary>
    /// Třída Program slouží jako vstupní bod aplikace pro generátor rozvrhů.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Oddělovač pro vizuální oddělení částí programu.
        /// </summary>
        public static string Oddelovac = "\n----------------------------------------------------------------------------------\n";
        /// <summary>
        /// Instance pro zpracování a zápis logů.
        /// </summary>
        public static LogHandler Log = new LogHandler();

        /// <summary>
        /// Instance pro načítání konfiguračního souboru.
        /// </summary>
        public static ConfigurationLoader Config =  new ConfigurationLoader();

        // <summary>
        /// Vstupní bod programu.
        /// </summary>
        /// <param name="args">Argumenty předané při spuštění programu.</param>
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
        }
    }
}