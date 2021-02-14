using System;
using ChatApplication.ConnectivityService;

namespace ChatApplication.Handlers
{
    public class Handler
    {
        //private IConnectivityService _connectivityService = Container.ConnServ;
        private Action<Exception> _onError;

        public Handler ExecuteOnError(Action<Exception> action)
        {
            _onError = action;
            return this;
        }
        public void Execute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                _onError.Invoke(e);
                Container.Logger.LogException(e);
                throw;
            }
        }  
    }
}