using ChatApplication.ConnectionStratagies;

namespace ChatApplication
{
    public class ChatClient : IChatClient
    {
        private ICommunicaionStrategy _connectionStrategy;
        private IChatIO _chatIO;
        public string Name { get; set; }
        public string FriendName { get; set; }

        public ChatClient()
        {
            _chatIO = Container.ChatIO;
            _chatIO.OnUserInput += OnUserInput;
        }
        public void ChangeStrategy(ICommunicaionStrategy newStrtegy)
        {
            _connectionStrategy = newStrtegy;
            _connectionStrategy.StarategyEstablished();
        }

        private void OnUserInput(object sender, string message)
        {
            _connectionStrategy.Process(message);
        }
        
    }
}