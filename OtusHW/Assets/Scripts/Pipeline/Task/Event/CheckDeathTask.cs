using System.Collections.Generic;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using VContainer;

namespace DefaultNamespace
{
    public class CheckDeathTask: EventTask
    {
        private readonly EventBus _eventBus;
        private readonly HeroTeamsService _heroTeamsService;
        
        public CheckDeathTask(IObjectResolver resolver)
        {
            _eventBus = resolver.Resolve<EventBus>();
            _heroTeamsService = resolver.Resolve<HeroTeamsService>();
        }

        protected override void OnStart()
        {
           CheckTeam(TeamType.Blue);
           CheckTeam(TeamType.Red);
           
           Complete();
        }

        private void CheckTeam(TeamType teamType)
        {
            List<Entity> entities = _heroTeamsService[teamType].Heroes;

            foreach (var entity in entities)
            {
                bool noDieMarker = entity.TryGetComponent(out DeathMarker _) == false;
                bool noHealth = entity.GetComponent<Health>().Value <= 0f;

                if (noDieMarker && noHealth)
                {
                    _eventBus.Raise(new CheckDeathEvent(entity));
                }
            }
        }
    }
}