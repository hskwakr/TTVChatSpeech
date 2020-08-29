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

        public WavPlayer(string fileName)
        {
            _mediaFile = fileName;
        }

        public void Play()
        {
            using (var audioFile = new AudioFileReader(_mediaFile))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                
                outputDevice.Volume = 0.1F;
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
