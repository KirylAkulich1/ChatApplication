using System;
using System.Collections;

namespace ChatApplication.ConnectivityService
{
    public interface IConnectivityService
    {
        event EventHandler<byte[]> onMessageRecieved;
        void Send(byte[] message);
        void Connect();
    }
}