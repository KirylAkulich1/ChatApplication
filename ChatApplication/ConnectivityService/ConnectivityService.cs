using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApplication.Handlers;

namespace ChatApplication.ConnectivityService
{
    public class ConnectivityService : IConnectivityService
    {
        private UdpClient _sender;
        private UdpClient _reciever;
        private string _remoteHost;
        private int _remotePort;

        public event EventHandler<byte[]> onMessageRecieved;
        public ConnectivityService(string remoteHost,int remotePort,int localPort)
        {
            _sender =  new UdpClient(remoteHost,remotePort);
            _reciever = new UdpClient(localPort);
            new Thread(new ThreadStart(Recieve)).Start();
        }
        public void Send(byte[] message)
        {
            ConnectionHandler handler = new ConnectionHandler();
            handler.Execute(() =>
            {
                _sender.Send(message,message.Length);
            });
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Recieve()
        {
            try
            {
                IPEndPoint endPoint = null;
                while (true)
                {
                    byte[] bytes = _reciever.Receive(ref endPoint);
                    onMessageRecieved?.Invoke(this,bytes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Disconnect()
        {
            _sender.Close();
        }

    }
}