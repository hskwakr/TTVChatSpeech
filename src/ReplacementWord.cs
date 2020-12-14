using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TTVChatSpeech
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

    class ReplacementFile : JsonFileIO
    {
        private static string _filePath = "replace.json";

        public IList<ReplacementWord> Read()
        {
            return ReadFile<ReplacementWord>(_filePath);
        }

        public void Add(string pattern, string replace)
        {
            IList<ReplacementWord> words = new List<ReplacementWord>()
            {
                new ReplacementWord(pattern, replace)
            };
            WriteFile<ReplacementWord>(_filePath, words);
        }
    }

    class Replacement
    {
        private ReplacementFile _words = new ReplacementFile();

        public string Replace(string chat)
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

            foreach (var word in _words.Read())
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

        public void Add(string pattern, string replace)
        {
            _words.Add(pattern, replace);
        }

        public void Add(ReplacementWord word)
        {
            _words.Add(word.Pattern, word.Replace);
        }
    }
}
