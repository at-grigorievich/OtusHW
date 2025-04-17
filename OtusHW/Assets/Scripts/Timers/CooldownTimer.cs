using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ATG.DateTimers
{
    public readonly struct CooldownTimerInfo
    {
        public readonly TimeSpan TimeLeft;
        public readonly bool IsFinished;

        public CooldownTimerInfo(TimeSpan timeLeft, bool isFinished)
        {
            TimeLeft = timeLeft;
            IsFinished = isFinished;
        }
    }
    
    public class CooldownTimer: IDisposable
    {
        private readonly TimeSpan _cooldown;
        
        private CancellationTokenSource _cts;
        private bool _canReset;
        
        public DateTime StartedTime { get; private set; }
        public DateTime FinishedTime { get; private set; }

        public bool IsFinished { get; private set; }
        
        public event Action OnTimerStarted;
        public event Action OnTimerFinished;
        public event Action<CooldownTimerInfo> OnTimerInfoChanged;

        public CooldownTimer(TimeSpan cooldown)
        {
            _cooldown = cooldown;
            _canReset = true;
        }

        public CooldownTimer(TimeSpan cooldown, DateTime startedTime, DateTime finishedTime): this(cooldown)
        {
            StartedTime = startedTime;
            FinishedTime = finishedTime;
            
            IsFinished = DateTime.Now >= finishedTime;
            _canReset = false;
        }
        
        public void Start()
        {
            Dispose();

            if (IsFinished == true)
            {
                Debug.LogWarning("timer is already finished, need to call Reset()");
                return;
            }

            if (_canReset == true)
            {
                StartedTime = DateTime.Now;
                FinishedTime = StartedTime + _cooldown;  
            }

            _cts = new CancellationTokenSource();
            UpdateTimerInfoAsync(_cts.Token).Forget();
            
            OnTimerStarted?.Invoke();
        }

        public void Reset()
        {
            IsFinished = false;
            _canReset = true;
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask UpdateTimerInfoAsync(CancellationToken token)
        {
            while (true)
            {
                var timeLeft = FinishedTime - DateTime.Now;

                CooldownTimerInfo timerInfo = new (timeLeft, timeLeft.TotalSeconds <= 0f);
                OnTimerInfoChanged?.Invoke(timerInfo);

                if (timerInfo.IsFinished)
                {
                    IsFinished = true;
                    
                    OnTimerFinished?.Invoke();
                    
                    Dispose();
                    return;
                }
                
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
    }
}