using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.IO;
using TwitchLib.Api.Core.Extensions.System;
using System.Text.RegularExpressions;

namespace TwitchChatSpeech
{
    class ReplacementWord
    {
        public string Pattern { get; set; }
        public string Replace { get; set; }

        public ReplacementWord(string pattern, string replace)
        {
            Pattern = pattern;
            Replace = replace;
        }
    }

    static class ReplacementWordsFile
    {
        static string file = "replace.json";

        public static IEnumerable<ReplacementWord> Read()
        {
            return ReadFile(file);
        }

        public static void Add(string pattern, string replace)
        {
            ReplacementWord[] words = new ReplacementWord[]
            {
                new ReplacementWord(pattern, replace)
            };
            WriteFile(file, words);
        }

        private static void WriteFile(string fileName, params ReplacementWord[] words)
        {
            ReplacementWord[] original;

            if (!File.Exists(fileName))
            {
                original = new ReplacementWord[] { };
            }
            else
            {
                original = ReadFile(fileName);
            }
        
            IList<ReplacementWord> unique = new List<ReplacementWord>();

            // If the File have nothing
            if (original.Length == 0)
            {
                foreach (var y in words)
                {
                    unique.Add(new ReplacementWord(y.Pattern, y.Replace));
                }
            }

            foreach (var x in original)
            {
                foreach (var y in words)
                {
                    if (String.Compare(x.Pattern, y.Pattern) == 0)
                    {
                        if (String.Compare(x.Replace, y.Replace) == 0)
                        {
                            unique.Add(new ReplacementWord(y.Pattern, y.Replace));
                        }
                    }
                    else
                    {
                        unique.Add(new ReplacementWord(x.Pattern, x.Replace));
                        unique.Add(new ReplacementWord(y.Pattern, y.Replace));
                    }
                }
            }

            File.WriteAllText(fileName, JsonConvert.SerializeObject(unique.ToArray()));
        }

        private static ReplacementWord[] ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new ReplacementWord[] { };
            }

            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<ReplacementWord[]>(json);
        }
    }

    static class ReplacementWordsController
    {
        public static string Replace(string chat)
        {
            //Word[] words = new Word[]
            //{
            //    new Word{ 
            //        Pattern = @"([8]{4,})", 
            //        Replace = "ぱちぱち" 
            //    },
            //    new Word{ 
            //        Pattern = @"(http|ftp|https)://([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?", 
            //        Replace = "url" 
            //    }
            //};

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            foreach (var word in ReplacementWordsFile.Read())
            {
                Match match = Regex.Match(chat, word.Pattern, options);

                //Console.WriteLine(match.Success.ToString());
                if (match.Success)
                {
                    // replace
                    chat = chat.Replace(match.Value, word.Replace);
                    //Console.WriteLine(chat);
                }
            }

            return chat;
        }

        public static void AddReplace(string pattern, string replace)
        {
            ReplacementWordsFile.Add(pattern, replace);
        }
    }
}
