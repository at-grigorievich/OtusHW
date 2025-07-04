namespace Core
{
    public sealed class SpecViewModel
    {
        public readonly string Tag;
        
        public int Value { get; private set; }
        
        public SpecViewModel(string tag)
        {
            Tag = tag;
        }

        public void ChangeValue(int value)
        {
            if( value == Value ) return;

            Value = value;
        }
    }
}