using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.src.FileHandling
{
    /// <summary>
    /// Třída reprezentující serializovatelný slovník, který může být uložen do/načten ze souboru ve formátu XML.
    /// </summary>
    [DataContract]
    public class SerializableDictionary
    {
        [DataMember]
        public List<KeyValuePair<string, string>> Items { get; set; }
        /// <summary>
        /// Inicializuje novou instanci třídy SerializableDictionary s prázdným seznamem položek.
        /// </summary>
        public SerializableDictionary()
        {
            Items = new List<KeyValuePair<string, string>>();
        }
    }
}
