using System;
using Global.Services.MessageBrokers.Logs;
using UniRx;

namespace Global.Services.MessageBrokers.Runtime
{
    public class MessageBrokerProxy : IMessageBroker
    {
        public MessageBrokerProxy(MessageBrokerLogger logger)
        {
            _logger = logger;
            _messageBroker = new MessageBroker();

            Msg.Inject(this);
        }

        private readonly MessageBrokerLogger _logger;
        private readonly MessageBroker _messageBroker;

        public void Publish<T>(T message)
        {
            _logger.OnPublish<T>();

            _messageBroker.Publish(message);
        }

        public IObservable<T> Receive<T>()
        {
            _logger.OnListen<T>();

            return _messageBroker.Receive<T>();
        }
    }
}