namespace ChatApplication.ConnectionStratagies
{
    public interface ICommunicaionStrategy
    {
        void Process(string content);
        void StarategyEstablished();
    }
}