using System.Collections.Generic;
using ATG.Resources;

namespace Event_Bus.Events
{
    public readonly struct GetResourcesRewardEvent: IEvent
    {
        public readonly IEnumerable<Resource> Rewards;

        public GetResourcesRewardEvent(IEnumerable<Resource> rewards)
        {
            Rewards = rewards;
        }
    }
}