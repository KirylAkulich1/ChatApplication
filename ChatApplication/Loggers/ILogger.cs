using System;

namespace ChatApplication.Loggers
{
    public interface ILogger
    {
        void LogException(Exception e);
        void Log(string logMessage);

    }
}