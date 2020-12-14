using TwitchLib.Client.Interfaces;

namespace TTVChatSpeech.TwitchChatManage
{
    public interface ITwitchChatClientInitializer
    {
        void Initialize(ITwitchClient client);
    }
}
