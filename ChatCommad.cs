using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using TwitchLib.Client;

namespace TwitchChatSpeech
{
    static class ChatCommad
    {
        public static string DetectCommand(string chat)
        {
            string pattern = $@"^!!([A-Za-z]+)";

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            Match match = Regex.Match(chat, pattern, options);

            if (match.Success)
            {
                //Console.WriteLine("◆◆◆◆◆◆◆");
                //Console.WriteLine(match.Result("$1"));

                return match.Result("$1");
            }
            else
            {
                return "";
            }
        }

        public static bool TryExtractCommandsCommand(string chat)
        {
            string identifier = "!!commands";

            // Split arguments with a space
            string[] commandArgs = chat.Split(null);

            if ((String.Compare(commandArgs[0], identifier) != 0) || (commandArgs.Length != 1))
            {
                return false;
            }

            return true;
        }

        public static Word TryExtractAddCommand(string chat)
        {
            string identifier = "!!add";

            // Split arguments with a space
            string[] commandArgs = chat.Split(null);

            if ((String.Compare(commandArgs[0], identifier) != 0) || (commandArgs.Length != 3))
            {
                return null;
            }

            // Convert to Word object
            Word word = new Word(commandArgs[1], commandArgs[2]);

            return word;
        }

        public static void ShowCommands(TwitchClient client, string channel)
        {
            string msg =
                "Actually doesn't need \"{}\" letters. !!add {pattern} {replacement word} : to add replacement word to this TTS. You need to use regular expression when you write pattarn.";
            client.SendMessage(channel, msg);
        }
    }
}
