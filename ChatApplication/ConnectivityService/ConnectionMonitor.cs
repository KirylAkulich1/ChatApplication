using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApplication.ConnectivityService
{
    public class ConnectionMonitor
    {
        private Socket _connection;
        private ConnectionState _currentState=ConnectionState.Disconnected;
        public event EventHandler<ConnectionState> onConnectivityChanged;

        public bool IsConnected => _connection.Connected;
        public void MonitorConnection(Socket connection)
        {
            _connection = connection;
            new Thread(new ThreadStart(MonitorConnection)).Start();
        }
        private void MonitorConnection()
        {
            
            ConnectionState previousConnection = ConnectionState.Connected;
            while (true)
            {
                Task.Delay(100);
                if (_connection.Connected)
                {
                    _currentState = ConnectionState.Connected;
                }
                else
                {
                    _currentState = ConnectionState.Disconnected;
                }

                if (_currentState!=previousConnection)
                {
                    onConnectivityChanged?.Invoke(this, previousConnection);
                }

                previousConnection = _currentState;
            }
        }
    }
}