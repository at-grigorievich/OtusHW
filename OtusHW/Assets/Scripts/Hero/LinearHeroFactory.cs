using System;
using UnityEngine;
using VContainer;

namespace DefaultNamespace.Hero
{
    public interface IHeroFactory
    {
        Entity GetNextHero();
    }

    [Serializable]
    public sealed class LinearHeroFactoryCreator
    {
        [SerializeField] private HeroConfig[] configs;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<IHeroFactory, LinearHeroFactory>(Lifetime.Singleton)
                .WithParameter(configs);
        }
    }
    
    public sealed class LinearHeroFactory: IHeroFactory
    {
        private readonly HeroConfig[] _availableHeroes;

        private int _selectIndex = -1;
        
        public LinearHeroFactory(HeroConfig[] availableHeroes)
        {
            _availableHeroes = availableHeroes;
        }

        public Entity GetNextHero()
        {
            _selectIndex++;
            if (_selectIndex >= _availableHeroes.Length) _selectIndex = 0;

            return _availableHeroes[_selectIndex].CreateHeroEntity();
        }
    }
}