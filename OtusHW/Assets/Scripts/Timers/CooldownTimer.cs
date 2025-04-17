using System;
using System.Threading;
using Cysharp.Threading.Tasks;

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
        private DateTime _startedTime;
        private DateTime _finishedTime;

        private CancellationTokenSource _cts;
        
        public bool IsFinished => _finishedTime <= DateTime.Now;

        public event Action<CooldownTimerInfo> OnTimerInfoChanged;

        public CooldownTimer(TimeSpan cooldown)
        {
            _cooldown = cooldown;
        }

        public CooldownTimer(TimeSpan cooldown, DateTime startedTime, DateTime finishedTime): this(cooldown)
        {
            _startedTime = startedTime;
            _finishedTime = finishedTime;
        }
        
        public void Start()
        {
            Dispose();
            
            _startedTime = DateTime.Now;
            _finishedTime = _startedTime + _cooldown;
            
            _cts = new CancellationTokenSource();
            UpdateTimerInfoAsync(_cts.Token).Forget();
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
                var timeLeft = _finishedTime - DateTime.Now;

                CooldownTimerInfo timerInfo = new (timeLeft, timeLeft.TotalSeconds <= 0f);
                OnTimerInfoChanged?.Invoke(timerInfo);

                if (timerInfo.IsFinished)
                {
                    Dispose();
                    return;
                }
                
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
    }
}