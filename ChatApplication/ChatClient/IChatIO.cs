using System;

namespace ChatApplication
{
    public interface IChatIO
    {
        event EventHandler<String> OnUserInput;
        void Write(string message);

        void GetNextMessage();
        string GetUserName();
        
    }
}