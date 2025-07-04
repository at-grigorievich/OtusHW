using System.Collections.Generic;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class SelectMarkerHeroHandler: EventLogicReceiver<NextMoveEvent>
    {
        private readonly HeroTeamsService _heroTeamsService;

        public SelectMarkerHeroHandler(EventBus eventBus, HeroTeamsService heroTeamsService): base(eventBus)
        {
            _heroTeamsService = heroTeamsService;
        }
        
        public override void OnEvent(NextMoveEvent evt)
        {
            TeamType currentMasterTeam = _heroTeamsService.SwapMoveTeam();
            
            _heroTeamsService[currentMasterTeam].MoveActiveHeroNext();
            
            RemoveMarkerFromWaitTeam();
            AddMarkerToMasterHero();
        }
        
        private void RemoveMarkerFromWaitTeam()
        {
            TeamType waitTeam = _heroTeamsService.MasterTeam == TeamType.Blue ? TeamType.Red : TeamType.Blue;
            IReadOnlyList<Entity> waiters = _heroTeamsService[waitTeam].Heroes;

            foreach (var e in waiters)
            {
                e.RemoveComponent<MasterMarker>();
            }
        }

        private void AddMarkerToMasterHero()
        {
            var attackers = _heroTeamsService[_heroTeamsService.MasterTeam].Heroes;
            int requiredIndex = _heroTeamsService[_heroTeamsService.MasterTeam].ActiveHeroIndex;

            for (int i = 0; i < attackers.Count; i++)
            {
                if (i == requiredIndex)
                {
                    attackers[i].AddComponent(new MasterMarker());
                }
                else
                {
                    attackers[i].RemoveComponent<MasterMarker>();
                }
            }
            
        }
    }
}