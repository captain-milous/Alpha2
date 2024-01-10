using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.src.UI
{
    /// <summary>
    /// Enumerace představující seznam povolených příkazů pro kompresi a dekompresi textového souboru.
    /// </summary>
    public enum Commands
    {
        def,
        help,
        compress,
        decompress,
        input,
        output,
        logs,
        exit
    }
}
