using System.Collections.Generic;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using UI;
using UnityEngine;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class ShowMarkerHeroViewHandler: EventViewReceiver<NextMoveEvent>
    {
        private readonly HeroTeamsService _heroTeamsService;
        
        private readonly UIService _uiService;
        private readonly TurnVisualPipeline _turnVisualPipeline;
        
        public ShowMarkerHeroViewHandler(HeroTeamsService heroTeamsService, 
            TurnVisualPipeline turnVisualPipeline, UIService uiService, EventBus eventBus) : base(eventBus)
        {
            _heroTeamsService = heroTeamsService;
            _turnVisualPipeline = turnVisualPipeline;
            _uiService = uiService;
        }

        public override void OnEvent(NextMoveEvent evt)
        {
            UpdateMarkerByTeam(TeamType.Blue);
            UpdateMarkerByTeam(TeamType.Red);
        }
        
        private void UpdateMarkerByTeam(TeamType teamType)
        {
            List<Entity> entities = _heroTeamsService[teamType].Heroes;

            for (int i = 0; i < entities.Count; i++)
            {
                bool hasMarker = entities[i].TryGetComponent(out MasterMarker _);

                if (hasMarker == true)
                {
                    PlaySoundOnMarkered(entities[i]);
                }
                
                _turnVisualPipeline.AddTask(new SetMarkerHeroViewTask(_uiService, teamType, i, hasMarker));
            }
        }

        private void PlaySoundOnMarkered(Entity e)
        {
            if (e.TryGetComponent(out StartTurnSounds startTurnSounds) == false) return;
            _turnVisualPipeline.AddTask(new PlaySoundTask(GetRandomSounds(), false));

            AudioClip GetRandomSounds()
            {
                int rndIndex = UnityEngine.Random.Range(0, startTurnSounds.Values.Length);
                return startTurnSounds.Values[rndIndex];
            }
        }
    }
}