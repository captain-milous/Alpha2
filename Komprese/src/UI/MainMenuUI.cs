using Komprese.src.CompressHandling;
using Komprese.src.FileHandling;
using Komprese.src.LogHandling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Komprese.src.UI
{
    /// <summary>
    /// Třída MainMenuUI obsahuje uživatelské rozhraní a interakce s uživatelem.
    /// </summary>
    public static class MainMenuUI
    {
        /// <summary>
        /// Výchozí uživatelské jméno, použité v případě, kdy uživatel nezadá žádné jméno.
        /// </summary>
        static string UserName = "Anonymous";
        /// <summary>
        /// Instance třídy LogHandler pro zpracování a zápis logů aplikace.
        /// </summary>
        static LogHandler Log = Program.Log;
        /// <summary>
        /// Instance třídy FileHandler pro manipulaci se soubory.
        /// </summary>
        static FileHandler FileHandler = new FileHandler();
        /// <summary>
        /// Instance třídy ConfigurationLoader pro načítání konfiguračních informací.
        /// </summary>
        static ConfigurationLoader Config = Program.Config;
        /// <summary>
        /// Slovník obsahující kompresní zkratky.
        /// </summary>
        public static Dictionary<string, string> CompressDict = new Dictionary<string, string>();
        /// <summary>
        /// Slovník obsahující zkratky pro příkazy.
        /// </summary>
        static Dictionary<string, string> Zkratky = new Dictionary<string, string>() 
        {
            { "?", "help" },
            { "he", "help" },
            { "x", "exit" },
            { "ex", "exit" },
            { "exi", "exit" },
            { "co" , "compress" },
            { "de" , "decompress" },
            { "in" , "input" },
            { "out" , "output" },
        };
        /// <summary>
        /// Spustí hlavní uživatelské rozhraní.
        /// </summary>
        /// <param name="run">Určuje, zda má program běžet nebo ukončit.</param>
        public static void Start(bool run)
        {
            if (run)
            {
                try
                {
                    CompressDict = FileHandler.ReadDictFromXml(Config.DictionaryFilePath);
                }
                catch
                {
                    Log.Write($"Nepovedlo se načíst ze souboru {Config.DictionaryFilePath}.");
                    Console.WriteLine($"Nepovedlo se načíst ze souboru {Config.DictionaryFilePath}.");
                    run = false;
                }
                if (run)
                {
                    Console.Write("Zadejte své uživatelské jméno: ");
                    string input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                    {
                        UserName = input;
                    }
                    Log.Write($"{UserName} se přihlásil.");
                }
                while (run)
                {
                    #region Input from user
                    string lineStartText = UserName + ": " + Config.InputFilePath + "> ";
                    Console.Write(lineStartText);
                    string input = Console.ReadLine().ToLower();
                    Console.WriteLine();
                    if (input.Length < 4)
                    {
                        if (Zkratky.TryGetValue(input, out string hodnota))
                        {
                            input = hodnota;
                        }
                    }

                    Commands userCommand = Commands.def;
                    try
                    {
                        userCommand = (Commands)Enum.Parse(typeof(Commands), input, true);
                    }
                    catch
                    {
                        Log.Write($"{UserName} použil neexistující příkaz.");
                    }
                    #endregion
                    switch (userCommand)
                    {
                        case Commands.help:
                            HelpHandler.Start();
                            break;
                        case Commands.compress:
                            string text = string.Empty;
                            if (!File.Exists(Config.InputFilePath))
                            {
                                Console.WriteLine($"Soubor na cestě {Config.InputFilePath} neexistuje.");
                                Log.Write($"Soubor na cestě {Config.InputFilePath} neexistuje.");
                            }
                            else
                            {
                                Compression compressFile = new Compression(text);
                                if (!string.IsNullOrEmpty(compressFile.CompressText))
                                {
                                    try
                                    {
                                        FileHandler.WriteToFile(Config.OutputFilePath, compressFile.CompressText);
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
                                try
                                {
                                    FileHandler.WriteDictToXml(CompressDict, Config.DictionaryFilePath);
                                }
                                catch 
                                {
                                    Log.Write("Nastala nečekaná chyba.");
                                }
                            }
                            Console.WriteLine();
                            break;
                        case Commands.decompress:
                            text = string.Empty;
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
                            Console.WriteLine();
                            break;
                        case Commands.input:
                            Console.WriteLine($"Cesta k souboru, ze kterého chcete načítat: {Config.InputFilePath}");
                            Console.Write("Napište novou cestu k souboru: ");
                            string newInputPath = Console.ReadLine();

                            if (!string.IsNullOrEmpty(newInputPath))
                            {
                                if(newInputPath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (File.Exists(newInputPath))
                                    {
                                        Log.Write($"{UserName} změnil InputFilePath z {Config.InputFilePath} na {newInputPath}.");
                                        Config.InputFilePath = newInputPath;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Soubor, který jste zadali neexistuje. Chcete ho vytvořit?");
                                        input = Console.ReadLine().ToLower();
                                        if(input == "ano" || input == "yes" || input == "y" || input == "a")
                                        {
                                            Log.Write($"{UserName} změnil InputFilePath z {Config.InputFilePath} na {newInputPath}.");
                                            Config.InputFilePath = newInputPath;
                                            try
                                            {
                                                // Vytvoření prázdného souboru
                                                using (FileStream fs = File.Create(Config.InputFilePath))
                                                {
                                                    Console.WriteLine($"{Config.InputFilePath} byl úspěšně vytvořen!");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"Nastala chyba při vytváření souboru: {ex.Message}");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Musíte zadat textový soubor!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Cesta k souboru nesmí být prázdná!\n");
                            }
                            Console.WriteLine();
                            break;
                        case Commands.output:
                            Console.WriteLine($"Cesta k souboru, do kterého chcete komprimovat/dekomprimovat: {Config.OutputFilePath}");
                            Console.Write("Napište novou cestu k souboru: ");
                            string newOutputPath = Console.ReadLine();

                            if (!string.IsNullOrEmpty(newOutputPath))
                            {
                                if (newOutputPath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!File.Exists(newOutputPath))
                                    {
                                        Log.Write($"{UserName} změnil OutputFilePath z {Config.OutputFilePath} na {newOutputPath}.");
                                        Config.OutputFilePath = newOutputPath;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Soubor, který jste zadali již existuje!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Musíte zadat textový soubor!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Cesta k souboru nesmí být prázdná!\n");
                            }
                            Console.WriteLine();
                            break;
                        case Commands.exit:
                            Log.Write($"{UserName} ukončil program");
                            run = false;
                            break;
                        default:
                            Console.WriteLine("Napište 'help' pro nápovědu.");
                            break;
                    }
                }
            }
        }
    }
}
