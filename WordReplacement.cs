using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TwitchChatSpeech
{
    static class WordReplacement
    {
        public static string Replace(string chat)
        {
            Word[] words = Words.Read().ToArray();

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

            foreach (var word in Words.Read())
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
            Words.Add(pattern, replace);
        }
    }
}
