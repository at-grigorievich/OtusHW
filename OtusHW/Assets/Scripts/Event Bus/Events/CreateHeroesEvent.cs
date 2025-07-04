namespace DefaultNamespace.Event_Bus.Events
{
    public struct CreateHeroesEvent: IEvent
    {
        public Hero.TeamType TeamType;
        public int HeroCount;
    }
}