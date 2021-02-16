using System;
using System.Text;
using ChatApplication.ConnectivityService;
using ChatApplication.Handlers;
using ChatApplication.Messages;
using ChatApplication.MessageService;

namespace ChatApplication.ConnectionStratagies
{
    public class LiveCommunicationStrategy : ICommunicaionStrategy
    {
        private IChatClient _client;
        private IChatIO _chatIo;
        private IMessageService _messageService;
        private IConnectivityService _connectivityService;
        public LiveCommunicationStrategy(IChatClient client)
        {
            _client = client;
            _messageService = Container.MessageServ;
            _connectivityService = Container.ConnServ;
            _chatIo = Container.ChatIO;
        }
        
        public void Process(string content)
        {
            _client.Name = content;
            _messageService.SaveMessage(ChatConstants.Me,content);
            Message greetMessage = new Message { Type = MessageType.Chat,Content = content};
            _connectivityService.Send(greetMessage.ToJson().ToByteArray());
            _chatIo.GetNextMessage();
        }

        public void StarategyEstablished()
        {
            _connectivityService.onMessageRecieved += OnMessageRecieved;
            _chatIo.GetNextMessage();
        }
        private void OnMessageRecieved(object sender, byte[] bytes)
        {
            string message = Encoding.Unicode.GetString(bytes);
            Handler handler = new Handler();
            handler.ExecuteOnError((Exception e) =>
            {
                _chatIo.Write(ChatConstants.ParseError);
                Message error = new Message {Type = MessageType.Error, Content = e.Message};
                _connectivityService.Send(error.ToJson().ToByteArray());
            }).Execute(()=>{
                Message recieved=message.ToMessage();
                switch (recieved.Type)
                {
                    case MessageType.Chat:
                        if (recieved.Content.MD5Hash() != recieved.ContentHash)
                        {
                            Message errorMessage = new Message {Type = MessageType.Error, Content = "Hash error"};
                            _connectivityService.Send(errorMessage.ToJson().ToByteArray());
                        }
                        _messageService.SaveMessage(_client.FriendName,recieved.Content);
                        _chatIo.Write($@"{_client.FriendName} : {recieved.Content}");
                        break;
                    
                    case  MessageType.Error:
                        _chatIo.Write(ChatConstants.RecieveError);
                        break;
                    
                    case MessageType.FirstMessage:
                        Message recoverMessage = new Message {Type = MessageType.RecoveryMessage, Content = _client.Name};
                        _connectivityService.Send(recoverMessage.ToJson().ToByteArray());
                        _chatIo.Write(ChatConstants.FriendReturned);
                        break;
                }
            });
        }
        
    }
}