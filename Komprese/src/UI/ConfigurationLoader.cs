using Komprese.src.LogHandling;
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

        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public string LogFilePath { get; private set; }
        public string DictionaryFilePath { get; private set; }

        /// <summary>
        /// Defaultní konstruktor třídy ConfigurationLoader.
        /// </summary>
        public ConfigurationLoader()
        {
            filePath = filePath;
        }

        /// <summary>
        /// Konstruktor třídy ConfigurationLoader.
        /// </summary>
        public ConfigurationLoader(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// Načte konfiguraci ze souboru XML a nastaví odpovídající vlastnosti.
        /// </summary>
        public void LoadConfiguration()
        {
            LogHandler Log = Program.Log;
            try
            {
                XDocument doc = XDocument.Load(filePath);
                InputFilePath = GetValueFromXml(doc, "InputFilePath");
                OutputFilePath = GetValueFromXml(doc, "OutputFilePath");
                LogFilePath = GetValueFromXml(doc, "LogFilePath");
                DictionaryFilePath = GetValueFromXml(doc, "DictionaryFilePath");
            }
            catch (Exception ex)
            {
                Log.Write($"Chyba při načítání konfigurace: {ex.Message}");
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
