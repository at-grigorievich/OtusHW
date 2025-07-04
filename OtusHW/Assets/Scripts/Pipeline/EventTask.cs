using System;
using UnityEngine;

public abstract class EventTask
{
    private Action _onComplete;

    public void Run(Action onComplete)
    {
        _onComplete = onComplete;
        OnStart();
    }

    public void Complete()
    {
        OnComplete();
        _onComplete?.Invoke();
    }

    protected abstract void OnStart();
    protected virtual void OnComplete() {}
}