using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchChatSpeech
{
    interface ISpeech
    {
        bool SpeechWord(string text);
    }
}
