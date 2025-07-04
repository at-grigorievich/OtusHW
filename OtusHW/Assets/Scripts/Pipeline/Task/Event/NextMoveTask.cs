using DefaultNamespace.Event_Bus.Events;
using VContainer;

namespace DefaultNamespace
{
    public class NextMoveTask: EventTask
    {
        private readonly EventBus _eventBus;

        public NextMoveTask(IObjectResolver resolver)
        {
            _eventBus = resolver.Resolve<EventBus>();
        }
        
        protected override void OnStart()
        {
            _eventBus.Raise(new NextMoveEvent());
            Complete();
        }
    }
}