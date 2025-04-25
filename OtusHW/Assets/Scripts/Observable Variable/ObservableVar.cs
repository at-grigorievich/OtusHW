using System;
using System.Collections.Generic;
using System.Linq;

namespace ATG.Observable
{
    public class ObservableVar<T>: IObservableVar<T>
    {
        private readonly T _defaultValue;
        private readonly bool _ignoreEqualValue = false;

        private List<Action<T>> _observers = new List<Action<T>>();

        private T _value;

        public T Value
        {
            get 
            { 
                return _value; 
            }
            set
            {
                if (_ignoreEqualValue)
                {
                    if (_value?.Equals(value) ?? ReferenceEquals(_value, value)) return;
                }

                _value = value;

                InvokeObservers(_value);
            }
        }
        public T DefaultValue { get { return _defaultValue; } }


        public ObservableVar()
        {
            _defaultValue = default(T);
            Value = _defaultValue;
        }

        public ObservableVar(T defaultValue, bool ignoreEqualValue = true)
        {
            _defaultValue = defaultValue;
            Value = _defaultValue;

            _ignoreEqualValue = ignoreEqualValue;
        }

        public ObserveDisposable Disposable { get; private set; }

        public ObserveDisposable Subscribe(Action<T> callback)
        {
            _observers.Add(callback);
            Disposable =  new ObserveDisposable(() => 
            {
                for (int i = 0; i < _observers.Count; i++)
                {
                    if(_observers[i] == callback)
                        _observers[i] = null;
                }
            });

            return Disposable;
        }

        private void InvokeObservers(T value)
        {
            int nullCount = 0;

            foreach (var o in _observers.ToArray())
            {
                if (o == null)
                {
                    nullCount++;
                    continue;
                }
                o.Invoke(value);
            }

            if (nullCount > 10)
            {
                _observers = new List<Action<T>>(_observers.Where(i => i != null));
            }
        }

        public void Dispose()
        {
            Disposable?.Dispose();
            _observers.Clear();
        }
    }
}
