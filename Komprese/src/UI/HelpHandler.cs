using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.src.UI
{
    public static class HelpHandler
    {
        private static Dictionary<Commands, string> description = new Dictionary<Commands, string>() 
        {
            { Commands.help, "Zobrazí seznam možných komandů." },
            { Commands.compress, "Komprimuje soubor, který byl zadán v konfiguračním souboru jako InputFilePath do souboru OutputFilePath" },
            { Commands.decompress, "Dekomprimuje soubor, který byl zadán v konfiguračním souboru jako InputFilePath do souboru OutputFilePath" },
            { Commands.exit, "Ukončí program" }
        };
        public static void Start()
        {
            Console.WriteLine("Příkazy, které můžete použít:\n");
            foreach (Commands command in description.Keys) 
            { 
                Console.WriteLine(command.ToString() + ": " + description[command].ToString());
            }
            Console.WriteLine();
        }
    }
}
