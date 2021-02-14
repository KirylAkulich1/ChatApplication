using System;

namespace ChatApplication.Loggers
{
    public class ConsoleLogger: ILogger
    {
        public void LogException(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }

        public void Log(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
    }
}