using Komprese.src.LogHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Komprese.src.FileHandling
{
    /// <summary>
    /// Třída FileHandler poskytuje metody pro čtení a zápis do souborů, a také pro práci s obsahem slovníku a jeho ukládání do/čtení ze souborů ve formátu XML.
    /// </summary>
    public class FileHandler
    {
        private static LogHandler Log = Program.Log;
        /// <summary>
        /// Konstruktor třídy FileHandler.
        /// </summary>
        public FileHandler() { }

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
            catch (FileNotFoundException ex)
            {
                // Zpracování chyby, kdy soubor není nalezen
                Log.Write($"Soubor {filePath} nenalezen: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Zpracování chyby, kdy není oprávnění pro čtení souboru
                Log.Write($"Nedostatečná oprávnění k {filePath}: {ex.Message}");
            }
            catch (IOException ex)
            {
                // Zpracování obecné chyby vstupně-výstupní operace
                Log.Write($"Chyba při čtení souboru {filePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Zpracování ostatních neočekávaných chyb
                Log.Write($"Neočekávaná chyba při čtení souboru {filePath}: {ex.Message}");
            }
            return null;
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
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Write($"Nemáte oprávnění k zápisu do {filePath}: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Log.Write($"Chyba: {filePath} není textový soubor: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Write($"Neočekávaná chyba při zápisu do souboru {filePath}: {ex.Message}");
            }
        }

        /// <summary>
        /// Ukládá obsah slovníku do XML souboru.
        /// </summary>
        /// <param name="dict">Slovník, který má být uložen do XML souboru.</param>
        /// <param name="filePath">Cesta k cílovému XML souboru</param>
        public void WriteDictToXml(Dictionary<string, string> dict, string filePath)
        {
            try
            {
                SerializableDictionary serializableDict = new SerializableDictionary();
                foreach (var kvp in dict)
                {
                    serializableDict.Items.Add(kvp);
                }

                DataContractSerializer serializer = new DataContractSerializer(typeof(SerializableDictionary));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    serializer.WriteObject(fs, serializableDict);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Write($"Nemáte oprávnění k zápisu do {filePath}: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Log.Write($"Chyba: {filePath} není textový soubor: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Write($"Neočekávaná chyba při zápisu do souboru {filePath}: {ex.Message}");
            }
        }

        /// <summary>
        /// Načítá obsah slovníku ze XML souboru.
        /// </summary>
        /// <param name="filePath">Cesta k existujícímu XML souboru.</param>
        /// <returns>Slovník přečtený ze souboru.</returns>
        public Dictionary<string, string> ReadDictFromXml(string filePath)
        {
            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(SerializableDictionary));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    SerializableDictionary serializableDict = (SerializableDictionary)serializer.ReadObject(fs);

                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    foreach (var kvp in serializableDict.Items)
                    {
                        dict.Add(kvp.Key, kvp.Value);
                    }

                    return dict;
                }
            }
            catch (FileNotFoundException ex)
            {
                Log.Write($"Soubor {filePath} nenalezen: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Write($"Nedostatečná oprávnění k {filePath}: {ex.Message}");
            }
            catch (IOException ex)
            {
                Log.Write($"Chyba při čtení souboru {filePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Write($"Neočekávaná chyba při čtení souboru {filePath}: {ex.Message}");
            }
            return null;
        }

    }
}
