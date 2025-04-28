using System;
using ATG.Observable;
using ATG.Stats;
using DefaultNamespace;
using UnityEngine;

namespace ATG.Zone
{
    public class ZoneStorage: IDisposable
    {
        private readonly Stat<int> _volume;
        public readonly ResourceType ResourceType;
        
        public ObservableVar<int> 
        
        public bool IsFull => CurrentAmount.Value == MaxAmount.Value;
        public bool IsEmpty => CurrentAmount.Value == 0;

        public ZoneStorage(ResourceType resourceType, int currentAmount, int maxAmount)
        {
            ResourceType = resourceType;
            CurrentAmount = new ObservableVar<int>(currentAmount);
            MaxAmount = new ObservableVar<int>(maxAmount);
        }
        
        public void AddAmount(int amount)
        {
            int nextAmount = Mathf.Clamp(CurrentAmount.Value + amount, 0, MaxAmount.Value);
            CurrentAmount.Value = nextAmount;
        }

        public void RemoveAmount(int amount)
        {
            int nextAmount = Mathf.Clamp(CurrentAmount.Value - amount, 0, MaxAmount.Value);
            CurrentAmount.Value = nextAmount;
        }

        public void Dispose()
        {
            CurrentAmount.Dispose();
            MaxAmount.Dispose();
        }
    }
}
