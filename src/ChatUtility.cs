using System;
using System.Text.RegularExpressions;

namespace TTVChatSpeech
{
    static class ChatUtility
    {
        public static void Urlfinder(string chat)
        {
            Regex re = new Regex(@"(http|ftp|https)://([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match m = re.Match(chat);
            for (int gIdx = 0; gIdx < m.Groups.Count; gIdx++)
            {
                Console.WriteLine("[{0}] = {1}", re.GetGroupNames()[gIdx], m.Groups[gIdx].Value);
            }
        }

        public static void RegexPatternTest(string text)
        {
            string replacement = "$1";
            string patternJapanese = $@"一-龯ぁ-んァ-ン";

            string pattern = $@"^([A-Za-z]+)[\s{patternJapanese}].*";
            //string pattern = $@"^([{patternJapanese}]+).*";

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            Match match = Regex.Match(text, pattern, options);
            if (match.Success)
            {
                Console.WriteLine("◆◆◆◆◆◆◆");
                Console.WriteLine(match.Result(replacement));
            }
        }
    }
}
