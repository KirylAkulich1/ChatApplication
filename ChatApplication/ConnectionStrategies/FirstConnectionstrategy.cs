using System;
using System.Text;
using System.Threading;
using ChatApplication.ConnectivityService;
using ChatApplication.Handlers;
using ChatApplication.Messages;
using ChatApplication.MessageService;

namespace ChatApplication.ConnectionStratagies
{
    public class FirstConnectionStrategy : ICommunicaionStrategy
    {
        private IConnectivityService _connectivityService;
        private IChatClient _client;
        private IChatIO _chatIo;
        private bool _answerRecieved;
        private bool _otherClientNameRecieved;
        private IMessageService _messageService;
        public FirstConnectionStrategy(IChatClient client)
        {
            _client = client;
            _connectivityService = Container.ConnServ;
            _chatIo = Container.ChatIO;
            _messageService = Container.MessageServ;
        }

        public void Process(string content)
        {
            _client.Name = content;
            Message greetMessage = new Message { Type = MessageType.FirstMessage,Content = content};
            
            this.Wait().
                Until(()=>_answerRecieved).
                Repeat(()=>_connectivityService.Send(greetMessage.ToJson().ToByteArray()));
           
            this.Wait().
                Until(()=>_otherClientNameRecieved).
                Start();
            
            _connectivityService.onMessageRecieved -= OnMessageecieved;
            _chatIo.Write("Connection Established.Start dialog");
            _client.ChangeStrategy(Container.LiveConnectionSt);
        }

        public void StarategyEstablished()
        {
            _connectivityService.onMessageRecieved += OnMessageecieved;
        }

        public void  OnMessageecieved(object sender, byte[] bytes)
        {
            string message = Encoding.Unicode.GetString(bytes);
            Handler handler = new Handler();
            handler.ExecuteOnError((Exception e) =>
            {
                Message error = new Message {Type = MessageType.Error, Content = e.Message};
                _connectivityService.Send(error.ToJson().ToByteArray());
            }).Execute(()=>{
            Message recieved=message.ToMessage();
                switch (recieved.Type)
                {
                    case MessageType.FirstMessage:
                        _client.FriendName = recieved.Content;
                        Message successMessage = new Message {Type = MessageType.Success, Content = "Success"};
                        _connectivityService.Send(successMessage.ToJson().ToByteArray());
                        _otherClientNameRecieved = true;
                        break;
                    
                    case MessageType.Success:
                        _answerRecieved = true;
                        break;
                    
                    case MessageType.RecoveryMessage:
                        _answerRecieved = true;
                        _otherClientNameRecieved = true;
                        _client.FriendName = recieved.Content;
                        _chatIo.Write(ChatConstants.OtherUserStillOnLine);
                        foreach (var m  in _messageService.ResoreHistory())
                        {
                            _chatIo.Write(m);
                        }
                        break;
                    
                    case  MessageType.Error:
                        Message greetMessage=new Message { Type = MessageType.FirstMessage,Content =_client.Name};
                        _connectivityService.Send(greetMessage.ToJson().ToByteArray());
                        break;
                }
            });

        }
    }
}