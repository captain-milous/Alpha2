﻿using Komprese.src.FileHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.src.LogHandling
{
    /// <summary>
    /// Třída LogHandler poskytuje metody pro čtení a zápis do logovacího souboru. Využívá třídu FileHandler pro manipulaci se soubory.
    /// </summary>
    public class LogHandler
    {
        private FileHandler fileHandler = new FileHandler();
        private string defaultPath = "log\\EmergencyLog.txt";
        public string LogFilePath { get; private set; }

        /// <summary>
        /// Konstruktor třídy LogHandler s defaultní hodnotou.
        /// </summary>
        public LogHandler() 
        { 
            LogFilePath = defaultPath;
        }

        /// <summary>
        /// Konstruktor třídy LogHandler.
        /// </summary>
        /// <param name="path">Cesta k logovacímu souboru.</param>
        public LogHandler(string path)
        {
            LogFilePath = path;
        }

        /// <summary>
        /// Čte obsah logovacího souboru.
        /// </summary>
        /// <returns>Obsah logovacího souboru nebo prázdný řetězec v případě chyby.</returns>
        private string ReadLogFile()
        {
            LogHandler log = new LogHandler();
            try
            {
                return fileHandler.ReadFromFile(LogFilePath);
            }
            catch (Exception ex) 
            {
                log.Write($"Chyba při čtení ze souboru {LogFilePath}: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Zapíše text do logovacího souboru s aktuálním datem a časem.
        /// </summary>
        /// <param name="text">Text k zapsání do logovacího souboru.</param>
        public void Write(string text)
        {
            LogHandler log = new LogHandler();
            try
            {
                string output = string.Empty;
                if (!string.IsNullOrEmpty(ReadLogFile()))
                {
                    output = ReadLogFile() + "\n";
                }
                if (!string.IsNullOrEmpty(text))
                {
                    output += DateTime.Now + ": " + text;
                }
                else
                {
                    output += DateTime.Now + $": Pokus o zapsání do {LogFilePath} se nepovedl.";
                }
                fileHandler.WriteToFile(LogFilePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při zapsání do souboru {LogFilePath}: {ex.Message}");
                log.Write($"Chyba při zapsání do souboru {LogFilePath}: {ex.Message}");
            }

        }
    }
}
