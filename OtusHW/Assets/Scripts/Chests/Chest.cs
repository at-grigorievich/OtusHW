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
    
    public class Chest
    {
        public readonly ChestType Tag;
        public CooldownTimer Timer { get; private set; }
        
        public event Action<CooldownTimerInfo> OnUnlockedTimerInfoChanged
        {
            add => Timer.OnTimerInfoChanged += value;
            remove => Timer.OnTimerInfoChanged -= value;
        }

        public Chest(ChestType tag, CooldownTimer timer)
        {
            Tag = tag;
            Timer = timer;
        }
    }
}