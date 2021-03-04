using System.IO;
using Google.Cloud.TextToSpeech.V1;

namespace TTVChatSpeech
{
    class GoogleSpeech : Speech
    {
        private static TextToSpeechClient _client = TextToSpeechClient.Create();
        private static string _mediaFilePath = "AudioFileForGoogleSpeech.wav";
        private object _mediaFileLock = new object();
        
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

            lock (_mediaFileLock)
            {
                if (File.Exists(_mediaFilePath))
                {
                    File.Delete(_mediaFilePath);
                }

                // Write the response to the output file.
                using (var output = File.Create(_mediaFilePath))
                {
                    response.AudioContent.WriteTo(output);
                    //output.Close();
                }

                //WavPlayer player = new WavPlayer(_mediaFilePath);
                //player.Play();

                ////Console.WriteLine("Audio content written to file \"output.mp3\"");

                //player.Dispose();
                //player = null;
            }
        }
    }
}
