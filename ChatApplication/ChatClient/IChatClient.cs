using ChatApplication.ConnectionStratagies;

namespace ChatApplication
{
    public interface IChatClient
    {
        string Name { get; set; }
        string FriendName { get; set; }
        void ChangeStrategy(ICommunicaionStrategy newStrtegy);
        
    }
}