using NAudio.Wave;
using System.Threading;

namespace TwitchChatSpeech
{
    class WavPlayer
    {
        private string mediaFilePath = "";
        private object mediaFileLock = new object();

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        public WavPlayer(string fileName)
        {
            mediaFilePath = fileName;
        }

        public void Play()
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }

            if (audioFile == null)
            {
                lock (mediaFileLock)
                {
                    audioFile = new AudioFileReader(mediaFilePath);
                    outputDevice.Init(audioFile);
                }
            }

            outputDevice.Volume = 0.05F;
            outputDevice.Play();

            while (outputDevice?.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
        }

        public void Dispose()
        {
            if (outputDevice?.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Stop();
            }

            outputDevice?.Dispose();
            outputDevice = null;

            lock (mediaFileLock)
            {
                audioFile?.Dispose();
                audioFile = null;
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            outputDevice?.Stop();
            Dispose();
        }
    }
}
