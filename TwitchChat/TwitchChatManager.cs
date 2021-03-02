using TwitchLib.Client;
using TwitchLib.Client.Interfaces;

namespace TTVChatSpeech.TwitchChat
{
    public class TwitchChatManager
    {
        private ITwitchClient _client;
        private ITwitchChatInitializer _initializer;
        private ITwitchChatEventsSubscriber _subscriber;
        private ITwitchChatConnection _clientConnection;

        public TwitchChatManager()
        {
            _client = new TwitchClient();
            _initializer = new TwitchChatInitializer();
            _clientConnection = new TwitchChatConnection();
            _subscriber = new TwitchChatEventsSubscriber();
        }

        public void ConnectToTwitch()
        {
            _initializer.Initialize(_client);
            _subscriber.Subscribe(_client);
            _clientConnection.Connect(_client);
        }
    }
}
