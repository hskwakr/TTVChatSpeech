using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;
using TwitchLib.Api.Core.Extensions.System;

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

    class WordEqualityComparer : IEqualityComparer<Word>
    {
        public bool Equals(Word x, Word y)
        {
            return x.Pattern.Equals(y.Pattern);
        }

        public int GetHashCode(Word obj)
        {
            return obj.Pattern.GetHashCode();
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

        private static void WriteFile(string fileName, params Word[] words)
        {
            Word[] original;

            if (!File.Exists(fileName))
            {
                original = new Word[] { };
            }
            else
            {
                original = ReadFile(fileName);
            }

            IList<Word> unique = new List<Word>();
            foreach (var x in words)
            {
                foreach (var y in original)
                {
                    if (String.Compare(x.Pattern, y.Pattern) == 0)
                    {
                        if (String.Compare(x.Replace, y.Replace) == 0)
                        {
                            unique.Add(new Word(y.Pattern, y.Replace));
                        }
                    }
                    else
                    {
                        unique.Add(new Word(x.Pattern, x.Replace));
                    }
                }
            }

            File.WriteAllText(fileName, JsonConvert.SerializeObject(unique.ToArray()));
        }

        private static Word[] ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new Word[] { };
            }

            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Word[]>(json);
        }
    }
}
