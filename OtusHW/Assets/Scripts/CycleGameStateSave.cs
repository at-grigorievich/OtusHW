using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Event_Bus.Events;

public sealed class CycleGameStateSave: IDisposable
{
    private readonly EventBus _eventBus;
    private readonly CancellationTokenSource _cts;
    private readonly int _delayInSeconds;

    public CycleGameStateSave(EventBus eventBus)
    {
        _eventBus = eventBus;
        _cts = new CancellationTokenSource();
        _delayInSeconds = 10;
    }
    
    public void Initialize()
    {
        _eventBus.Raise(new LoadGameStateEvent());
        SaveCycle().Forget();
    }

    public void Dispose()
    {
        _eventBus.Raise(new SaveGameStateEvent());
        _cts?.Cancel();
        _cts?.Dispose();
    }
    
    private async UniTask SaveCycle()
    {
        while (true)
        {
            await UniTask.Delay(_delayInSeconds * 1000, cancellationToken: _cts.Token);
            _eventBus.Raise(new SaveGameStateEvent());
        }
    }
}