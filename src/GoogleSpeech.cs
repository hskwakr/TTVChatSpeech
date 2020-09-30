using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Google.Cloud.TextToSpeech.V1;

namespace TwitchChatSpeech
{
    class GoogleSpeech : Speech
    {
        private static TextToSpeechClient client = TextToSpeechClient.Create();
        private static string mediaFilePath = "AudioFileForGoogleSpeech.wav";
        private object mediaFileLock = new object();
        
        protected override void DoSpeech(string culture, string chat)
        {
            // The input to be synthesized, can be provided as text or SSML.
            var input = new SynthesisInput
            {
                Text = chat
            };

            // Build the voice request.
            var voiceSelection = new VoiceSelectionParams
            {
                LanguageCode = culture,
                SsmlGender = SsmlVoiceGender.Female
            };

            // Specify the type of audio file.
            var audioConfig = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Linear16
            };

            // Perform the text-to-speech request.
            var response = client.SynthesizeSpeech(input, voiceSelection, audioConfig);

            lock (mediaFileLock)
            {
                if (File.Exists(mediaFilePath))
                {
                    File.Delete(mediaFilePath);
                }

                // Write the response to the output file.
                using (var output = File.Create(mediaFilePath))
                {
                    response.AudioContent.WriteTo(output);
                    //output.Close();
                }

                WavPlayer player = new WavPlayer(mediaFilePath);
                player.Play();

                //Console.WriteLine("Audio content written to file \"output.mp3\"");

                player.Dispose();
                player = null;
            }
        }
    }
}
