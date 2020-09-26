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
        private object _audioFileLock = new object();

        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;

        public WavPlayer(string fileName)
        {
            _mediaFile = fileName;
        }

        public void Play()
        {
            if (_outputDevice == null)
            {
                _outputDevice = new WaveOutEvent();
                _outputDevice.PlaybackStopped += OnPlaybackStopped;
            }

            if (_audioFile == null)
            {
                lock (_audioFileLock)
                {
                    _audioFile = new AudioFileReader(_mediaFile);
                    _outputDevice.Init(_audioFile);
                }
            }

            _outputDevice.Volume = 0.05F;
            _outputDevice.Play();

            while (_outputDevice?.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
        }

        public void Dispose()
        {
            if (_outputDevice?.PlaybackState == PlaybackState.Playing)
            {
                _outputDevice.Stop();
            }

            _outputDevice?.Dispose();
            _outputDevice = null;

            lock (_audioFileLock)
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
