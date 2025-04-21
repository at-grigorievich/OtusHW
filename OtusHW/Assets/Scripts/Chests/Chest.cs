using System;
using ATG.DateTimers;

namespace ATG.RealtimeChests
{
    public enum ChestType : byte
    {
        Wooden = 1,
        Iron = 2,
        Gold = 3
    }
    
    public class Chest: IDisposable
    {
        public readonly ChestType Tag;
        public readonly ChestMetaData Meta;

        public bool ReadyToOpen => Timer.IsFinished == true;
        public CooldownTimer Timer { get; private set; }

        public event Action OnChestTimerStarted
        {
            add => Timer.OnTimerStarted += value;
            remove => Timer.OnTimerStarted -= value;
        }
        
        public event Action OnChestTimerEnded
        {
            add => Timer.OnTimerFinished += value;
            remove => Timer.OnTimerFinished -= value;
        }
        
        public event Action<CooldownTimerInfo> OnTimerChanged
        {
            add => Timer.OnTimerChanged += value;
            remove => Timer.OnTimerChanged -= value;
        }

        public Chest(ChestType tag, ChestMetaData meta, CooldownTimer timer)
        {
            Tag = tag;
            Timer = timer;
            Meta = meta;
        }

        public void ResetTimer()
        {
            Timer.Reset();
        }
        
        public void ActivateTimer()
        {
            Timer.Start();
        }
        
        public void Dispose()
        {
            Timer.Dispose();
        }
    }
}