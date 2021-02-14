using System.Runtime.CompilerServices;
using ChatApplication.ConnectionStratagies;
using ChatApplication.ConnectivityService;
using ChatApplication.Loggers;
using ChatApplication.MessageService;

namespace ChatApplication
{
    //Simple IoC container).Ne beite
    public static class Container
    {
        public static ICommunicaionStrategy FistConnectionSt;
        public static ICommunicaionStrategy  LiveConnectionSt;
        public static IConnectivityService ConnServ;
        public static IChatClient ChatCl;
        public static IChatIO ChatIO;
        public static IMessageService MessageServ;
        public static IMessageStorage MessageStorage;
        public static ILogger Logger;

        static Container()
        {
            ChatIO = new ConsoleChatIO();
            Logger = new ConsoleLogger();
        }

        public static void ConfigureClient()
        {
            ChatCl = new ChatClient();
            FistConnectionSt = new FirstConnectionStrategy(ChatCl);
            LiveConnectionSt = new LiveCommunicationStrategy(ChatCl);
            ChatCl.ChangeStrategy(FistConnectionSt);
        }
        public static void ConfigureServices(string remoteHost,int remotePort,int localPort)
        {
            ConnectionBuilder connBuilder = new ConnectionBuilder();
            ConnServ = connBuilder.
                ConfigureLocal(localPort)
                .ConfigureRemote(remoteHost, remotePort)
                .Build();
            MessageStorage = new FileStorage($@"{remoteHost}{remotePort}.txt");
            MessageServ = new MessageService.MessageService(MessageStorage);
        }

        public static void ConfigureStorage(string remoteHost, int remotePort)
        {
            MessageStorage = new FileStorage(remoteHost);
            MessageServ = new MessageService.MessageService(MessageStorage);

        }
        
    }
}