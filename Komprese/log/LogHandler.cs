using Komprese.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komprese.log
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHandler
    {
        private FileHandler fileHandler = new FileHandler();
        public string LogFilePath { get; private set; }

        LogHandler(string path)
        {
            LogFilePath = path;
        }

        private string ReadLogFile()
        {
            return fileHandler.ReadFromFile(LogFilePath);
        }

        public void WriteLogFile(string text) 
        {
            string output = string.Empty;
            if (!string.IsNullOrEmpty(ReadLogFile())) 
            { 
                output = ReadLogFile() + "\n";
            }
            output += DateTime.Now + ": " + text;
            fileHandler.WriteToFile(LogFilePath, output);
        }
    }
}
