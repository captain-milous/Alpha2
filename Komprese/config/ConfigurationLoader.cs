using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Komprese.config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationLoader
    {
        private readonly string filePath;

        public string InputFilePath { get; private set; }
        public string OutputFilePath { get; private set; }
        public string LogFilePath { get; private set; }
        public string DictionaryFilePath { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public ConfigurationLoader(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadConfiguration()
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);

                // Načtení hodnot z XML
                InputFilePath = GetValueFromXml(doc, "InputFilePath");
                OutputFilePath = GetValueFromXml(doc, "OutputFilePath");
                LogFilePath = GetValueFromXml(doc, "LogFilePath");
                DictionaryFilePath = GetValueFromXml(doc, "DictionaryFilePath");

                // Další načítané hodnoty podle potřeby

                Console.WriteLine("Konfigurace načtena úspěšně.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při načítání konfigurace: {ex.Message}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        private string GetValueFromXml(XDocument doc, string elementName)
        {
            XElement element = doc.Element("Configuration")?.Element(elementName);
            return element?.Value ?? string.Empty;
        }
    }
}
