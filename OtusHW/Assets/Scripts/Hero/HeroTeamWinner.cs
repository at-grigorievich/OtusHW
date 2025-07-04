using System.Text;

namespace DefaultNamespace.Hero
{
    public sealed class HeroTeamWinner
    {
        private readonly HeroTeamsService _heroTeamsService;

        private TeamType _winner  = TeamType.None;
        
        public HeroTeamWinner(HeroTeamsService heroTeamsService)
        {
            _heroTeamsService = heroTeamsService;
        }

        public bool HasWinner()
        {
            if (_heroTeamsService[TeamType.Blue].IsAllDied() == true)
                _winner = TeamType.Red;
            else if (_heroTeamsService[TeamType.Red].IsAllDied() == true)
                _winner = TeamType.Blue;
            else 
                _winner = TeamType.None;

            return _winner != TeamType.None;
        }

        public string GetWinnerOutput()
        {
            if(_winner == TeamType.None) return string.Empty;

            string localization = _winner == TeamType.Blue ? "cиних" : "красных";
            
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append("Победила команда ");
            strBuilder.Append(localization);
            strBuilder.Append(". Состав: ");

            foreach (var hero in _heroTeamsService[_winner].Heroes)
            {
                if(hero.TryGetComponent(out Name name) == false) continue;
                strBuilder.Append(name.Value);
                strBuilder.Append("; ");
            }

            return strBuilder.ToString();
        }
    }
}