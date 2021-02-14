namespace ChatApplication.ConnectionStratagies
{
    public class RecoveryConnectionStrategy 
    {
        private IChatClient _chatClient;
        public RecoveryConnectionStrategy(IChatClient client)
        {
            _chatClient = client;
        }
        public void Connect()
        {
            throw new System.NotImplementedException();
        }

        public void Send(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}