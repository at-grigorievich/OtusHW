using UnityEngine;

namespace ATG.RealtimeChests.Reward
{
    [CreateAssetMenu(menuName = "Configs/New Reward Config", fileName = "New Reward Config")]
    public abstract class RewardConfig: ScriptableObject
    {
        public abstract void GetReward(EventBus eventBus);
    }
}