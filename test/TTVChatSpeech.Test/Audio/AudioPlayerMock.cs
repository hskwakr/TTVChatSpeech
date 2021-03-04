using TTVChatSpeech.Audio;

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
}
