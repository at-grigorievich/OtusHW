using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using VContainer;

namespace DefaultNamespace
{
    public class CounterFightTask: EventTask
    {
        private readonly EventBus _eventBus;
        private readonly HeroTeamsService _heroTeamsService;

        public CounterFightTask(IObjectResolver resolver)
        {
            _eventBus = resolver.Resolve<EventBus>();
            _heroTeamsService = resolver.Resolve<HeroTeamsService>();
        }
        
        protected override void OnStart()
        {
            Entity attackerEntity = GetAttackerMarkerEntity();
            Entity defenderEntity = GetDefenderMarkerEntity();

            if (attackerEntity == null || defenderEntity == null)
            {
                Complete();
                return;
            }
            
            _eventBus.Raise(new FightEvent(defenderEntity, attackerEntity));
            Complete();
        }

        private Entity GetAttackerMarkerEntity()
        {
            TeamType attackerTeam = _heroTeamsService.MasterTeam;

            List<Entity> entities = _heroTeamsService[attackerTeam].Heroes;
            
            return entities.FirstOrDefault(e => 
                e.TryGetComponent(out AttackerMarker _) == true &&
                e.TryGetComponent(out DeathMarker _) == false
                );
        }

        private Entity GetDefenderMarkerEntity()
        {
            TeamType defenderTeam = _heroTeamsService.MasterTeam == TeamType.Blue ? TeamType.Red : TeamType.Blue;
            
            List<Entity> entities = _heroTeamsService[defenderTeam].Heroes;
            
            return entities.FirstOrDefault(e => 
                e.TryGetComponent(out DamagedMarker _) == true &&
                e.TryGetComponent(out DeathMarker _) == false
                );
        }
    }
}