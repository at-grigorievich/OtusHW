using ATG.Resource;
using DefaultNamespace;
using MBT;

namespace ATG.Characters.AI
{
    public sealed class BTSensor_ForestHasTree: BTSensor_Wrapper<BoolVariable, bool>
    {
        private readonly IResourceChecker _resourceChecker;
        private readonly ResourceType _requiredResourceType;
        
        protected override string Key => "ForestHasTrees";
        
        public BTSensor_ForestHasTree(Blackboard blackboard, IResourceChecker resourceChecker) : base(blackboard)
        {
            _resourceChecker = resourceChecker;
            _requiredResourceType = ResourceType.Wood;
            
            _resourceChecker.OnAvailableResourceChanged += OnAvailableResourceChanged;
        }

        public override void Dispose()
        {
            _resourceChecker.OnAvailableResourceChanged -= OnAvailableResourceChanged;
        }

        protected override bool GetValue()
        {
            return _resourceChecker.HasResourceByType(_requiredResourceType);
        }
        
        private void OnAvailableResourceChanged(ResourceCheckerEventArgs obj)
        {
            if(obj.Tag != _requiredResourceType) return;
            UpdateBlackboard(obj.HasResource);
        }
    }
}