using System;
using ATG.RealtimeChests;
using ATG.Session;
using VContainer.Unity;

public sealed class ChestsEntryPoint: IStartable, IDisposable
{
    private readonly ChestPresenterPool _chestsPool;
    private readonly CycleGameStateSave _cycleGameStateSave;
    private readonly SessionsService _sessionsService;
    
    public ChestsEntryPoint(ChestPresenterPool chestsPool, EventBus eventBus, 
        SessionsService sessionsService)
    {
        _chestsPool = chestsPool;
        _cycleGameStateSave = new CycleGameStateSave(eventBus);
        _sessionsService = sessionsService;
    }
    
    public void Start()
    {
        _cycleGameStateSave.Initialize();
        _chestsPool.Init();
        _sessionsService.Start();
    }

    public void Dispose()
    {
        _cycleGameStateSave?.Dispose();
    }
}