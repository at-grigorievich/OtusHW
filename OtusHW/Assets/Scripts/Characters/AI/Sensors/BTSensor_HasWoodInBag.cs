using System;
using ATG.Inventory;
using DefaultNamespace;
using MBT;

namespace ATG.Characters.AI
{
    public sealed class BTSensor_HasWoodInBag: BTSensor_Wrapper<BoolVariable, bool>
    {
        private readonly ResourceType _requiredResourceType;
        private readonly IInventoryChecker _inventoryChecker;

        protected override string Key => "HasWoodInBag";
        
        public BTSensor_HasWoodInBag(Blackboard blackboard, IInventoryChecker inventoryChecker)
        :base(blackboard)
        {
            _inventoryChecker = inventoryChecker;
            _requiredResourceType = ResourceType.Wood;
            
            _inventoryChecker.OnInventoryChanged += OnInventoryChanged;
        }

        public override void Dispose()
        {
            _inventoryChecker.OnInventoryChanged -= OnInventoryChanged;
        }

        protected override bool GetValue()
        {
            return _inventoryChecker.HasResource(_requiredResourceType);
        }

        private void OnInventoryChanged(InventoryCheckerEventArgs obj)
        {
            if(obj.ResourceTag != _requiredResourceType) return;
            UpdateBlackboard(obj.HasResource);
        }
    }
}