using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Komprese.src.UI
{
    /// <summary>
    /// Třída ConfigurationLoader slouží k načítání konfigurace ze souboru XML.
    /// Obsahuje vlastnosti pro cesty k vstupnímu, výstupnímu, logovacímu a slovníkovému souboru.
    /// </summary>
    public class ConfigurationLoader
    {
        private readonly string filePath = "config\\config.xml";

        public string InputFilePath { get; private set; }
        public string OutputFilePath { get; private set; }
        public string LogFilePath { get; private set; }
        public string DictionaryFilePath { get; private set; }

        /// <summary>
        /// Konstruktor třídy ConfigurationLoader.
        /// </summary>
        public ConfigurationLoader(){ }

        /// <summary>
        /// Načte konfiguraci ze souboru XML a nastaví odpovídající vlastnosti.
        /// </summary>
        public void LoadConfiguration()
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);
                InputFilePath = GetValueFromXml(doc, "InputFilePath");
                OutputFilePath = GetValueFromXml(doc, "OutputFilePath");
                LogFilePath = GetValueFromXml(doc, "LogFilePath");
                DictionaryFilePath = GetValueFromXml(doc, "DictionaryFilePath");
                //Console.WriteLine("Konfigurace načtena úspěšně.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při načítání konfigurace: {ex.Message}");
            }
        }

        /// <summary>
        /// Získá hodnotu z XML elementu podle zadaného názvu.
        /// </summary>
        /// <param name="doc">XDocument reprezentující načtený XML soubor.</param>
        /// <param name="elementName">Název hledaného XML elementu.</param>
        /// <returns>Hodnota XML elementu nebo prázdný řetězec, pokud není nalezen.</returns>
        private string GetValueFromXml(XDocument doc, string elementName)
        {
            XElement element = doc.Element("Configuration")?.Element(elementName);
            return element?.Value ?? string.Empty;
        }
    }
}
