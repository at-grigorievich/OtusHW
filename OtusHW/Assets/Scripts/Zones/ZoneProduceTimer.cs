using System;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Zone
{
    public class ZoneProduceTimer: ITickable
    {
        private float _produceDuration;
        private float _currentDuration;

        public bool IsCompleted { get; private set; } = true;
        
        public event Action OnCompleted;
        
        public ZoneProduceTimer(float produceDuration)
        {
            _produceDuration = produceDuration;
        }

        public void Tick()
        {
            if(IsCompleted == true) return;
            if (_currentDuration < _produceDuration)
            {
                _currentDuration += Time.deltaTime;
                return;
            }
            
            IsCompleted = true;
            OnCompleted?.Invoke();
        }

        public void Reset()
        {
            IsCompleted = false;
            _currentDuration = 0f;
        }
    }
}