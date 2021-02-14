using System;

namespace ChatApplication
{
    public class ConsoleChatIO : IChatIO
    {
        public event EventHandler<string> OnUserInput;

        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void GetNextMessage()
        {
            string message = Console.ReadLine();
            OnUserInput?.Invoke(this,message);
        }

        public string Read()
        {
            return Console.ReadLine();
        }

        public string GetUserName()
        {
            Console.WriteLine(ChatConstants.GreetingMessage);
            return Console.ReadLine();
        }

        public string GeetUser()
        {
            throw new NotImplementedException();
        }
        
    }
}