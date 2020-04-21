using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.Speech.Synthesis;
using Microsoft.Speech.AudioFormat;

using LanguageDetection;

namespace TwitchChatSpeech
{
    static class ChatTool
    {
        public static string Replace(string chat)
        {
            Dictionary<string, string> replaceList = new Dictionary<string, string>();
            replaceList.Add("([8]{4,})", " pach pach ");
            replaceList.Add(@"(http|ftp|https)://([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?", "url");

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            foreach (KeyValuePair<string, string> kvp in replaceList)
            {
                Match match = Regex.Match(chat, kvp.Key, options);

                //Console.WriteLine(match.Success.ToString());
                if (match.Success)
                {
                    // replace
                    chat = chat.Replace(match.Value, kvp.Value);
                    //Console.WriteLine(chat);
                }
            }

            return chat;
        }

        public static void urlfinder(string chat)
        {
            Regex re = new Regex(@"(http|ftp|https)://([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match m = re.Match(chat);
            for (int gIdx = 0; gIdx < m.Groups.Count; gIdx++)
            {
                Console.WriteLine("[{0}] = {1}", re.GetGroupNames()[gIdx], m.Groups[gIdx].Value);
            }
        }

        public static bool detectLanguage(string chat)
        {
            bool isEnglish;
            string country = "en";
            LanguageDetector detector = new LanguageDetector();

            //splitEnglishAndJapanese
            //string JapanesePattern = @"(\J+)";
            //string EnglishPattern = @"([a-zA-Z]+)";

            List<string> matchEnglish = new List<string>();
            List<string> matchJapanese = new List<string>();

            matchEnglish = matchingEnglishWords(chat);
            matchJapanese = matchingJapaneseWords(chat);

            //detector.AddAllLanguages();
            detector.AddLanguages("ja", "en");
            Console.WriteLine(detector.Detect(chat));
            if (country == detector.Detect(chat))
            {
                isEnglish = true;
            } 
            else
            {
                isEnglish = false;
            }

            return isEnglish;
        }

        private static List<string> matchingEnglishWords(string text)
        {
            List<string> matches = new List<string>();
            string pattern = @"^([A-Za-z]+)[\s\J].*";

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            Match match = Regex.Match(text, pattern, options);
            // To advance
            while(match.Success)
            {
                matches.Add(match.Value);
                
                text = text.Substring(match.Length - 1);
                match = Regex.Match(text, pattern, options);
            }

            return matches;
        }

        // I need to test RegexPatterns 
        public static void foo(string text)
        {
            List<string> matches = new List<string>(); 
            string pattern = @"^([A-Za-z]+)[\s\p{IsHiragana}].*";

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            Match match = Regex.Match(text, pattern, options);
            Console.WriteLine(match.Value);
        }

        private static List<string> matchingJapaneseWords(string text)
        {
            List<string> matches = new List<string>();
            string pattern = @"^(\J+).*";

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            Match match = Regex.Match(text, pattern, options);
            // To advance
            while (match.Success)
            {
                matches.Add(match.Value);

                text = text.Substring(match.Length - 1);
                match = Regex.Match(text, pattern, options);
            }

            return matches;
        }

        public static void CheckInstalledVoices()
        {
            // Initialize a new instance of the SpeechSynthesizer.
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                // Output information about all of the installed voices. 
                Console.WriteLine("Installed voices -");
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    string AudioFormats = "";
                    foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
                    {
                        AudioFormats += String.Format("{0}\n",
                        fmt.EncodingFormat.ToString());
                    }

                    Console.WriteLine(" Name:          " + info.Name);
                    Console.WriteLine(" Culture:       " + info.Culture);
                    Console.WriteLine(" Age:           " + info.Age);
                    Console.WriteLine(" Gender:        " + info.Gender);
                    Console.WriteLine(" Description:   " + info.Description);
                    Console.WriteLine(" ID:            " + info.Id);
                    Console.WriteLine(" Enabled:       " + voice.Enabled);
                    if (info.SupportedAudioFormats.Count != 0)
                    {
                        Console.WriteLine(" Audio formats: " + AudioFormats);
                    }
                    else
                    {
                        Console.WriteLine(" No supported audio formats found");
                    }

                    string AdditionalInfo = "";
                    foreach (string key in info.AdditionalInfo.Keys)
                    {
                        AdditionalInfo += String.Format("  {0}: {1}\n", key, info.AdditionalInfo[key]);
                    }

                    Console.WriteLine(" Additional Info - " + AdditionalInfo);
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
