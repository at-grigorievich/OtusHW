using System;
using ATG.DateTimers;
using ATG.RealtimeChests.Reward;

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
        public readonly RewardConfig Reward;
        
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

        public Chest(ChestType tag, ChestMetaData meta, RewardConfig reward, CooldownTimer timer)
        {
            Tag = tag;
            Timer = timer;
            Meta = meta;
            Reward = reward;
        }
        
        public void GetReward(EventBus eventBus) => Reward.GetReward(eventBus);
        
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