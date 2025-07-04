using DefaultNamespace.Event_Bus.Events;
using VContainer;

namespace DefaultNamespace
{
    public class CreateHeroesTask: EventTask
    {
        private readonly EventBus _eventBus;
        private readonly int _unitsPerTeam;
        
        public CreateHeroesTask(IObjectResolver resolver, int unitsPerTeam)
        {
            _eventBus = resolver.Resolve<EventBus>();
            _unitsPerTeam = unitsPerTeam;
        }
        
        protected override void OnStart()
        {
            _eventBus.Raise(new CreateHeroesEvent
            {
                TeamType = Hero.TeamType.Blue,
                HeroCount = _unitsPerTeam
            });
            
            _eventBus.Raise(new CreateHeroesEvent
            {
                TeamType = Hero.TeamType.Red,
                HeroCount = _unitsPerTeam
            });
            
            Complete();
        }
    }
}