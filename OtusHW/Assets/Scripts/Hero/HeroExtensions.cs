using UI;

namespace DefaultNamespace.Hero
{
    public static class HeroExtensions
    {
        public static HeroView GetHeroView(this Entity entity, UIService uiService)
        {
            HeroListView heroListView = (TeamType)entity.GetComponent<Team>().Value== TeamType.Blue
                ? uiService.GetBluePlayer()
                : uiService.GetRedPlayer();

            return heroListView.GetView(entity.GetComponent<ViewIndex>().Value);
        }
    }
}