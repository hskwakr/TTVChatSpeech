using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChatSpeech
{
    interface ISpeech
    {
        bool SpeechWord(string text);
    }
}
