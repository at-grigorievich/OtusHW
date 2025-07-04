using DefaultNamespace.Event_Bus.Handlers;
using DefaultNamespace.Hero;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneScope: LifetimeScope
    {
        [SerializeField] private LinearHeroFactoryCreator linearHeroCreator;
        [SerializeField] private UIService uiService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            linearHeroCreator.Create(builder);
            builder.RegisterInstance(uiService).AsSelf();
            
            builder.Register<EventBus>(Lifetime.Singleton);
            
            builder.Register<HeroTeamsService>(Lifetime.Singleton);
            builder.Register<HeroTeamWinner>(Lifetime.Singleton);
            
            RegisterRootPipeline(builder);
            RegisterTurnPipeline(builder);
            RegisterTurnVisualPipeline(builder);
        }

        private void RegisterRootPipeline(IContainerBuilder builder)
        {
            builder.Register<RootPipeline>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            
            builder.Register<CreateHeroesHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterTurnPipeline(IContainerBuilder builder)
        {
            builder.Register<TurnPipeline>(Lifetime.Singleton).AsSelf();
            
            builder.Register<SelectMarkerHeroHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<HeroFightHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<HeroDeathHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterTurnVisualPipeline(IContainerBuilder builder)
        {
            builder.Register<TurnVisualPipeline>(Lifetime.Singleton).AsSelf();
            builder.Register<FightVisualPipeline>(Lifetime.Singleton).AsSelf();
            
            builder.Register<LinkHeroesWithViewHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ShowMarkerHeroViewHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<HeroLowHealthViewHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<HeroDeathViewHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ShowHeroFightHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}