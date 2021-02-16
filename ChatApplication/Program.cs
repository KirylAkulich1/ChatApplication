using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ChatApplication.ConnectivityService;

namespace ChatApplication
{
    class Program
    {
        /// <summary>
        /// args[0]=remote address
        /// args[1]= remote port
        /// args[2]=local port
        /// </summary>
        /// <param name="args"></param>
        enum  ConnectionState
        {
            CONNECTED,
            DISCONNECTED,
            CLOSED
        }
        static async Task  DoFirstConnectAsync(string remoteAddress,int remotePort,string userName)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            socket.Connect(localIP);
            try
            {
                while (!socket.Connected)
                {
                    await socket.ConnectAsync(localIP);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        static void Main(string[] args)
        {
            ConfigureChat("127.0.0.1", 4444, 5555);
            Container.ChatIO.Write(ChatConstants.GreetingMessage);
            Container.ChatIO.GetNextMessage();
        }

        private static  void ConfigureChat(string hostname,int remotePort,int localPort)
        {
            Container.ConfigureServices(hostname,remotePort,localPort);
            Container.ConfigureClient();
            Container.ConfigureStorage(hostname,remotePort);
            
        }
    }
}