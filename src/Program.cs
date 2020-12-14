using System;
using TTVChatSpeech.TwitchChatManage;

namespace TTVChatSpeech
{
    class Program
    {
        static void Main(string[] args)
        {
            //TwitchBot bot = new TwitchBot();
            TwitchChatManager manager = new TwitchChatManager();
            manager.ConnectToTwitch();

            Console.ReadLine();
        }
    }
}
