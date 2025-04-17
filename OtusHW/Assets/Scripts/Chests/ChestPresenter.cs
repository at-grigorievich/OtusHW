using System;
using UI;
using UnityEngine;
using VContainer.Unity;

namespace ATG.RealtimeChests
{
    public sealed class ChestPresenter: IStartable, IDisposable
    {
        private readonly ChestView _view;
        private readonly Chest _model;

        public ChestPresenter(ChestConfig chestConfig, ChestView view)
        {
            _model = chestConfig.Create();
            _view = view;
        }

        public void Start()
        {
            _view.ShowMetaData(_model.Meta);
            _model.ActivateTimer();
            
            _model.OnUnlockedTimerInfoChanged += _view.UpdateTimer;
            _view.OnChestOpenClicked += OnChestOpened;
        }

        public void Dispose()
        {
            _model.Dispose();
            
            _model.OnUnlockedTimerInfoChanged -= _view.UpdateTimer;
            _view.OnChestOpenClicked -= OnChestOpened;
        }
        
        private void OnChestOpened()
        {
            _view.SelectLockedState();
            _model.ActivateTimer();
        }
    }
}