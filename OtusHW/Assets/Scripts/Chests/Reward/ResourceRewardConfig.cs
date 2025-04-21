using System.Linq;
using ATG.Resources;
using Event_Bus.Events;
using UnityEngine;

namespace ATG.RealtimeChests.Reward
{
    [CreateAssetMenu(menuName = "Configs/New Resource Reward Config", fileName = "New Resource Reward Config")]
    public sealed class ResourceRewardConfig: RewardConfig
    {
        [SerializeField] private ResourceGenerator[] resources;
        
        public override void GetReward(EventBus eventBus)
        {
            eventBus.Raise(new GetResourcesRewardEvent(resources
                            .Select(r => r.Create()))
            );
        }
    }
}