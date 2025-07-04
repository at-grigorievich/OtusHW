using DefaultNamespace.Hero;
using UI;
using UnityEngine;
using VContainer;

namespace DefaultNamespace
{
    public class DeathHeroVisualTask: EventTask
    {
        private readonly Entity _entity;
        private readonly UIService _uiService;

        public DeathHeroVisualTask(Entity deathEntity, UIService uiService)
        {
            _entity = deathEntity;
            _uiService = uiService;
        }
        
        protected override void OnStart()
        {
            var heroView = _entity.GetHeroView(_uiService);

            //GameObject.Destroy(heroView.gameObject);
            Debug.Log("Death");
            heroView.gameObject.SetActive(false);
            
            Complete();
        }
    }
}