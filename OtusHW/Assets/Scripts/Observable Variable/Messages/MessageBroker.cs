using System;
using System.Collections.Generic;

namespace ATG.Observable
{
    public sealed class MessageBroker: IMessageBroker
    {
        private readonly Dictionary<Type, ObservableVar<IMessage>> _dic = new Dictionary<Type, ObservableVar<IMessage>>();

        public void Send<T>(T message) where T: IMessage
        {
            Type type = message.GetType();

            if (_dic.ContainsKey(type))
            {
                var observe = _dic[type];
                observe.Value = message;
            }
            else
            {
                var observe = new ObservableVar<IMessage>();
                _dic.Add(type, observe);
                observe.Value = message;
            }
        }

        public ObserveDisposable Subscribe<T>(Action<IMessage> receiver ) where T : IMessage
        {
            Type type = typeof(T);

            if (_dic.ContainsKey(type))
            {
                var observe = _dic[type];
                return observe.Subscribe(receiver);
            }
            else
            {
                var observe = new ObservableVar<IMessage>();
                _dic.Add(type, observe);

                return observe.Subscribe(receiver);
            }
        }
    }
}
