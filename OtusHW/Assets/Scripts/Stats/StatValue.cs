using System;
using ATG.Observable;
using UnityEngine;

namespace ATG.Stats
{
    public abstract class StatValue<T>: IDisposable
    {
        private readonly Stat<T> _stat;
        public readonly ObservableVar<T> CurrentValue;
        
        public IObservableVar<int> CurrentLevel => _stat.CurrentLevel;
        public IObservableVar<bool> IsMaxLevel => _stat.IsMaxLevel;
        
        public T CurrentVolume => _stat.CurrentValue;

        public StatValue(T defaultValue, Stat<T> stat)
        {
            _stat = stat;
            CurrentValue = new ObservableVar<T>(defaultValue);
        }

        public abstract void AddValue(T addedValue);
        public abstract void RemoveValue(T removedValue);

        public void AddLevel()
        {
            _stat.ChangeLevel(CurrentLevel.Value + 1);
        }

        public void RemoveLevel()
        {
            _stat.ChangeLevel(CurrentLevel.Value - 1);
        }

        public void Dispose()
        {
            CurrentValue.Dispose();
        }
    }

    public sealed class IntStatValue : StatValue<int>
    {
        public IntStatValue(int defaultValue, Stat<int> stat) : base(defaultValue, stat) { }

        public override void AddValue(int addedValue)
        {
            CurrentValue.Value = Mathf.Clamp(CurrentValue.Value + addedValue, 0, CurrentVolume);
        }

        public override void RemoveValue(int removedValue)
        {
            CurrentValue.Value = Mathf.Clamp(CurrentValue.Value - removedValue, 0, CurrentVolume);
        }
    }
}