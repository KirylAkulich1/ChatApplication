using ChatApplication.ConnectionStratagies;

namespace ChatApplication
{
    public interface IChatClient
    {
        string Name { get; set; }
        void ChangeStrategy(IConnectionStrategy newStrtegy);
    }
}