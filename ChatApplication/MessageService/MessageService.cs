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

        public void ResoreHistory()
        {
          
        }
    }
}