using Xunit;

namespace TTVChatSpeech.Test
{
    public class AudioPlayerMock : IAudioPlayer
    {
        public bool Play()
        {
            return true;
        }

        public bool Dispose()
        {
            return true;
        }
    }

    public class TestAudioPlayer
    {
        private readonly string fileName = "test.wav";

        [Fact]
        public void CanPlayAudioFile()
        {
            AudioPlayer player = new AudioPlayer(fileName, new AudioPlayerMock());

            Assert.True(player.Play());
        }
    }

    interface IAudioPlayer
    {
        bool Play();
        bool Dispose();
    }

    class AudioPlayer
    {
        private readonly string _fileName;
        private IAudioPlayer _audioPlayer;

        public AudioPlayer(string fileName, IAudioPlayer audioPlayer)
        {
            _fileName = fileName;
            _audioPlayer = audioPlayer;
        }

        public bool Play()
        {
            _audioPlayer.Play();
            _audioPlayer.Dispose();

            return true;
        }
    }
}
