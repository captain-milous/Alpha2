using Komprese.src.CompressHandling;
using Komprese.src.FileHandling;
using Komprese.src.LogHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.src.UI
{
    public static class CompressUI
    {
        /// <summary>
        /// Instance třídy ConfigurationLoader pro načítání konfiguračních informací.
        /// </summary>
        static ConfigurationLoader Config = Program.Config;
        /// <summary>
        /// Instance třídy LogHandler pro zpracování a zápis logů aplikace.
        /// </summary>
        static LogHandler Log = Program.Log;
        /// <summary>
        /// Instance třídy FileHandler pro manipulaci se soubory.
        /// </summary>
        static FileHandler FileHandler = new FileHandler();

        /// <summary>
        /// Slovník obsahující kompresní zkratky.
        /// </summary>
        static Dictionary<string, string> CompressDict = MainMenuUI.CompressDict;

        public static void StartCompression(string UserName)
        {
            string text = string.Empty;
            if (!File.Exists(Config.InputFilePath))
            {
                Console.WriteLine($"Soubor na cestě {Config.InputFilePath} neexistuje.");
                Log.Write($"Soubor na cestě {Config.InputFilePath} neexistuje.");
            }
            else
            {
                try
                {
                    text = FileHandler.ReadFromFile(Config.InputFilePath);
                    Console.WriteLine(text);
                    Console.WriteLine($"Text byl načten z {Config.InputFilePath}.");
                }
                catch
                {
                    text = string.Empty;
                    Console.WriteLine($"Text se nepodařilo načíst {Config.InputFilePath}.");
                    Log.Write($"Text se nepodařilo načíst {Config.InputFilePath}.");
                }
                Compression compressFile = new Compression(text);
                if (!string.IsNullOrEmpty(compressFile.CompressText))
                {
                    Console.WriteLine($"Text se úspěšně zkompimoval.");
                    Log.Write($"{UserName} úspěšně zkompimoval text: {Config.InputFilePath}.");
                    if (!File.Exists(Config.OutputFilePath))
                    {
                        File.Create(Config.OutputFilePath);
                    }
                    try
                    {
                        FileHandler.WriteToFile(Config.OutputFilePath, compressFile.CompressText);
                        Log.Write($"Zkomprimovaný text byl úspěšně uložen do složky {Config.OutputFilePath}.");
                        Console.WriteLine($"Zkomprimovaný text byl úspěšně uložen do složky {Config.OutputFilePath}.");
                    }
                    catch
                    {
                        Console.WriteLine($"Text se nepodařilo uložit do souboru {Config.OutputFilePath}");
                        Log.Write($"Text se nepodařilo uložit do souboru {Config.OutputFilePath}");
                    }
                }
                else
                {
                    Log.Write($"V textovém souboru {Config.InputFilePath} není žádný text.");
                }
                try
                {
                    FileHandler.WriteDictToXml(CompressDict, Config.DictionaryFilePath);
                }
                catch
                {
                    Log.Write("Nastala nečekaná chyba.");
                }
            }
        }

        public static void StartDecompression(string UserName) 
        {
            string text = string.Empty;
            if (!File.Exists(Config.InputFilePath))
            {
                Console.WriteLine($"Soubor na cestě {Config.InputFilePath} neexistuje.");
                Log.Write($"Soubor na cestě {Config.InputFilePath} neexistuje.");
            }
            else
            {
                Compression decompressFile = new Compression(text);
                if (!string.IsNullOrEmpty(decompressFile.CompressText))
                {
                    try
                    {
                        FileHandler.WriteToFile(Config.OutputFilePath, decompressFile.RawText);
                        Log.Write("Zkomprimovaný text byl úspěšně uložen.");
                        Console.WriteLine("Zkomprimovaný text byl úspěšně uložen.");
                    }
                    catch
                    {
                        Log.Write("Nastaly potíže při ukládání zkomprimovaného textu.");
                    }
                }
                else
                {
                    Log.Write($"V textovém souboru {Config.InputFilePath} není žádný text.");
                }
            }
        }
    }
}
