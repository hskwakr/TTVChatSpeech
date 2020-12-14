using TwitchLib.Client;
using TwitchLib.Client.Interfaces;

namespace TTVChatSpeech.TwitchChatManage
{
    public class TwitchChatManager
    {
        private ITwitchClient _client;
        private ITwitchChatClientInitializer _initializer;
        private ITwitchChatClientEventsSubscriber _subscriber;
        private ITwitchChatClientConnection _clientConnection;

        public TwitchChatManager()
        {
            _client = new TwitchClient();
            _initializer = new TwitchChatClientInitializer();
            _clientConnection = new TwitchChatClientConnection();
            _subscriber = new TwitchChatClientEventsSubscriber();
        }

        public void ConnectToTwitch()
        {
            _initializer.Initialize(_client);
            _subscriber.Subscribe(_client);
            _clientConnection.Connect(_client);
        }
    }
}
