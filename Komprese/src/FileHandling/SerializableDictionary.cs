using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.src.FileHandling
{
    /// <summary>
    /// Slovník, který lze serializovat a deserializovat do XML
    /// </summary>
    [DataContract]
    public class SerializableDictionary
    {
        [DataMember]
        public List<KeyValuePair<string, string>> Items { get; set; }

        public SerializableDictionary()
        {
            Items = new List<KeyValuePair<string, string>>();
        }
    }
}
