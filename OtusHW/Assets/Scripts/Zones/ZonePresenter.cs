using System;
using ATG.Inventory;
using ATG.Observable;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Zone
{
    public sealed class ZonePresenter: IStartable, IDisposable
    {
        private readonly ZoneStorage _storage;
        private readonly ZoneView _view;

        private readonly Action _onDisposed;
        
        private CompositeObserveDisposable _dis;
        
        public bool IsFull => _storage.IsFull;
        public bool IsEmpty => _storage.IsEmpty;
        
        public int AvailableVolume => _storage.AvailableVolume;
        
        public event Action OnAmountChanged;
        public event Action OnLevelChanged;
        
        public ZonePresenter(ZoneStorage storage, ZoneView view)
        {
            _storage = storage;
            _view = view;

            _dis = new CompositeObserveDisposable();
            
            _storage.CurrentValue.Subscribe(OnCurrentAmountChanged).AddTo(_dis);
            _storage.CurrentLevel.Subscribe(OnStorageLevelChanged).AddTo(_dis);

            if (_view is TriggerZoneView triggerZone)
            {
                triggerZone.OnTriggerEntered += OnTriggerZoneEntered;
                _onDisposed += () => triggerZone.OnTriggerEntered -= OnTriggerZoneEntered;
            }
        }
        public void Start()
        {
            _view.UpdateAmounts(_storage.CurrentValue.Value, _storage.CurrentVolume);
        }
        
        public void Dispose()
        {
            _onDisposed?.Invoke();
            _storage?.Dispose();
            _dis?.Dispose();
        }

        public void LevelUp()
        {
            _storage.AddLevel();
        }
        
        public void AddAmount(int amount)
        {
            _storage.AddAmount(amount);
        }

        public void RemoveAmount(int amount)
        {
            _storage.RemoveAmount(amount);
        }

        private void OnCurrentAmountChanged(int obj)
        {
            OnAmountChanged?.Invoke();
            _view.UpdateAmounts(_storage.CurrentValue.Value, _storage.CurrentVolume);
        }

        private void OnStorageLevelChanged(int lvl)
        {
            OnLevelChanged?.Invoke();
            _view.UpdateAmounts(_storage.CurrentValue.Value, _storage.CurrentVolume);
        }
        
        private void OnTriggerZoneEntered(Collider obj)
        {
             if(obj.TryGetComponent(out IInventoryOwner inventoryOwner) == false) return;
             int resInBagCount = inventoryOwner.GetResourceAmount(_storage.ResourceType);
             
             Debug.Log(resInBagCount);
             
             if(resInBagCount == 0) return;
             if(_storage.IsFull) return;

             int delta = 0;

             if (AvailableVolume > resInBagCount) delta = resInBagCount;
             else if (AvailableVolume < resInBagCount) delta = AvailableVolume;
             
             _storage.AddAmount(delta);
             inventoryOwner.RemoveElementsByType(_storage.ResourceType, delta);
        }
    }
}
