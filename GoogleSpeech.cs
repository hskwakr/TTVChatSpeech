using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Google.Cloud.TextToSpeech.V1;

namespace TwitchChatSpeech
{
    class GoogleSpeech : Speech
    {
        private static TextToSpeechClient _client = TextToSpeechClient.Create();
        private static string _mediaFile = "AudioFileForGoogleSpeech.wav";
        
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
            var response = _client.SynthesizeSpeech(input, voiceSelection, audioConfig);

            if (File.Exists(_mediaFile))
            {
                File.Delete(_mediaFile);
            }

            // Write the response to the output file.
            using (var output = File.Create(_mediaFile))
            {
                response.AudioContent.WriteTo(output);
                //output.Close();
            }

            WavPlayer player = new WavPlayer(_mediaFile);
            player.Play();

            //Console.WriteLine("Audio content written to file \"output.mp3\"");
        }
    }
}
