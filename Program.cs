using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchChatSpeech
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Form1());

            //ChatTool.CheckInstalledVoices();
            TwitchBot bot = new TwitchBot();
            Console.ReadLine();
        }
    }
}
