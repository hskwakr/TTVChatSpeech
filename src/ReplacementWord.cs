using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.IO;
using TwitchLib.Api.Core.Extensions.System;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;

namespace TwitchChatSpeech
{
    class ReplacementWord : IComparable<ReplacementWord>
    {
        public string Pattern { get; set; }
        public string Replace { get; set; }

        public ReplacementWord(string pattern, string replace)
        {
            Pattern = pattern;
            Replace = replace;
        }

        public int CompareTo(ReplacementWord other)
        {
            if (String.Compare(this.Pattern, other.Pattern) == 0)
            {
                return 0;
            }

            return 1;
        }
    }

    class ReplacementWordsFile : JsonFileIO
    {
        private static string file = "replace.json";

        public static IList<ReplacementWord> Read()
        {
            return ReadFile<ReplacementWord>(file);
        }

        public static void Add(string pattern, string replace)
        {
            IList<ReplacementWord> words = new List<ReplacementWord>()
            {
                new ReplacementWord(pattern, replace)
            };
            WriteFile<ReplacementWord>(file, words);
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
