using Event_Bus.Events;
using SaveSystem;

namespace Event_Bus.Handlers
{
    public sealed class SaveGameStateHandler: EventLogicReceiver<SaveGameStateEvent>
    {
        private readonly ISaveService _saveService;
        
        public SaveGameStateHandler(ISaveService saveService, EventBus eventBus) 
            : base(eventBus)
        {
            _saveService = saveService;
        }

        public override void OnEvent(SaveGameStateEvent evt)
        {
            _saveService.Save();
        }
    }
}