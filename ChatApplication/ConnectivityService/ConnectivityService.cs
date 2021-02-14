using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApplication.ConnectivityService
{
    public class ConnectivityService : IConnectivityService
    {
        private UdpClient _sender;
        private UdpClient _reciever;
        private string _remoteHost;
        private int _remotePort;
        private ConnectionMonitor _connectionMonitor;
        
        public event EventHandler<byte[]> onMessageRecieved;
        public ConnectivityService(string remoteHost,int remotePort,int localPort)
        {
            _sender = new UdpClient(remoteHost, remotePort);
            _reciever = new UdpClient(localPort);
            _remoteHost = remoteHost;
            _remotePort = remotePort;
            _connectionMonitor = Container.ConnMonitor;
            new Thread(new ThreadStart(Recieve)).Start();
        }
        public void Send(byte[] message)
        {
            int bytesToSend = message.Length;
            while (bytesToSend < ChatConstants.MaxMessageLength)
            {
                var messageChunk = message[(message.Length - bytesToSend )..(message.Length - bytesToSend + ChatConstants.MaxMessageLength) ];
                if (_connectionMonitor.IsConnected)
                {
                    _sender.Send(messageChunk, ChatConstants.MaxMessageLength);
                }
                bytesToSend -= ChatConstants.MaxMessageLength;
            }

            if (_connectionMonitor.IsConnected)
            {
                _sender.Send(message[(message.Length - bytesToSend)..], bytesToSend);
            }
        }

        public void Recieve()
        {
            try
            {
                IPEndPoint endPoint = null;
                while (true)
                {
                    byte[] bytes = _reciever.Receive(ref endPoint);
                    onMessageRecieved.Invoke(this,bytes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void Connect()
        {
            _sender.Connect(_remoteHost,_remotePort);
            _connectionMonitor.MonitorConnection(_sender.Client);
        }

        public void TryReconnect()
        {
            _sender.Connect(_remoteHost,_remotePort); 
        }

        public void Disconnect()
        {
            _sender.Close();
        }

    }
}