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
        
        public DateTime StartedTime { get; private set; }
        public DateTime FinishedTime { get; private set; }

        public bool IsFinished { get; private set; }
    
        public event Action<CooldownTimerInfo> OnTimerInfoChanged;

        public CooldownTimer(TimeSpan cooldown)
        {
            _cooldown = cooldown;
        }

        public CooldownTimer(TimeSpan cooldown, DateTime startedTime, DateTime finishedTime): this(cooldown)
        {
            StartedTime = startedTime;
            FinishedTime = finishedTime;
            
            IsFinished = DateTime.Now >= finishedTime;
        }
        
        public void Start()
        {
            Dispose();

            if (IsFinished == true)
            {
                Debug.LogWarning("timer is already finished, need to call Reset()");
                return;
            }
            
            StartedTime = DateTime.Now;
            FinishedTime = StartedTime + _cooldown;   

            _cts = new CancellationTokenSource();
            UpdateTimerInfoAsync(_cts.Token).Forget();
        }

        public void Reset()
        {
            IsFinished = false;
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
                    
                    Dispose();
                    return;
                }
                
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
    }
}