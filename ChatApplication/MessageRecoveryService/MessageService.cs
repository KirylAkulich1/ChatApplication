using System;

namespace ChatApplication.MessageService
{
    public class MessageService : IMessageService
    {
        private IMessageStorage _messageStorage;
        public MessageService(IMessageStorage messageStorage)
        {
            _messageStorage = messageStorage;
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