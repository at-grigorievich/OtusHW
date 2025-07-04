using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public sealed class CreateHeroesHandler: EventLogicReceiver<CreateHeroesEvent>
    {
        private readonly HeroTeamsService _heroTeamsService;
        
        public CreateHeroesHandler(HeroTeamsService heroTeamsService, EventBus eventBus): base(eventBus)
        {
            _heroTeamsService = heroTeamsService;
        }
        
        public override void OnEvent(CreateHeroesEvent evt)
        {
            for (int i = 0; i < evt.HeroCount; i++)
            {
                Entity newHero = _heroTeamsService[evt.TeamType].AddNewHero();
                
                newHero.AddComponent(new ViewIndex { Value = i });
            }
        }
    }
}