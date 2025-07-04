using System.Collections.Generic;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using UI;
using UnityEngine;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class LinkHeroesWithViewHandler: EventViewReceiver<CreateHeroesEvent>
    {
        private readonly UIService _uiService;
        private readonly HeroTeamsService _heroTeamsService;
        
        private readonly TurnVisualPipeline _turnVisualPipeline;
        
        public LinkHeroesWithViewHandler(UIService uiService, TurnVisualPipeline turnVisualPipeline,
            HeroTeamsService heroTeamsService,
            EventBus eventBus) : base(eventBus)
        {
            _uiService = uiService;
            _turnVisualPipeline = turnVisualPipeline;
            _heroTeamsService = heroTeamsService;
        }

        public override void OnEvent(CreateHeroesEvent evt)
        {
            List<Entity> teamHeroes = _heroTeamsService[evt.TeamType].Heroes;

            foreach (var newHero in teamHeroes)
            {
                _turnVisualPipeline.AddTask(new ShowHeroViewTask(newHero, _uiService));
            }
        }
    }
}