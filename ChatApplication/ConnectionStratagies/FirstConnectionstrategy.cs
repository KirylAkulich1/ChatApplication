using System.Text;
using ChatApplication.ConnectivityService;

namespace ChatApplication.ConnectionStratagies
{
    public class FirstConnectionStrategy : IConnectionStrategy
    {
        private IConnectivityService _connectivityService;
        private ConnectionMonitor _connectionMonitor;
        private IChatClient _client;
        public FirstConnectionStrategy(IChatClient client)
        {
            _client = client;
            _connectivityService = Container.ConnServ;
            _connectionMonitor = Container.ConnMonitor;
            _connectionMonitor.onConnectivityChanged += ConnectivityChanged;
        }

        public void Connect()
        {
            
        }

        public void Send(string message)
        {
            _connectivityService.Send( Encoding.UTF8.GetBytes(message));
        }

        public void ConnectivityChanged(object sender, ConnectionState currentState)
        {
            switch (currentState)
            {
                case ConnectionState.Disconnected:
                    _client.ChangeStrategy(Container.RecoveryconnectionSt);
                    break;
                case ConnectionState.Connected:
                    Send(_client.Name);
                    break;
            }
        }
    }
}