using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace ATG.Inventory
{
    public sealed class Bag: IInventoryOwner, IInventoryChecker
    {
        //IntStatValue for Inventory progress can be added
        
        private readonly Dictionary<ResourceType, int> _resources = new();

        public event Action<InventoryCheckerEventArgs> OnInventoryChanged;
        
        public int GetResourceAmount(ResourceType resourceType)
        {
            return _resources.GetValueOrDefault(resourceType, 0);
        }
        
        public void AddElementsByType(ResourceType resourceType, int count)
        {
            if (_resources.TryAdd(resourceType, count) == false)
                _resources[resourceType] += count;

            int totalCount = _resources[resourceType];
            OnInventoryChanged?.Invoke(new InventoryCheckerEventArgs(resourceType, totalCount));
        }

        public void RemoveElementsByType(ResourceType resourceType, int count)
        {
            if(_resources.ContainsKey(resourceType) == false) return;
            _resources[resourceType] -= count;
            
            int totalCount = _resources[resourceType];
            OnInventoryChanged?.Invoke(new InventoryCheckerEventArgs(resourceType, totalCount));
        }
        
        public bool HasResource(ResourceType resourceType)
        {
            if (_resources.TryGetValue(resourceType, out var amount) == false) return false;
            return amount > 0;
        }
    }
}