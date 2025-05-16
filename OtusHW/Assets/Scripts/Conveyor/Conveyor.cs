using System;
using ATG.Stats;
using ATG.Zone;
using DefaultNamespace.Conveyor;
using VContainer.Unity;

public sealed class Conveyor : IInitializable, IDisposable, ILoadedZoneChecker
{
    private readonly ConveyorView _view;
    
    private readonly ZonePresenter _loadZone;
    private readonly ZonePresenter _unloadZone;

    //private readonly ZoneProduceTimer _produceTimer;
    private readonly ConveyorProcessor _conveyorProcessor;

    public bool IsLoadedZoneAvailable => _loadZone.IsFull == false;
    public event Action OnLoadedZoneAmountChanged
    {
        add => _loadZone.OnAmountChanged += value;
        remove => _loadZone.OnAmountChanged -= value;
    }
    
    public Conveyor(ConveyorView view, ZonePresenter loadZone, ZonePresenter unloadZone, int produceInSeconds,
        Stat<int> convertDelayStat)
    {
        _view = view;
        _loadZone = loadZone;
        _unloadZone = unloadZone;

        //_produceTimer = new ZoneProduceTimer(produceInSeconds);
        _conveyorProcessor = new ConveyorProcessor(loadZone, unloadZone, convertDelayStat);
        
        //_produceTimer.OnCompleted += OnProducedTime;
        
        _conveyorProcessor.OnConvertProgressChanged += OnConvertProgressChanged;
        _conveyorProcessor.OnConvertStarted += OnStartConverting;
        _conveyorProcessor.OnConvertFinished += OnStopConverting;
    }

    public void Initialize()
    {
        _loadZone.Initialize();
        _unloadZone.Initialize();
        
        //_produceTimer.Reset();   
        _conveyorProcessor.Initialize();
    }
    public void Dispose()
    {
        _loadZone.Dispose();
        _unloadZone.Dispose();
        
        //_produceTimer.Dispose();
        _conveyorProcessor.Dispose();
        
        //_produceTimer.OnCompleted -= OnProducedTime;
        
        _conveyorProcessor.OnConvertStarted -= OnStartConverting;
        _conveyorProcessor.OnConvertFinished -= OnStopConverting;
        _conveyorProcessor.OnConvertProgressChanged -= OnConvertProgressChanged;
    }

    public void LoadZoneLevelUp() => _loadZone.LevelUp();
    public void UnloadZoneLevelUp() => _unloadZone.LevelUp();
    public void ProcessorLevelUp() => _conveyorProcessor.LevelUp();

    public bool TryAddAmount(int amount)
    {
        if (_loadZone.IsFull == false)
        {
            _loadZone.AddAmount(amount);
            return true;
        }
        return false;
    }
    
    private void OnStartConverting()
    {
        _view.StartConvert();
    }
    private void OnStopConverting()
    {
        _view.StopConvert();
    }
    
    private void OnConvertProgressChanged(float rate)
    {
        _view.UpdateProgress(rate);
    }
    
    private void OnProducedTime()
    {
        if (_loadZone.IsFull == false)
        {
            _loadZone.AddAmount(1);
        }

        //_produceTimer.Reset();
    }
}