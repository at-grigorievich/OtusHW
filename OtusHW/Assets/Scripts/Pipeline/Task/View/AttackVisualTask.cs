using Cysharp.Threading.Tasks;
using DefaultNamespace.Hero;
using UI;

namespace DefaultNamespace
{
    public class AttackVisualTask: EventTask
    {
        private readonly Entity _attackerEntity;
        private readonly Entity _defenderEntity;

        private readonly UIService _uiService;

        public AttackVisualTask(Entity attackerEntity, Entity defenderEntity, UIService uiService)
        {
            _attackerEntity = attackerEntity;
            _defenderEntity = defenderEntity;

            _uiService = uiService;
        }
        
        protected override void OnStart()
        {
            TeamType defenderTeam = (TeamType)_defenderEntity.GetComponent<Team>().Value;
            TeamType attackerTeam = (TeamType)_attackerEntity.GetComponent<Team>().Value;

            HeroView defenderView = _defenderEntity.GetHeroView(_uiService);
            HeroView attackerView = _attackerEntity.GetHeroView(_uiService);

            SetCanvasOrder(defenderTeam, false);
            SetCanvasOrder(attackerTeam, true);
            
            AnimateAttack(attackerView, defenderView).Forget();
        }

        protected override void OnComplete()
        {
            SetCanvasOrder(TeamType.Blue, false);
            SetCanvasOrder(TeamType.Red, false);
            
            base.OnComplete();
        }

        private async UniTask AnimateAttack(HeroView attackerView, HeroView defenderView)
        {
            await attackerView.AnimateAttack(defenderView);
            Complete();
        }

        private void SetCanvasOrder(TeamType teamType, bool isActive)
        {
            HeroListView heroListView = teamType == TeamType.Blue
                ? _uiService.GetBluePlayer()
                : _uiService.GetRedPlayer();
            
            heroListView.SetActive(isActive);
        }
    }
}