namespace DefaultNamespace.Event_Bus.Events
{
    public struct HeroGetDamageEvent: IEvent
    {
        public Entity Entity;

        public HeroGetDamageEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}