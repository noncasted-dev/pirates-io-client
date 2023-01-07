using System;
using UniRx;

namespace Global.Services.MessageBrokers.Runtime
{
    public static class Msg
    {
        private static IMessageBroker _messageBroker;

        public static void Inject(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public static void Publish<T>(T message)
        {
            _messageBroker.Publish(message);
        }

        public static IDisposable Listen<T>(Action<T> listener)
        {
            return _messageBroker.Receive<T>().Subscribe(listener);
        }
    }
}