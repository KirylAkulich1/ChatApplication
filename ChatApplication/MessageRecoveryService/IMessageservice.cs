namespace ChatApplication.MessageService
{
    public interface IMessageService
    {
        void SaveMessage(string author,string content);
        string[] ResoreHistory();
    }
}