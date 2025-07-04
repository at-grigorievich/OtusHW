using DefaultNamespace.Hero;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class RootPipeline: Pipeline, IInitializable, IPostStartable
    {
        private const int UNITS_PER_TEAM = 4;
            
        private readonly IObjectResolver _objectResolver;
        private readonly HeroTeamWinner _heroTeamWinner;
        
        public RootPipeline(IObjectResolver resolver)
        {
            _objectResolver = resolver;
            _heroTeamWinner = _objectResolver.Resolve<HeroTeamWinner>();
        }
        
        public void Initialize()
        {
            OnCompleted += OnCompletedCallback;
            
            AddTask(new CreateHeroesTask(_objectResolver, UNITS_PER_TEAM));
            AddTask(new StartTurnPipelineTask(_objectResolver));
        }

        public void PostStart()
        {
            Reset();
            Run();
        }
        
        private void OnCompletedCallback()
        {
            OnCompleted += OnCompletedCallback;
            
            Debug.Log(_heroTeamWinner.GetWinnerOutput());
        }
    }
}