using DefaultNamespace.Event_Bus.Events;
using UI;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class HeroDeathViewHandler: EventViewReceiver<CheckDeathEvent>
    {
        private readonly FightVisualPipeline _fightVisualPipeline;
        private readonly UIService _uiService;
        
        public HeroDeathViewHandler(FightVisualPipeline visualPipeline, UIService uiService,
            EventBus eventBus) : base(eventBus)
        {
            _fightVisualPipeline = visualPipeline;
            _uiService = uiService;
        }

        public override void OnEvent(CheckDeathEvent evt)
        {
            if (evt.Entity.TryGetComponent(out DeathSound deathSound) == true)
            {
                _fightVisualPipeline.AddTask(new PlaySoundTask(deathSound.Value, true));   
            }
            
            _fightVisualPipeline.AddTask(new DeathHeroVisualTask(evt.Entity, _uiService));
        }
    }
}