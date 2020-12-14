using System;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TTVChatSpeech
{
    class TwitchBot
    {
        private TwitchClient _client;
        private Replacement _replacement = new Replacement();
        private ISpeech _speech = new GoogleSpeech();

        public TwitchBot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials("rarararcha", Environment.GetEnvironmentVariable("TwitchToken"));

            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            _client = new TwitchClient(customClient);
            _client.Initialize(credentials, "hskwakr");

            _client.OnLog += ClientOnLog;
            _client.OnJoinedChannel += ClientOnJoinedChannel;
            _client.OnMessageReceived += ClientOnMessageReceived;
            _client.OnWhisperReceived += ClientOnWhisperReceived;
            _client.OnNewSubscriber += ClientOnNewSubscriber;
            _client.OnConnected += ClientOnConnected;

            _client.Connect();
        }

        private void ClientOnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void ClientOnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void ClientOnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            //Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            //client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        private void ClientOnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            string chat = _replacement.Replace(e.ChatMessage.Message);
            //ChatUtility.Urlfinder(e.ChatMessage.Message);
            //ChatUtility.RegexPatternTest(chat);

            var cmd = ChatCommad.DetectCommand(chat);
            switch (cmd)
            {
                case "commands":
                    var commands = ChatCommad.TryExtractCommandsCommand(chat);
                    if (commands == false) break;
                    else ChatCommad.ShowCommands(_client, e.ChatMessage.Channel);

                    break;
                case "add":
                    var add = ChatCommad.TryExtractAddCommand(chat);
                    if (add == null) break;
                    else _replacement.Add(add.Pattern, add.Replace);

                    break;
                default:
                    break;
            }

            if (!_speech.SpeechWord(chat))
            {
                Console.WriteLine("Speech failed.");
            }

            //if (e.ChatMessage.Message.Contains("badword"))
            //    client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
        }

        private void ClientOnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            //if (e.WhisperMessage.Username == "my_friend")
            //    client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }

        private void ClientOnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            //if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
            //    client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            //else
            //    client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }
    }
}
