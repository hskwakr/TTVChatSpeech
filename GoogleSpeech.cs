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
        private static TextToSpeechClient client = TextToSpeechClient.Create();
        private static string mediaFile = "AudioFileForGoogleSpeech.mp3";
        
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
                AudioEncoding = AudioEncoding.Mp3
            };

            // Perform the text-to-speech request.
            var response = client.SynthesizeSpeech(input, voiceSelection, audioConfig);

            if (File.Exists(mediaFile))
            {
                File.Delete(mediaFile);
            }

            // Write the response to the output file.
            using (var output = File.Create(mediaFile))
            {
                response.AudioContent.WriteTo(output);
                //output.Close();
            }

            Mp3Player player = new Mp3Player(mediaFile);
            player.Repeat = false;
            player.Play();

            File.Delete(mediaFile);
            //Console.WriteLine("Audio content written to file \"output.mp3\"");
        }
    }
}
