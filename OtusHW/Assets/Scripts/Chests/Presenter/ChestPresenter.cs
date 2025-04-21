using System;
using Event_Bus.Events;
using UI;
using VContainer.Unity;

namespace ATG.RealtimeChests
{
    public sealed class ChestPresenter: IStartable, IDisposable
    {
        private readonly ChestView _view;
        private readonly Chest _model;
        
        private readonly EventBus _eventBus;

        public ChestPresenter(Chest chest, ChestView view, EventBus eventBus)
        {
            _model = chest;
            _view = view;
            
            _eventBus = eventBus;
        }

        public void Start()
        {
            _view.ShowMetaData(_model.Meta);
            _view.SetupInitialState(_model.ReadyToOpen);
            _model.ActivateTimer();
            
            _model.OnTimerChanged += _view.UpdateTimer;
            _model.OnChestTimerStarted += OnChestStateChanged;
            _model.OnChestTimerEnded += OnChestStateChanged;
            
            _view.OnChestOpenClicked += OnChestOpened;
        }

        public void Dispose()
        {
            _model.Dispose();
            
            _model.OnTimerChanged -= _view.UpdateTimer;
            _view.OnChestOpenClicked -= OnChestOpened;
            _model.OnChestTimerStarted -= OnChestStateChanged;
            _model.OnChestTimerEnded -= OnChestStateChanged;
        }
        
        private void OnChestOpened()
        {
            _view.SelectLockedState();
            
            _model.GetReward(_eventBus);
            
            _model.ResetTimer();
            _model.ActivateTimer();
        }

        private void OnChestStateChanged()
        {
            _eventBus.Raise(new SaveGameStateEvent());
        }
    }
}