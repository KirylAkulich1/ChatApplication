using System;
using System.Text;
using System.Threading;

namespace ChatApplication.Handlers
{
    public class ConnectionHandler
    {
        public void Execute(Action action)
        {
            while (!Retry(action))
            {
                Thread.Sleep(ChatConstants.RetryDelay);
            }
        }
        private bool Retry(Action action)
        {
            bool success = true;
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Container.Logger.LogException(e);
                success = false;
            }

            return success;
        }
    }
}