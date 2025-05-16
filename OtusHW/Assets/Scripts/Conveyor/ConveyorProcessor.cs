using System;
using System.Threading;
using ATG.Stats;
using ATG.Zone;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace DefaultNamespace.Conveyor
{
    public sealed class ConveyorProcessor: IInitializable, IDisposable
    {
        private readonly ZonePresenter _loadZone;
        private readonly ZonePresenter _unloadZone;

        private readonly Stat<int> _convertDelays;
        
        private float _currentDuration;
        
        private CancellationTokenSource _cts;

        public event Action<float> OnConvertProgressChanged;
        
        public event Action OnConvertStarted;
        public event Action OnConvertFinished;
        
        public ConveyorProcessor(ZonePresenter loadZone, ZonePresenter unloadZone, Stat<int> convertDelays)
        {
            _loadZone = loadZone;
            _unloadZone = unloadZone;
            _convertDelays = convertDelays;
        }
        
        public void Initialize()
        {
            Dispose();
            
            _loadZone.OnAmountChanged += StartConvert;
            _unloadZone.OnAmountChanged += StartConvert;
            _unloadZone.OnLevelChanged += StartConvert;
            
            StartConvert();
        }
        
        public void Dispose()
        {
            _loadZone.OnAmountChanged -= StartConvert;
            _unloadZone.OnAmountChanged -= StartConvert;
            _unloadZone.OnLevelChanged -= StartConvert;
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public void LevelUp()
        {
            _convertDelays.ChangeLevel(_convertDelays.CurrentLevel.Value + 1);
        }
        
        private void StartConvert()
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

                if (_currentDuration < _convertDelays.CurrentValue)
                {
                    _currentDuration += Time.deltaTime;
                    OnConvertProgressChanged?.Invoke(_currentDuration /_convertDelays.CurrentValue);
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