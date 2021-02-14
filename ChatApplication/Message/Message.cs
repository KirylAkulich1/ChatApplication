namespace ChatApplication.Messages
{
    public class Message
    {
        private string _contentHash;
        public MessageType Type { get; set; }
        public string Content { get; set; }
        
        public string ContentHash
        {
            get => Content.MD5Hash();
            set { _contentHash = value; }
        }
        
    }
}