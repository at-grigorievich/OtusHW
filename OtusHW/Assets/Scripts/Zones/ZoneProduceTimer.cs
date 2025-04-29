using System;
using System.Threading;
using ATG.Observable;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Zone
{
    public class ZoneProduceTimer: IDisposable
    {
        private float _produceDuration;

        private CancellationTokenSource _cts;
        
        public event Action OnCompleted;
        
        public ZoneProduceTimer(float produceDuration)
        {
            _produceDuration = produceDuration;
        }
        
        public void Reset()
        {
            Dispose();
            _cts = new CancellationTokenSource();
            StartTimer().Forget();
        }

        private async UniTask StartTimer()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_produceDuration), cancellationToken: _cts.Token);
            OnCompleted?.Invoke();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}