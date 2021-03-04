using Xunit;

namespace TTVChatSpeech.Test
{
    public class AudioPlayerMock : IAudioPlayer
    {
        private readonly bool _disposable;
        private readonly bool _playable;

        public AudioPlayerMock(bool playable, bool disposable)
        {
            _disposable = disposable;
            _playable = playable;
        }

        public bool Play()
        {
            return _playable;
        }

        public bool Dispose()
        {
            return _disposable;
        }
    }

    public class TestAudioPlayer
    {
        private readonly string fileName = "test.wav";

        [Fact]
        public void CanPlayAudioFile()
        {
            AudioPlayer player1 = new AudioPlayer(fileName, new AudioPlayerMock(true, true));
            AudioPlayer player2 = new AudioPlayer(fileName, new AudioPlayerMock(false, true));
            AudioPlayer player3 = new AudioPlayer(fileName, new AudioPlayerMock(true, false));

            Assert.True(player1.Play());
            Assert.False(player2.Play());
            Assert.False(player3.Play());
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
            return _audioPlayer.Play() && _audioPlayer.Dispose();
        }
    }
}
