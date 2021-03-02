using TwitchLib.Client.Interfaces;

namespace TTVChatSpeech.TwitchChat
{
    public interface ITwitchChatConnection
    {
        void Connect(ITwitchClient client);
    }
}
