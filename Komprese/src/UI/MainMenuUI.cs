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
    public static class MainMenuUI
    {
        static string Oddelovac = Program.Oddelovac;
        static string UserName = "Anonymous";
        static LogHandler Log = Program.Log;
        static ConfigurationLoader Config = Program.Config; 
        static FileHandler FileHandler = new FileHandler();

        public static Dictionary<string, string> CompressDict = new Dictionary<string, string>();
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
        /// 
        /// </summary>
        /// <param name="run"></param>
        public static void Start(bool run)
        {
            if (run)
            {
                string InputFile = Config.InputFilePath;
                string OutputFile = Config.OutputFilePath;
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
                    string lineStartText = UserName + ": " + InputFile + "> ";
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
                    switch (userCommand)
                    {
                        case Commands.help:
                            HelpHandler.Start();
                            break;
                        case Commands.compress:
                            string text = string.Empty;
                            if (!File.Exists(InputFile))
                            {
                                Console.WriteLine($"Soubor na cestě {InputFile} neexistuje.");
                                Log.Write($"Soubor na cestě {InputFile} neexistuje.");
                            }
                            else
                            {
                                bool runCompression = true;
                                if (File.Exists(OutputFile))
                                {

                                }

                                if (runCompression)
                                {
                                    Compression compressFile = new Compression(text);
                                }
                            }


                            Compression test = new Compression(text);
                            //Console.WriteLine(test.CompressText);
                            if (!string.IsNullOrEmpty(test.CompressText))
                            {
                                Log.Write("Text byl úspěšně zkomprimován.");
                                try
                                {
                                    FileHandler.WriteToFile(OutputFile, test.CompressText);
                                    Log.Write("Zkomprimovaný text byl úspěšně uložen.");
                                }
                                catch
                                {
                                    Log.Write("Nastaly potíže při ukládání zkomprimovaného textu.");
                                }
                            }
                            else
                            {
                                Log.Write("Text se nepodařilo zkomprimovat.");
                            }
                            try
                            {
                                FileHandler.WriteDictToXml(CompressDict, Config.DictionaryFilePath);
                            } catch { }
                            break;
                        case Commands.decompress:

                            break;
                        case Commands.input:
                            Console.WriteLine($"Cesta k souboru, ze kterého chcete načítat: {InputFile}");
                            Console.Write("Napište novou cestu k souboru: ");
                            string newInputPath = Console.ReadLine();

                            if (!string.IsNullOrEmpty(newInputPath))
                            {
                                if(newInputPath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (File.Exists(newInputPath))
                                    {
                                        Log.Write($"{UserName} změnil InputFilePath z {InputFile} na {newInputPath}.");
                                        InputFile = newInputPath;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Soubor, který jste zadali neexistuje. Chcete ho vytvořit?");
                                        input = Console.ReadLine().ToLower();
                                        if(input == "ano" || input == "yes" || input == "y" || input == "a")
                                        {
                                            Log.Write($"{UserName} změnil InputFilePath z {InputFile} na {newInputPath}.");
                                            InputFile = newInputPath;
                                            try
                                            {
                                                // Vytvoření prázdného souboru
                                                using (FileStream fs = File.Create(InputFile))
                                                {
                                                    Console.WriteLine($"{InputFile} byl úspěšně vytvořen!");
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
                            Console.WriteLine($"Cesta k souboru, do kterého chcete komprimovat/dekomprimovat: {OutputFile}");
                            Console.Write("Napište novou cestu k souboru: ");
                            string newOutputPath = Console.ReadLine();

                            if (!string.IsNullOrEmpty(newOutputPath))
                            {
                                if (newOutputPath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!File.Exists(newOutputPath))
                                    {
                                        Log.Write($"{UserName} změnil OutputFilePath z {OutputFile} na {newOutputPath}.");
                                        InputFile = newOutputPath;
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
                            Console.WriteLine("Napište help pro nápovědu.");
                            break;
                    }
                }
            }
        }
    }
}
