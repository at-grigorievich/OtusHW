using DefaultNamespace.Conveyor;
using MBT;

namespace ATG.Characters.AI
{
    public sealed class BTSensor_LoadZoneAvailable: BTSensor_Wrapper<BoolVariable, bool>
    {
        private readonly ILoadedZoneChecker _loadedZoneChecker;
        
        protected override string Key => "LoadZoneAvailable";
        
        public BTSensor_LoadZoneAvailable(Blackboard blackboard, ILoadedZoneChecker loadedZoneChecker) : base(blackboard)
        {
            _loadedZoneChecker = loadedZoneChecker;
            _loadedZoneChecker.OnLoadedZoneAmountChanged += OnLoadedZoneAmountChanged;
        }

        public override void Dispose()
        {
            _loadedZoneChecker.OnLoadedZoneAmountChanged -= OnLoadedZoneAmountChanged;
        }

        protected override bool GetValue()
        {
            return _loadedZoneChecker.IsLoadedZoneAvailable;
        }
        
        private void OnLoadedZoneAmountChanged()
        {
            bool isAvailable = _loadedZoneChecker.IsLoadedZoneAvailable;
            UpdateBlackboard(isAvailable);
        }
    }
}