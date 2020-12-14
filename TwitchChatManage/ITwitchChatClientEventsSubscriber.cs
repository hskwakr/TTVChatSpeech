using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;

namespace TTVChatSpeech.TwitchChatManage
{
    public interface ITwitchChatClientEventsSubscriber
    {
        void Subscribe(ITwitchClient client);
        void OnLog(object sender, OnLogArgs e);
        void OnJoinedChannel(object sender, OnLogArgs e);
        void OnMessageReceived(object sender, OnLogArgs e);
        void OnWhisperReceived(object sender, OnLogArgs e);
        void OnNewSubscriber(object sender, OnLogArgs e);
        void OnConnected(object sender, OnLogArgs e);
    }
}
