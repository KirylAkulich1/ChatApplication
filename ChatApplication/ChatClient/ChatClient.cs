using ChatApplication.ConnectionStratagies;

namespace ChatApplication
{
    public class ChatClient : IChatClient
    {
        private IConnectionStrategy _connectionStrategy;
        private IChatIO _chatIO;
        public string Name { get; set; }
        
        public ChatClient(string name,IChatIO chatIO)
        {
            _connectionStrategy = Container.FistConnectionSt;
            _chatIO = chatIO;
            Name = name;
        }
        public void ChangeStrategy(IConnectionStrategy newStrtegy)
        {
            _connectionStrategy = newStrtegy;
        }
        
    }
}