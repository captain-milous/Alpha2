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
        public static string Oddelovac = "\n----------------------------------------------------------------------------------\n";
        public static LogHandler Log = Program.Log;

        public static void Start(bool run)
        {

            //text = FileHandler.ReadFromFile(Config.InputFilePath);
            while (run)
            {
                Console.Write(Program.Config.InputFilePath+"> ");
                string input = Console.ReadLine();


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
