using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TwitchChatSpeech
{
    class TwitchBot
    {
        TwitchClient _client;

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

            _client.OnLog += Client_OnLog;
            _client.OnJoinedChannel += Client_OnJoinedChannel;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnWhisperReceived += Client_OnWhisperReceived;
            _client.OnNewSubscriber += Client_OnNewSubscriber;
            _client.OnConnected += Client_OnConnected;

            _client.Connect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            //Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            //client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            string chat = ReplacementWordsController.Replace(e.ChatMessage.Message);
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
                    else ReplacementWordsController.AddReplace(add.Pattern, add.Replace);

                    break;
                default:
                    break;
            }

            //ISpeech speech = new MicrosoftSpeech();
            ISpeech speech = new GoogleSpeech();
            if (!speech.SpeechWord(chat))
            {
                Console.WriteLine("Speech failed.");
            }

            //if (e.ChatMessage.Message.Contains("badword"))
            //    client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            //if (e.WhisperMessage.Username == "my_friend")
            //    client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            //if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
            //    client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            //else
            //    client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }


    }
}
