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
        
        public CooldownTimer Timer { get; private set; }
        
        public event Action<CooldownTimerInfo> OnUnlockedTimerInfoChanged
        {
            add => Timer.OnTimerInfoChanged += value;
            remove => Timer.OnTimerInfoChanged -= value;
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