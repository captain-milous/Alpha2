using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.src.UI
{
    public class FileHandler
    {
        /// <summary>
        /// Čte obsah souboru na zadané cestě.
        /// </summary>
        /// <param name="filePath">Cesta k souboru, který má být přečten.</param>
        /// <returns>Obsah přečteného souboru nebo null v případě chyby.</returns>
        public string ReadFromFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při čtení souboru {filePath}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Zapíše zadaný obsah do souboru na zadané cestě.
        /// </summary>
        /// <param name="filePath">Cesta k souboru, do kterého má být obsah zapsán.</param>
        /// <param name="content">Obsah k zápisu do souboru.</param>
        public void WriteToFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
                Console.WriteLine($"Obsah úspěšně zapsán do souboru {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při zápisu do souboru {filePath}: {ex.Message}");
            }
        }
    }
}
