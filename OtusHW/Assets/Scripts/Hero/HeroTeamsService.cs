using System.Collections.Generic;

namespace DefaultNamespace.Hero
{
    public enum TeamType: byte
    {
        None = 0,
        Red = 1,
        Blue = 2
    }
    
    public class HeroTeamsService
    {
        private readonly IHeroFactory _heroFactory;
        
        public readonly Dictionary<TeamType, HeroTeam> Teams = new ();
        
        public TeamType MasterTeam { get; set; } = TeamType.None;

        public HeroTeam this[TeamType teamType]
        {
            get
            {
                if (Teams.ContainsKey(teamType) == false)
                {
                    Teams.Add(teamType, new HeroTeam(teamType, _heroFactory));
                }
                return Teams[teamType];
            }
        }
        
        public HeroTeamsService(IHeroFactory heroCreator)
        {
            _heroFactory = heroCreator;
        }
        
        public TeamType SwapMoveTeam()
        {
            if (MasterTeam== TeamType.None) MasterTeam = SelectFirstMoveTeam();
            MasterTeam = MasterTeam == TeamType.Blue ? TeamType.Red : TeamType.Blue;

            return MasterTeam;
        }
        
        private TeamType SelectFirstMoveTeam()
        {
            int rnd = UnityEngine.Random.Range(0, 11);
            
            if(rnd % 2 == 0) return TeamType.Blue;
            return TeamType.Red;
            
        }
    }
}