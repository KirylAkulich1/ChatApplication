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
        private IMessageService _messageService;
        public FirstConnectionStrategy(IChatClient client)
        {
            _client = client;
            _connectivityService = Container.ConnServ;
            _chatIo = Container.ChatIO;
            
        }

        public void Process(string content)
        {
            _client.Name = content;
            Message greetMessage = new Message { Type = MessageType.FirstMessage,Content = content};
            _connectivityService.Send(greetMessage.ToJson().ToByteArray());
            while (!_answerRecieved)
            {
                Thread.Sleep(ChatConstants.RetryDelay);
            }
            _connectivityService.onMessageRecieved -= OnMessageecieved;
        }

        public void StarategyEstablished()
        {
            _connectivityService.onMessageRecieved += OnMessageecieved;
        }

        public void  OnMessageecieved(object sender, byte[] bytes)
        {
            string message = Encoding.Unicode.GetString(bytes);
            _answerRecieved = true;
            Handler handler = new Handler();
            handler.ExecuteOnError((Exception e) =>
            {
                Message error = new Message {Type = MessageType.Error, Content = e.Message};
                _connectivityService.Send(error.ToJson().ToByteArray());
            }).Execute(()=>{
            Message recieved=message.ToMessage();
                switch (recieved.Type)
                {
                    case MessageType.Success:
                        _client.ChangeStrategy(Container.LiveConnectionSt);
                        _chatIo.Write(ChatConstants.ConnectionEstablished);
                        break;
                    case MessageType.RecoveryMessage:
                        foreach (var message  in _messageService.ResoreHistory())
                        {
                            _chatIo.Write(message);
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