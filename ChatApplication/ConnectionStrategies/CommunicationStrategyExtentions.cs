using System;
using System.Threading;

namespace ChatApplication.ConnectionStratagies
{
    public static  class CommunicationStrategyExtentions
    {
        public class Waiter
        {
            private int _retryIn = ChatConstants.RetryDelay;
            private Func<bool> _predicate;
            public Waiter Until(Func<bool> predicate)
            {
                _predicate = predicate;
                return this;
            }
            public void Repeat(Action repeatAction, int timestamp= ChatConstants.RetryDelay)
            {
                while (!_predicate.Invoke())
                {
                    repeatAction.Invoke();
                    Thread.Sleep(timestamp);
                }
            }

            public void RepeatIn(int timestamp)
            {
                _retryIn = timestamp;
                Start();
            }

            public void Start()
            {
                while (!_predicate.Invoke())
                {
                    Thread.Sleep(_retryIn);
                }
            }
        }
        public static Waiter Wait(this ICommunicaionStrategy self)
        {
            return new Waiter();
        }
    }
}