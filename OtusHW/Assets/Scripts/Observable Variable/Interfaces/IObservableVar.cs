using System;

namespace ATG.Observable
{
    public interface IObservableVar<T>: IDisposable
    {
        T Value { get; set; }
        T DefaultValue { get; }

        ObserveDisposable Disposable { get; }
        ObserveDisposable Subscribe(Action<T> callback);
    }
}
