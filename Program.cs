using System;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

using Microsoft.Speech.Synthesis;


namespace TwitchChatSpeech
{
    class Program
    {
        static void Main(string[] args)
        {
            //ChatTool.CheckInstalledVoices();
            Bot bot = new Bot();
            Console.ReadLine();
        }
    }
    class Bot
    {
        TwitchClient client;

        public Bot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials("rarararcha", Environment.GetEnvironmentVariable("TwitchToken"));

            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, "hskwakr");

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;

            client.Connect();
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
            string chat = ChatTool.Replace(e.ChatMessage.Message);
            //ChatTool.Urlfinder(e.ChatMessage.Message);
            //ChatTool.RegexPatternTest(chat);

            if (!SpeechWord(chat))
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

        private void Speech(string culture, string chat)
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

        private bool SpeechWord(string text)
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
