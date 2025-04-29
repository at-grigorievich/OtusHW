using System;
using ATG.Zone;
using DefaultNamespace.Conveyor;
using VContainer.Unity;

public sealed class Conveyor : IStartable, IDisposable
{
    private readonly ConveyorView _view;
    
    private readonly ZonePresenter _loadZone;
    private readonly ZonePresenter _unloadZone;

    private readonly ZoneProduceTimer _produceTimer;
    private readonly ConveyorProcessor _conveyorProcessor;

    public Conveyor(ConveyorView view, ZonePresenter loadZone, ZonePresenter unloadZone, int produceInSeconds)
    {
        _view = view;
        _loadZone = loadZone;
        _unloadZone = unloadZone;

        _produceTimer = new ZoneProduceTimer(produceInSeconds);
        _conveyorProcessor = new ConveyorProcessor(loadZone, unloadZone, 4);
        
        _produceTimer.OnCompleted += OnProducedTime;
        
        _conveyorProcessor.OnConvertProgressChanged += OnConvertProgressChanged;
        _conveyorProcessor.OnConvertStarted += OnStartConverting;
        _conveyorProcessor.OnConvertFinished += OnStopConverting;
    }

    public void Start()
    {
        _loadZone.Start();
        _unloadZone.Start();
        
        _produceTimer.Reset();   
        _conveyorProcessor.Start();
    }
    
    public void Dispose()
    {
        _loadZone.Dispose();
        _unloadZone.Dispose();
        
        _produceTimer.Dispose();
        _conveyorProcessor.Dispose();
        
        _produceTimer.OnCompleted -= OnProducedTime;
        
        _conveyorProcessor.OnConvertStarted -= OnStartConverting;
        _conveyorProcessor.OnConvertFinished -= OnStopConverting;
        _conveyorProcessor.OnConvertProgressChanged -= OnConvertProgressChanged;
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

        _produceTimer.Reset();
    }
}