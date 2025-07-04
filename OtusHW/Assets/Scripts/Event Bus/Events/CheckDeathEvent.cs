namespace DefaultNamespace.Event_Bus.Events
{
    public struct CheckDeathEvent: IEvent
    {
        public readonly Entity Entity;

        public CheckDeathEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}