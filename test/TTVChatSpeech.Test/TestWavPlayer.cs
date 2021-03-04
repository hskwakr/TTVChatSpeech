using Xunit;
using TTVChatSpeech.Audio;

namespace TTVChatSpeech.Test
{
    public class TestWavPlayer
    {
        private readonly string fileName = "./audio/test.wav";

        [Fact]
        public void CanPlayWavAudioFile()
        {
            WavPlayer success = new WavPlayer(fileName);
            WavPlayer fail = new WavPlayer("");

            Assert.True(success.Play());
            Assert.False(fail.Play());
        }

        [Fact]
        public void CanDispose()
        {
            WavPlayer fileExist = new WavPlayer(fileName);
            WavPlayer fileEmpty = new WavPlayer("");

            Assert.True(fileExist.Dispose());
            Assert.True(fileEmpty.Dispose());
        }
    }
}
