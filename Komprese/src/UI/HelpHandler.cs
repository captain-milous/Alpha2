using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.src.UI
{
    /// <summary>
    /// Statická třída obsahující metody pro zobrazení nápovědy k jednotlivým příkazům.
    /// </summary>
    public static class HelpHandler
    {
        /// <summary>
        /// Slovník obsahující popisy jednotlivých příkazů pro zobrazení nápovědy.
        /// </summary>
        private static Dictionary<Commands, string> description = new Dictionary<Commands, string>() 
        {
            //{ Commands.help, "Zobrazí seznam možných komandů." },
            { Commands.input, "Změna cesty k souboru, který chcete kompresovat/dekomresovat."},
            { Commands.output, "Změna cesty k souboru, do kterého chcete provést kompresy/dekomresy."},
            { Commands.compress, "Komprimuje soubor, který byl zadán v konfiguračním souboru jako InputFilePath do souboru OutputFilePath" },
            { Commands.decompress, "Dekomprimuje soubor, který byl zadán v konfiguračním souboru jako InputFilePath do souboru OutputFilePath" },
            { Commands.exit, "Ukončí program" }
        };
        /// <summary>
        /// Zobrazí nápovědu obsahující popisy jednotlivých příkazů.
        /// </summary>
        public static void Start()
        {
            Console.WriteLine("Příkazy, které můžete použít:\n");
            foreach (Commands command in description.Keys) 
            { 
                Console.WriteLine("   - " + command.ToString() + ": " + description[command].ToString());
            }
            Console.WriteLine();
        }
    }
}
