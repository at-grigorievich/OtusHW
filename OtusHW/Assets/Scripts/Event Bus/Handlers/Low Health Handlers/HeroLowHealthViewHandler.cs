using DefaultNamespace.Event_Bus.Events;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class HeroLowHealthViewHandler : EventViewReceiver<HeroGetDamageEvent>
    {
        private const float CRITICAL_PERCENT = 0.2f;
        
        private readonly FightVisualPipeline _fightVisualPipeline;
        
        public HeroLowHealthViewHandler(FightVisualPipeline visualPipeline, EventBus eventBus) : base(eventBus)
        {
            _fightVisualPipeline = visualPipeline;
        }

        public override void OnEvent(HeroGetDamageEvent evt)
        {
            if(evt.Entity.TryGetComponent(out LowHealthSound lowHealthSound) == false) return;
            
            var health = evt.Entity.GetComponent<Health>();
            
            
            float criticalHealth = CRITICAL_PERCENT * health.Base;
            
            if (health.Value <= criticalHealth && health.Value > 0f)
            {
                _fightVisualPipeline.AddTask(new PlaySoundTask(lowHealthSound.Value, true));
            }
        }
    }
}