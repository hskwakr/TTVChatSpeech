using System;
using System.Text.RegularExpressions;

namespace TwitchChatSpeech
{
    abstract class Speech : ISpeech
    {
        protected abstract void DoSpeech(string culture, string chat);

        // Try to speech Japanese and Englsih.
        public bool SpeechWord(string text)
        {
            string cultureEnglish = "en-US";
            string cultureJapanese = "ja-JP";
            string replacement = "$1";

            string patternJapanese = $@"一-龯ぁ-んァ-ン";
            string patternEnglishWord = $@"^([\s]+|[^{patternJapanese}]+).*";
            string patternJapaneseWord = $@"^([\s{patternJapanese}]+).*";

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            string subtractedChat = text;
            Match match;
            while (subtractedChat != String.Empty)
            {
                match = Regex.Match(subtractedChat, patternEnglishWord, options);
                // English word
                if (match.Success)
                {
                    subtractedChat = subtractedChat.Substring(match.Result(replacement).Length);
                    //Console.WriteLine(match.Result(replacement));
                    DoSpeech(cultureEnglish, match.Result(replacement));
                }
                else
                {
                    match = Regex.Match(subtractedChat, patternJapaneseWord, options);

                    // Japanese word
                    if (match.Success)
                    {
                        subtractedChat = subtractedChat.Substring(match.Result(replacement).Length);
                        //Console.WriteLine(match.Result(replacement));
                        DoSpeech(cultureJapanese, match.Result(replacement));
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
