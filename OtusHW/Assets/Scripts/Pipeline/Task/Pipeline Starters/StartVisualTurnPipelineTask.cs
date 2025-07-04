using UnityEngine;
using VContainer;

namespace DefaultNamespace
{
    public class StartVisualTurnPipelineTask: EventTask
    {
        private readonly TurnVisualPipeline _visualPipeline;

        public StartVisualTurnPipelineTask(IObjectResolver resolver)
        {
            _visualPipeline = resolver.Resolve<TurnVisualPipeline>();
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