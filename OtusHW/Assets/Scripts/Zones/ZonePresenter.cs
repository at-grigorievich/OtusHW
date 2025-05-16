using System;
using ATG.Observable;
using VContainer.Unity;

namespace ATG.Zone
{
    public sealed class ZonePresenter: IStartable, IDisposable
    {
        private readonly ZoneStorage _storage;
        private readonly ZoneView _view;

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

        public void Start()
        {
            _view.UpdateAmounts(_storage.CurrentValue.Value, _storage.CurrentVolume);
        }
        
        public void Dispose()
        {
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
    }
}
