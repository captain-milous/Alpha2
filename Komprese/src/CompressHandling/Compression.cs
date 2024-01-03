using Komprese.src.LogHandling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Komprese.src.CompressHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class Compression
    {
        private char[] interpunkce = { '.', '!', '?', ',', ';', ':', '(', ')', '[', ']', '{', '}', '-', '—', '–', '„', '“', '”', '‘', '’' };
        public string RawText { get; private set; }
        public string CompressText { get; private set;}
        public Dictionary<string, string> CompressDict { get; private set; }
        public LogHandler Log { get; private set; }

        public Compression(string text, Dictionary<string, string> compressDict)
        {
            CompressDict = compressDict;
            RawText = text;
            CompressText = Compress(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="decompress"></param>
        public Compression(string text, Dictionary<string, string> compressDict, bool decompress)
        {
            CompressDict = compressDict;
            if (decompress)
            {
                CompressText = text;
                RawText = Decompress(text);
            } 
            else
            {
                RawText = text;
                CompressText = Compress(text);
            }
        }

        private string Compress(string text)
        {
            //Console.WriteLine(text);
            string output = string.Empty;

            var punctuationMatches = Regex.Matches(text, @"[.,;!?""'\-]");
            char[] interpunctionOrder = string.Join("", punctuationMatches.Select(match => match.Value)).ToCharArray();

            var parts = Regex.Split(text, @"[.,;!?""'\-]").ToList();
            List<string> splitText = parts.Select(part => part.Trim()).Where(part => !string.IsNullOrEmpty(part)).ToList();

            Console.WriteLine(interpunctionOrder.Length);
            Console.WriteLine(splitText.Count);

            for(int i = 0; i < splitText.Count; i++)
            {
                string sentence = splitText[i];
                string[] words = sentence.Split(' ');
                foreach (var word in words)
                {
                    string cleanedWord = Regex.Replace(word, @"[^\w\s]", "");
                    string key = cleanedWord.Length >= 3 ? cleanedWord.Substring(0, 3) : cleanedWord;
                    if (!CompressDict.ContainsValue(word))
                    {
                        if (!CompressDict.ContainsKey(key))
                        {
                            CompressDict.Add(key, word);
                        }
                        else
                        {
                            int index = 1;
                            string newKey;
                            do
                            {
                                newKey = $"{key}{index}";
                                index++;
                            } while (CompressDict.ContainsKey(newKey));

                            CompressDict.Add(newKey, word);
                            key = newKey;
                        }
                    }
                    if (string.IsNullOrEmpty(output))
                    {
                        output += key;
                    }
                    else
                    {
                        output += " " + key;
                    }
                }
                output += interpunctionOrder[i];
            }
            return output;
        }

        private string Decompress(string text)
        {
            string output = string.Empty;

            return output;
        }
    }
}
