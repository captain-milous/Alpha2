using Komprese.src.LogHandling;
using Komprese.src.UI;
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
    /// Třída Compression poskytuje metody pro kompresi a dekompresi textu pomocí jednoduchého slovníkového kódování.
    /// </summary>
    public class Compression
    {
        /// <summary>
        /// Vstupní text před kompresí/ Text po dekompresi.
        /// </summary>
        public string RawText { get; private set; }
        /// <summary>
        /// Kompresovaný text.
        /// </summary>
        public string CompressText { get; private set;}
        /// <summary>
        /// Inicializuje novou instanci třídy Compression pro kompresi vstupního textu.
        /// </summary>
        /// <param name="text">Vstupní text k zpracování.</param>
        public Compression(string text)
        {
            RawText = text;
            CompressText = Compress(text);
        }
        /// <summary>
        /// Inicializuje novou instanci třídy Compression pro kompresi nebo dekompresi vstupního textu.
        /// </summary>
        /// <param name="text">Vstupní text k zpracování.</param>
        /// <param name="decompress">Určuje, zda má být provedena dekompresia (true) nebo komprese (false).</param>
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
        /// <summary>
        /// Kompresuje vstupní text pomocí jednoduchého slovníkového kódování.
        /// </summary>
        /// <param name="text">Vstupní text k zpracování.</param>
        /// <returns>Kompresovaný text.</returns>
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
                    if (!MainMenuUI.CompressDict.ContainsValue(word))
                    {
                        if (!MainMenuUI.CompressDict.ContainsKey(key))
                        {
                            MainMenuUI.CompressDict.Add(key, word);
                        }
                        else
                        {
                            int index = 1;
                            string newKey;
                            do
                            {
                                newKey = $"{key}{index}";
                                index++;
                            } while (MainMenuUI.CompressDict.ContainsKey(newKey));

                            MainMenuUI.CompressDict.Add(newKey, word);
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
        /// Dekompresuje komprimovaný text pomocí jednoduchého slovníkového kódování.
        /// </summary>
        /// <param name="text">Kompresovaný text k zpracování.</param>
        /// <returns>Dekomprimovaný text.</returns>
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
                    if (MainMenuUI.CompressDict.ContainsKey(key))
                    {
                        if (string.IsNullOrEmpty(output))
                        {
                            output += MainMenuUI.CompressDict[key];
                        }
                        else
                        {
                            output += " " + MainMenuUI.CompressDict[key];
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
