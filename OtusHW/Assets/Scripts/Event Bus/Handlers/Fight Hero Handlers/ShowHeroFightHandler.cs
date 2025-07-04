using System;
using DefaultNamespace.Event_Bus.Events;
using UI;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class ShowHeroFightHandler: EventViewReceiver<FightEvent>, IInitializable, IDisposable
    {
        private readonly FightVisualPipeline _fightVisualPipeline;
        private readonly UIService _uiService;
        
        public ShowHeroFightHandler(FightVisualPipeline fightVisualPipeline, UIService uiService,
            EventBus eventBus) : base(eventBus)
        {
            _fightVisualPipeline = fightVisualPipeline;
            _uiService = uiService;
        }

        public override void OnEvent(FightEvent evt)
        {
            var attackerHealth = evt.Attacker.GetComponent<Health>().Value;
            
            //if(attackerHealth <= 0) return;
            
            _fightVisualPipeline.AddTask(new AttackVisualTask(evt.Attacker, evt.Defender, _uiService));
            _fightVisualPipeline.AddTask(new ShowHeroViewTask(evt.Defender, _uiService));
        }

        public void Initialize() => Enter();
        public void Dispose() => Exit();
    }
}