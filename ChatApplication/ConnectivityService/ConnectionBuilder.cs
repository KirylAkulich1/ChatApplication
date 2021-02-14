using System.Dynamic;

namespace ChatApplication.ConnectivityService
{
    public class ConnectionBuilder
    {
        private static ConnectivityService _object;

        private int  _remotePort;
        private string _remoteHost;
        private int _localPort;

        public ConnectionBuilder ConfigureRemote(string remoteHost, int remotePort)
        {
            _remoteHost = remoteHost;
            _remotePort = remotePort;
            return this;
        }

        public ConnectionBuilder ConfigureLocal(int localPort)
        {
            _localPort = localPort;
            return this;
        }

        public ConnectivityService Build()
        {
            _object = new ConnectivityService(_remoteHost,_remotePort,_localPort);
            return _object;
        }

        public static ConnectivityService GetInstance() => _object;
    }
}