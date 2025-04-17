using System;
using System.Threading;
using ATG.RealtimeChests;
using Cysharp.Threading.Tasks;
using SaveSystem;
using VContainer.Unity;

public sealed class ChestsEntryPoint: IInitializable, IDisposable
{
    private readonly ChestPresenterPool _chestsPool;
    
    private readonly ISaveService _saveService;
    private readonly CancellationTokenSource _cts;
    private readonly int _delayInSeconds;
    
    public ChestsEntryPoint(ISaveService saveService, ChestPresenterPool chestsPool)
    {
        _saveService = saveService;
        _cts = new CancellationTokenSource();
        _delayInSeconds = 10;
        
        _chestsPool = chestsPool;
    }
    
    public void Initialize()
    {
        _saveService.Load();
        SaveCycle().Forget();
        
        foreach (var chest in _chestsPool.Chests)
        {
            chest.OnChestTimerStarted += _saveService.Save;
            chest.OnChestTimerEnded += _saveService.Save;
        }
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
        
        foreach (var chest in _chestsPool.Chests)
        {
            chest.OnChestTimerStarted -= _saveService.Save;
            chest.OnChestTimerEnded -= _saveService.Save;
        }
        
        _saveService.Save();
    }

    private async UniTask SaveCycle()
    {
        while (true)
        {
            await UniTask.Delay(_delayInSeconds * 1000);
            _saveService.Save();
        }
    }
}