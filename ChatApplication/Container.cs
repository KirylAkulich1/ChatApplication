using System.Runtime.CompilerServices;
using ChatApplication.ConnectionStratagies;
using ChatApplication.ConnectivityService;
using ChatApplication.MessageService;

namespace ChatApplication
{
    //Simple IoC container).Ne beite
    public static class Container
    {
        public static IConnectionStrategy FistConnectionSt;
        public static IConnectionStrategy LiveConnectionSt;
        public static IConnectionStrategy RecoveryconnectionSt;
        public static ConnectionMonitor ConnMonitor;
        public static IConnectivityService ConnServ;
        public static IChatClient ChatCl;
        public static IChatIO ChatIO;
        public static IMessageService MessageServ;
        public static IMessageStorage MessageStorage;

        static Container()
        {
            ConnMonitor = new ConnectionMonitor();
            ChatIO = new ConsoleChatIO();
        }

        public static void ConfigureClient(string name)
        {
            ChatCl = new ChatClient(name,ChatIO);
            FistConnectionSt = new FirstConnectionStrategy(ChatCl);
            LiveConnectionSt = new LiveCommunicationStrategy(ChatCl);
            RecoveryconnectionSt = new RecoveryConnectionStrategy(ChatCl);
        }
        public static void ConfigureServices(string remoteHost,int remotePort,int localPort)
        {
            ConnectionBuilder connBuilder = new ConnectionBuilder();
            ConnServ = connBuilder.
                ConfigureLocal(localPort)
                .ConfigureRemote(remoteHost, remotePort)
                .Build();
            
        }

        public static void ConfigureStorage(string remoteHost, int remotePort)
        {
            MessageStorage = new FileStorage(remoteHost);
            MessageServ = new MessageService.MessageService(MessageStorage);

        }
        
    }
}