using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NAudio.Wave;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchChatSpeech
{
    class WavPlayer
    {
        private string _mediaFile = "";
        private object _lock = new object();

        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;

        public WavPlayer(string fileName)
        {
            _mediaFile = fileName;
            _outputDevice = new WaveOutEvent();
            _outputDevice.PlaybackStopped += OnPlaybackStopped;
            
            lock (_lock)
            {
                _audioFile = new AudioFileReader(_mediaFile);
                _outputDevice.Init(_audioFile);
            }

            _outputDevice.Volume = 0.05F;
        }

        public void Play()
        {
            _outputDevice?.Play();
        }

        public void Dispose()
        {
            _outputDevice?.Dispose();
            _outputDevice = null;

            lock (_lock)
            {
                _audioFile?.Dispose();
                _audioFile = null;
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _outputDevice?.Stop();
            Dispose();
        }
    }
}
