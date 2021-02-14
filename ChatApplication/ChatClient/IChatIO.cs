namespace ChatApplication
{
    public interface IChatIO
    {
        void Write(string message);
        string Read();
        string GetUserName();
        string GeetUser();
    }
}