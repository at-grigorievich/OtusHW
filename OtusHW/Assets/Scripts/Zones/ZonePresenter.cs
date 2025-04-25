using System;
using ATG.Observable;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Zone
{
    public sealed class ZonePresenter: IStartable, IDisposable
    {
        private readonly ZoneStorage _storage;
        private readonly ZoneView _view;

        private ObserveDisposable _dis;
        
        public bool IsFull => _storage.IsFull;
        public bool IsEmpty => _storage.IsEmpty;

        public event Action OnAmountChanged;
        
        public ZonePresenter(ZoneStorage storage, ZoneView view)
        {
            _storage = storage;
            _view = view;
            
            _dis = _storage.CurrentAmount.Subscribe(OnCurrentAmountChanged);
        }

        private void OnCurrentAmountChanged(int obj)
        {
            OnAmountChanged?.Invoke();
            _view.UpdateAmounts(_storage.CurrentAmount.Value, _storage.MaxAmount.Value);
        }

        public void AddAmount(int amount)
        {
            _storage.AddAmount(amount);
        }

        public void RemoveAmount(int amount)
        {
            _storage.RemoveAmount(amount);
        }

        public void Start()
        {
            _view.UpdateAmounts(_storage.CurrentAmount.Value, _storage.MaxAmount.Value);
        }
        
        public void Dispose()
        {
            _storage?.Dispose();
            _dis?.Dispose();
        }
    }
}
