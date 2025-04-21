using Event_Bus.Events;

namespace Event_Bus.Handlers
{
    public sealed class GetResourcesRewardHandler: EventLogicReceiver<GetResourcesRewardEvent>
    {
        private readonly HeroInventory _heroInventory;
        
        public GetResourcesRewardHandler(HeroInventory heroInventory, EventBus eventBus) : base(eventBus)
        {
            _heroInventory = heroInventory;
        }

        public override void OnEvent(GetResourcesRewardEvent evt)
        {
            foreach (var evtReward in evt.Rewards)
            {
                _heroInventory.AddResource(evtReward.Type, evtReward.Amount);
            }
            
            _eventBus.Raise(new SaveGameStateEvent());
        }
    }
}