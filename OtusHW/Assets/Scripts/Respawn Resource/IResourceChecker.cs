using System;
using DefaultNamespace;
using UnityEngine;

namespace ATG.Resource
{
    public readonly struct ResourceCheckerEventArgs
    {
        public readonly ResourceType Tag;
        public readonly int Amount;
        
        public bool HasResource => Amount > 0;

        public ResourceCheckerEventArgs(ResourceType tag, int amount)
        {
            Tag = tag;
            Amount = amount;
        }
    }
    
    public interface IResourceChecker
    {
        bool HasResourceByType(ResourceType resourceType);
        event Action<ResourceCheckerEventArgs> OnAvailableResourceChanged; 
        
        bool TryGetNearestResourceByType(ResourceType resourceType, Vector3 targetPosition,
            out ResourcePresenter resourcePresenter);
    }
}