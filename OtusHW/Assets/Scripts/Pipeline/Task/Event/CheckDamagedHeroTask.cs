using System.Collections.Generic;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using VContainer;

namespace DefaultNamespace
{
    public class CheckDamagedHeroTask: EventTask
    {
        private readonly EventBus _eventBus;
        private readonly HeroTeamsService _heroTeamsService;

        public CheckDamagedHeroTask(IObjectResolver resolver)
        {
            _eventBus = resolver.Resolve<EventBus>();
            _heroTeamsService = resolver.Resolve<HeroTeamsService>();
        }
        
        protected override void OnStart()
        {
            CheckByTeam(TeamType.Blue);
            CheckByTeam(TeamType.Red);
            
            Complete();
        }

        private void CheckByTeam(TeamType teamType)
        {
            List<Entity> entities = _heroTeamsService[teamType].Heroes;

            foreach (var entity in entities)
            {
                if(entity.TryGetComponent(out DamagedMarker _) == false) continue;
                
                _eventBus.Raise(new HeroGetDamageEvent(entity));
            }
        }
    }
}