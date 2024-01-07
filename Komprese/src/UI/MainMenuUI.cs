using Komprese.src.CompressHandling;
using Komprese.src.FileHandling;
using Komprese.src.LogHandling;
using System;
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
        static LogHandler Log = Program.Log;
        static ConfigurationLoader Config = Program.Config; 
        static FileHandler FileHandler = new FileHandler();

        static Dictionary<string, string> CompressDict = new Dictionary<string, string>();
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

        public static void Start(bool run)
        {
            try
            {

                CompressDict = FileHandler.ReadDictFromXml(Config.DictionaryFilePath);
            }
            catch 
            { 
            
            }
            //text = FileHandler.ReadFromFile(Config.InputFilePath);
            while (run)
            {
                Console.Write(Program.Config.InputFilePath+"> ");
                string input = Console.ReadLine().ToLower();

                if(input.Length < 4)
                {

                }

                Commands userCommand = Commands.def;
                try
                {
                    userCommand = (Commands)Enum.Parse(typeof(Commands), input, true);
                }
                catch 
                {
                    Log.Write("Uživatel použil neexistující příkaz.");
                }
                switch (userCommand)
                {
                    case Commands.help:
                        HelpHandler.Start();
                        break;
                    case Commands.compress:
                        string text = string.Empty;
                        Compression test = new Compression(text);
                        //Console.WriteLine(test.CompressText);
                        if (!string.IsNullOrEmpty(test.CompressText))
                        {
                            Log.Write("Text byl úspěšně zkomprimován.");
                            try
                            {
                                FileHandler.WriteToFile(Config.OutputFilePath, test.CompressText);
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

                        break;
                    case Commands.decompress: 
                        
                        break;
                    case Commands.exit:
                        Log.Write("Uživatel ukončil program");
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
