using System;
using DefaultNamespace;

namespace ATG.Inventory
{
    public readonly struct InventoryCheckerEventArgs
    {
        public readonly ResourceType ResourceTag;
        public readonly int Quantity;
        
        public bool HasResource => Quantity > 0;

        public InventoryCheckerEventArgs(ResourceType tag, int quantity)
        {
            ResourceTag = tag;
            Quantity = quantity;
        }
    }
    
    public interface IInventoryChecker
    {
        event Action<InventoryCheckerEventArgs> OnInventoryChanged;
        bool HasResource(ResourceType resourceType);
    }
}