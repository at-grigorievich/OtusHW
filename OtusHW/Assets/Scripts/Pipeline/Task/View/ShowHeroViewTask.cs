using System;
using DefaultNamespace.Hero;
using UI;
using UnityEngine;
using VContainer;

namespace DefaultNamespace
{
    public class ShowHeroViewTask: EventTask
    {
        private readonly UIService _uiService;
        private readonly Entity _heroEntity;
        
        public ShowHeroViewTask(Entity heroEntity, UIService uiService)
        {
            _heroEntity = heroEntity;
            _uiService = uiService;
        }
        
        protected override void OnStart()
        {
            if (_heroEntity.TryGetComponent(out Team teamType) == false)
            {
                Complete();
                return;
            }

            HeroView view = _heroEntity.GetHeroView(_uiService);
            
            //ShowName();
            //ShowDescription();
            
            ShowIcon(view);
            ShowCurrentStats(view);
            
            Complete();
        }

        private void ShowName(HeroView view)
        {
            string name = _heroEntity.TryGetComponent(out Name res) ? res.Value : "no_name";
            view.name = name;
        }

        private void ShowDescription(HeroView view)
        {
            string desc = _heroEntity.TryGetComponent(out Description res) ? res.Value : "no_description";
            //...
        }

        private void ShowIcon(HeroView view)
        {
            Sprite icon = _heroEntity.TryGetComponent(out Icon res) ? res.Value : null;
            view.SetIcon(icon);
        }

        private void ShowCurrentStats(HeroView view)
        {
            int health = _heroEntity.TryGetComponent(out Health hp) ? hp.Value : 0;
            int damage = _heroEntity.TryGetComponent(out Damage dmg) ? dmg.Value : 0;

            string statOutput = $"{damage}/{health}";
            
            view.SetStats(statOutput);
        }
    }
}