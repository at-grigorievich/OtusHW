using ATG.Observable;

namespace ATG.Stats
{
    public abstract class StatValue<T>
    {
        private readonly Stat<T> _stat;
        public readonly ObservableVar<T> CurrentValue;
        
        public IObservableVar<int> CurrentLevel => _stat.CurrentLevel;
        public IObservableVar<bool> IsMaxLevel => _stat.IsMaxLevel;
        
        public T CurrentVolume => _stat.CurrentValue;

        public StatValue(T defaultValue, Stat<T> stat)
        {
            _stat = stat;
            CurrentValue = new ObservableVar<T>(default(T));
        }

        public abstract void AddValue(T addedValue);
    }

    public sealed class IntStatValue : StatValue<int>
    {
        public IntStatValue(int defaultValue, Stat<int> stat) : base(defaultValue, stat) { }

        public override void AddValue(int addedValue)
        {
            
        }
    }
}