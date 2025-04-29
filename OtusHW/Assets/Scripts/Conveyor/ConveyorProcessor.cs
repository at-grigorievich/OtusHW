using System;
using System.Threading;
using ATG.Zone;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace DefaultNamespace.Conveyor
{
    public sealed class ConveyorProcessor: IStartable, IDisposable
    {
        private readonly ZonePresenter _loadZone;
        private readonly ZonePresenter _unloadZone;

        private float _convertDurationInSeconds;
        private float _currentDuration;
        
        
        private CancellationTokenSource _cts;

        public event Action<float> OnConvertProgressChanged;
        
        public event Action OnConvertStarted;
        public event Action OnConvertFinished;
        
        public ConveyorProcessor(ZonePresenter loadZone, ZonePresenter unloadZone, float convertDurationInSeconds)
        {
            _loadZone = loadZone;
            _unloadZone = unloadZone;
            _convertDurationInSeconds = convertDurationInSeconds;
        }
        
        public void Start()
        {
            Dispose();
            
            _loadZone.OnAmountChanged += OnAmountChanged;
            _unloadZone.OnAmountChanged += OnAmountChanged;
            
            OnAmountChanged();
        }
        
        public void Dispose()
        {
            _loadZone.OnAmountChanged -= OnAmountChanged;
            _unloadZone.OnAmountChanged -= OnAmountChanged;
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private void OnAmountChanged()
        {
            if(_cts != null) return;
            if(_loadZone.IsEmpty || _unloadZone.IsFull) return;
            
            _cts = new CancellationTokenSource();
            WaitToConvert(_cts.Token).Forget();
        }

        private void Convert()
        {
            _unloadZone.AddAmount(3);
        }
        
        private async UniTask WaitToConvert(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(.5f), cancellationToken: token);
            
            _loadZone.RemoveAmount(1);
            OnConvertStarted?.Invoke();
            
            _currentDuration = 0f;
            
            while (true)
            {
                if (token.IsCancellationRequested == true) return;

                if (_currentDuration < _convertDurationInSeconds)
                {
                    _currentDuration += Time.deltaTime;
                    OnConvertProgressChanged?.Invoke(_currentDuration / _convertDurationInSeconds);
                    await UniTask.Yield();
                }
                else break;
            }
            
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
            
            Convert();
            
            OnConvertFinished?.Invoke();
        }
    }
}