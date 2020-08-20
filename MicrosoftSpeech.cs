using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using Microsoft.Speech.Synthesis;

namespace TwitchChatSpeech
{
    static class MicrosoftSpeech
    {
        private static void Speech(string culture, string chat)
        {
            // Initialize a new instance of the SpeechSynthesizer.
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();

                // Set the volume of the TTS voice, and the combined output volume.
                synth.TtsVolume = 20;
                synth.Volume = 20;

                // Build a prompt containing recorded audio and synthesized speech.
                PromptBuilder builder = new PromptBuilder(
                  new System.Globalization.CultureInfo(culture));
                //builder.AppendAudio("C:\\Test\\WelcomeToContosoRadio.wav");
                builder.AppendText(chat);

                // Speak the prompt.
                synth.Speak(builder);
            }
        }

        // Try to speech Japanese and Englsih.
        public static bool SpeechWord(string text)
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
                    Speech(cultureEnglish, match.Result(replacement));
                }
                else
                {
                    match = Regex.Match(subtractedChat, patternJapaneseWord, options);

                    // Japanese word
                    if (match.Success)
                    {
                        subtractedChat = subtractedChat.Substring(match.Result(replacement).Length);
                        //Console.WriteLine(match.Result(replacement));
                        Speech(cultureJapanese, match.Result(replacement));
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
