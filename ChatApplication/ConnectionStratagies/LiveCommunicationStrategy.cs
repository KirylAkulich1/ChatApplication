using System;
using ChatApplication.ConnectivityService;

namespace ChatApplication.ConnectionStratagies
{
    public class LiveCommunicationStrategy : IConnectionStrategy
    {
        private IChatClient _client;
        private IChatIO _chatIO;
        private ConnectivityService.ConnectivityService _service = ConnectionBuilder.GetInstance();
        public LiveCommunicationStrategy(IChatClient client)
        {
            _client = client;
        }
        
        private void  RecieveMessages()
        {
            try
            {
                while (true)
                {
                    
                }
            }
            catch (Exception ex)
            {
                _chatIO.Write(ChatConstants.ErrorMessage);
            }
            finally
            {
                _chatIO.Write(ChatConstants.ByeMessage);
            }
         
        }
        public void Connect()
        {
            
        }

        public void Send(string message)
        {
            
        }
    }
}