using Event_Bus.Events;
using SaveSystem;

namespace Event_Bus.Handlers
{
    public sealed class LoadGameStateHandler: EventLogicReceiver<LoadGameStateEvent>
    {
        private readonly ISaveService _saveService;
        
        public LoadGameStateHandler(ISaveService saveService, EventBus eventBus) 
            : base(eventBus)
        {
            _saveService = saveService;
        }

        public override void OnEvent(LoadGameStateEvent evt)
        {
            _saveService.Load();
        }
    }
}