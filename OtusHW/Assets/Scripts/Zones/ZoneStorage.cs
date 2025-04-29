using System;
using ATG.Observable;
using ATG.Stats;
using DefaultNamespace;

namespace ATG.Zone
{
    public class ZoneStorage: IDisposable
    {
        private readonly IntStatValue _volume;
        public readonly ResourceType ResourceType;

        public IObservableVar<int> CurrentLevel => _volume.CurrentLevel;
        public IObservableVar<bool> IsMaxLevel => _volume.IsMaxLevel;
        
        public IObservableVar<int> CurrentValue => _volume.CurrentValue;
        public int CurrentVolume => _volume.CurrentVolume;

        public bool IsFull => _volume.CurrentValue.Value == _volume.CurrentVolume;
        public bool IsEmpty => _volume.CurrentValue.Value == 0;

        public ZoneStorage(ResourceType resourceType,int defaultValue, Stat<int> volumeStat)
        {
            ResourceType = resourceType;
            _volume = new IntStatValue(defaultValue, volumeStat);
        }
        
        public void AddAmount(int amount) => _volume.AddValue(amount);
        public void RemoveAmount(int amount) => _volume.RemoveValue(amount);

        public void Dispose() => _volume.Dispose();
    }
}
