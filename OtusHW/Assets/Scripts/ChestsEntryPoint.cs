using System;
using ATG.RealtimeChests;
using VContainer.Unity;

public sealed class ChestsEntryPoint: IStartable, IDisposable
{
    private readonly ChestPresenterPool _chestsPool;
    private readonly CycleGameStateSave _cycleGameStateSave;
    
    public ChestsEntryPoint(ChestPresenterPool chestsPool, EventBus eventBus)
    {
        _chestsPool = chestsPool;
        _cycleGameStateSave = new CycleGameStateSave(eventBus);
    }
    
    public void Start()
    {
        _cycleGameStateSave.Initialize();
        _chestsPool.Init();
    }

    public void Dispose()
    {
        _cycleGameStateSave?.Dispose();
    }
}