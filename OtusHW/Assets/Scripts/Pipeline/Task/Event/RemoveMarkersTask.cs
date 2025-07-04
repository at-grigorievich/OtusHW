using System.Collections.Generic;
using DefaultNamespace.Hero;
using VContainer;

namespace DefaultNamespace
{
    public class RemoveMarkersTask: EventTask
    {
        private readonly HeroTeamsService _heroTeamsService;

        public RemoveMarkersTask(IObjectResolver resolver)
        {
            _heroTeamsService = resolver.Resolve<HeroTeamsService>();
        }
        
        protected override void OnStart()
        {
            RemoveMarkersByTeam(TeamType.Blue);
            RemoveMarkersByTeam(TeamType.Red);
            
            Complete();
        }

        private void RemoveMarkersByTeam(TeamType teamType)
        {
            List<Entity> entities = _heroTeamsService[teamType].Heroes;

            foreach (var entity in entities)
            {
                entity.RemoveComponent<AttackerMarker>();
                entity.RemoveComponent<DamagedMarker>();
            }
        }
    }
}