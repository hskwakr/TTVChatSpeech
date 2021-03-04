using Xunit;
using TTVChatSpeech.Audio;

namespace TTVChatSpeech.Test
{

    public class TestAudioPlayer
    {
        private readonly string fileName = "test.wav";

        [Fact]
        public void CanPlayAudioFile()
        {
            AudioPlayer player1 = new AudioPlayer(new AudioPlayerMock(true, true));
            AudioPlayer player2 = new AudioPlayer(new AudioPlayerMock(false, true));
            AudioPlayer player3 = new AudioPlayer(new AudioPlayerMock(true, false));

            Assert.True(player1.Play());
            Assert.False(player2.Play());
            Assert.False(player3.Play());
        }
    }
}
