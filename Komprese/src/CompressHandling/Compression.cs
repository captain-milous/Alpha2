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

        public Compression(string text)
        {
            RawText = text;
            CompressText = Compress(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="decompress"></param>
        public Compression(string text, bool decompress)
        {
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
            string output = string.Empty;

            var punctuationMatches = Regex.Matches(text, @"[.,;!?""'\-]");
            char[] interpunctionOrder = string.Join("", punctuationMatches.Select(match => match.Value)).ToCharArray();

            var parts = Regex.Split(text, @"[.,;!?""'\-]").ToList();
            List<string> splitText = parts.Select(part => part.Trim()).Where(part => !string.IsNullOrEmpty(part)).ToList();

            for(int i = 0; i < splitText.Count; i++)
            {
                string sentence = splitText[i];
                string[] words = sentence.Split(' ');
                foreach (var word in words)
                {
                    string key = word.Length >= 3 ? word.Substring(0, 3) : word;
                    if (!Program.CompressDict.ContainsValue(word))
                    {
                        if (!Program.CompressDict.ContainsKey(key))
                        {
                            Program.CompressDict.Add(key, word);
                        }
                        else
                        {
                            int index = 1;
                            string newKey;
                            do
                            {
                                newKey = $"{key}{index}";
                                index++;
                            } while (Program.CompressDict.ContainsKey(newKey));

                            Program.CompressDict.Add(newKey, word);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string Decompress(string text)
        {
            string output = string.Empty;

            var punctuationMatches = Regex.Matches(text, @"[.,;!?""'\-]");
            char[] interpunctionOrder = string.Join("", punctuationMatches.Select(match => match.Value)).ToCharArray();

            var parts = Regex.Split(text, @"[.,;!?""'\-]").ToList();
            List<string> splitText = parts.Select(part => part.Trim()).Where(part => !string.IsNullOrEmpty(part)).ToList();

            for(int i = 0; i < splitText.Count; i++)
            {
                string sentence = splitText[i];
                string[] keys = sentence.Split(' ');
                foreach (var key in keys)
                {
                    if (Program.CompressDict.ContainsKey(key))
                    {
                        if (string.IsNullOrEmpty(output))
                        {
                            output += Program.CompressDict[key];
                        }
                        else
                        {
                            output += " " + Program.CompressDict[key];
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(output))
                        {
                            output += key;
                        }
                        else
                        {
                            output += " " + key;
                        }
                    }
                }
                output += interpunctionOrder[i];
            }
            return output;
        }
    }
}
