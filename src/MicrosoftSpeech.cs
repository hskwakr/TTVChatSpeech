using Microsoft.Speech.Synthesis;

namespace TwitchChatSpeech
{
    class MicrosoftSpeech : Speech
    {
        protected override void DoSpeech(string culture, string chat)
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

        
    }
}
