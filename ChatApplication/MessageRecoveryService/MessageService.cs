namespace ChatApplication.MessageService
{
    public class MessageService : IMessageService
    {
        private IMessageStorage _messageStorage;
        public MessageService(IMessageStorage messStorage)
        {
            _messageStorage = _messageStorage;
        }
        public void SaveMessage()
        {
            
        }

        public void SaveMessage(string author, string content)
        {
            _messageStorage.Save($@"{author} : {content}");
        }

        public string[] ResoreHistory()
        {
            return _messageStorage.Read();
        }
    }
}