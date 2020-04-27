using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace TwitchChatSpeech
{
    class Word
    {
        public string Pattern { get; set; }
        public string Replace { get; set; }

        public Word(string pattern, string replace)
        {
            Pattern = pattern;
            Replace = replace;
        }
    }

    static class Words
    {
        static string file = "replace.json";

        public static IEnumerable<Word> Read()
        {
            return ReadFile(file);
        }

        public static void Add(string pattern, string replace)
        {
            Word[] words = new Word[]
            {
                new Word(pattern, replace)
            };
            WriteFile(file, words);
        }

        private static void WriteFile(string fileName, Word[] words)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            using (FileStream fs = File.Create(fileName))
            {
                JsonSerializer.SerializeAsync(fs, words, options).Wait();
            }
        }

        private static Word[] ReadFile(string fileName)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            if (!File.Exists(fileName))
            {
                // Create the file.
                return new Word[] { };
            }

            Word[] words = new Word[]{ };
            using (FileStream fs = File.OpenRead(fileName))
            {
                words = JsonSerializer.DeserializeAsync<Word[]>(fs).Result;
            }

            return words;
        }
    }
}
