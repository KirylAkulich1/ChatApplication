namespace ChatApplication.ConnectionStratagies
{
    public interface IConnectionStrategy
    {
        void Connect();
        void Send(string message);
    }
}