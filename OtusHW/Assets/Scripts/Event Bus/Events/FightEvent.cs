namespace DefaultNamespace.Event_Bus.Events
{
    public struct FightEvent: IEvent
    {
        public readonly Entity Attacker;
        public readonly Entity Defender;

        public FightEvent(Entity attacker, Entity defender)
        {
            Attacker = attacker;
            Defender = defender;
        }
    }
}