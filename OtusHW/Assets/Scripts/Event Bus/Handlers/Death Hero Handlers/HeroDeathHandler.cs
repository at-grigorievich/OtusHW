using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class HeroDeathHandler: EventLogicReceiver<CheckDeathEvent>
    {
        private readonly HeroTeamsService _heroTeamsService;

        public HeroDeathHandler(EventBus eventBus, HeroTeamsService heroTeamsService): base(eventBus)
        {
            _heroTeamsService = heroTeamsService;
        }
        
        public override void OnEvent(CheckDeathEvent evt)
        {
            TeamType deathTeam = (TeamType)evt.Entity.GetComponent<Team>().Value;
            _heroTeamsService[deathTeam].RemoveEntity(evt.Entity);
        }
    }
}