namespace TTVChatSpeech.Audio
{
    public class AudioPlayer
    {
        private IAudioPlayer _audioPlayer;

        public AudioPlayer(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public bool Play()
        {
            return _audioPlayer.Play() && _audioPlayer.Dispose();
        }
    }
}
