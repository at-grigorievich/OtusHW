using DefaultNamespace.Hero;
using UI;

namespace DefaultNamespace
{
    public class SetMarkerHeroViewTask: EventTask
    {
        private readonly UIService _uiService;
        private readonly TeamType _teamType;
        private readonly bool _isMarkered;
        private readonly int _index;

        public SetMarkerHeroViewTask(UIService uiService, TeamType teamType, int index, bool isMarkered)
        {
            _uiService = uiService;
            _teamType = teamType;
            _isMarkered = isMarkered;
            _index = index;
        }
        
        protected override void OnStart()
        {
            HeroListView needList = _teamType == TeamType.Blue ? _uiService.GetBluePlayer() : _uiService.GetRedPlayer();
            HeroView heroView = needList.GetView(_index);
            
            heroView.SetActive(_isMarkered);
            
            Complete();
        }
    }
}