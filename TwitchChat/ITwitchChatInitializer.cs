using TwitchLib.Client.Interfaces;

namespace TTVChatSpeech.TwitchChat
{
    public interface ITwitchChatInitializer
    {
        void Initialize(ITwitchClient client);
    }
}
