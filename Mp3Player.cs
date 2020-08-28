using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace TwitchChatSpeech
{
    class Mp3Player : IDisposable
    {
        public bool Repeat { get; set; }

        public Mp3Player(string fileName)
        {
            const string FORMAT = @"open ""{0}"" type mpegvideo alias MediaFile";
            string command = String.Format(FORMAT, fileName);
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public void Play()
        {
            string command = "play MediaFile";
            if (Repeat)
            {
                command += " REPEAT";
            }

            int result = mciSendString(command, null, 0, IntPtr.Zero);
            if (result != 0)
            {
                StringBuilder msg = new StringBuilder();
                if (mciGetErrorString(result, msg, 128) != 0)
                {
                    Console.WriteLine("◆◆◆◆◆◆◆◆◆◆◆");
                    Console.WriteLine(msg);
                }
            }
        }

        public void Stop()
        {
            //string command = "stop MediaFile";
            string command = "pause MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        [DllImport("winmm.dll")]
        private static extern int mciSendString(
            string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        [DllImport("winmm.dll")]
        private static extern int mciGetErrorString(
            int fdwError, StringBuilder errMsg, int buflen);

        public void Dispose()
        {
            string command = "close MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);
        }
    }
}
