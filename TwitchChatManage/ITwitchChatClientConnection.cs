using TwitchLib.Client.Interfaces;

namespace TTVChatSpeech.TwitchChatManage
{
    public interface ITwitchChatClientConnection
    {
        void Connect(ITwitchClient client);
    }
}
