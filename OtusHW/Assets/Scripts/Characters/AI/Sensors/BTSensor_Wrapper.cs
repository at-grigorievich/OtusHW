using System;
using MBT;

namespace ATG.Characters.AI
{
    public abstract class BTSensor_Wrapper<T,TK>: IDisposable where T: Variable<TK>
    {
        protected abstract string Key { get; }
        private Blackboard _blackboard;

        public BTSensor_Wrapper(Blackboard blackboard)
        {
            _blackboard = blackboard;
        }

        public void Update()
        {
            TK nextValue = GetValue();
            UpdateBlackboard(nextValue);
        }
        
        public abstract void Dispose();

        protected abstract TK GetValue();
        
        protected void UpdateBlackboard(TK value)
        {
            var result = _blackboard.GetVariable<T>(Key);
            if (result == null)
            {
                throw new NullReferenceException($"No variable with {Key} key added into Blackboard");
            }

            result.Value = value;
        }
    }
}