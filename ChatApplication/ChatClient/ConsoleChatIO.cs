using System;

namespace ChatApplication
{
    public class ConsoleChatIO : IChatIO
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
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