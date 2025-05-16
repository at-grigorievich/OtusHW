using DefaultNamespace;

namespace ATG.Inventory
{
    public interface IInventoryOwner
    {
        int GetResourceAmount(ResourceType resourceType);
        void AddElementsByType(ResourceType resourceType, int count);
        void RemoveElementsByType(ResourceType resourceType, int count);
    }
}