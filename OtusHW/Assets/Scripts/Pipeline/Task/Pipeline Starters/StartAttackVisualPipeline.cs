using UnityEngine;
using VContainer;

namespace DefaultNamespace
{
    public class StartAttackVisualPipeline: EventTask
    {
        private readonly FightVisualPipeline _visualPipeline;
        
        public StartAttackVisualPipeline(IObjectResolver resolver)
        {
            _visualPipeline = resolver.Resolve<FightVisualPipeline>();
        }
        
        protected override void OnStart()
        {
            Debug.Log("Start turn visual pipeline task");
            _visualPipeline.OnCompleted += OnVisualPipelineCompleted;
            _visualPipeline.Reset();
            _visualPipeline.Run();
        }

        private void OnVisualPipelineCompleted()
        {
            _visualPipeline.OnCompleted -= OnVisualPipelineCompleted;
            _visualPipeline.ClearTasks();
            Complete();
        }
    }
}