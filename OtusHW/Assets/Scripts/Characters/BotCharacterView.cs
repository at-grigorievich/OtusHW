using System;
using ATG.Inventory;
using DefaultNamespace;
using UnityEngine;

namespace ATG.Characters
{
    [RequireComponent(typeof(Rigidbody), typeof(Rigidbody))]
    public class BotCharacterView: MonoBehaviour, IInventoryOwner
    {
        private Rigidbody _rb;
        private Collider _collider;
        
        public event Func<ResourceType, int> GetResourceAmountFunc;
        public event Action<ResourceType, int> AddResourceAction;
        public event Action<ResourceType, int> RemoveResourceAction;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _collider.isTrigger = false;
            _rb.useGravity = false;
            _rb.isKinematic = true;
        }
        
        public int GetResourceAmount(ResourceType resourceType)
        {
            return GetResourceAmountFunc?.Invoke(resourceType) ?? 0;
        }

        public void AddElementsByType(ResourceType resourceType, int count)
        {
            AddResourceAction?.Invoke(resourceType, count);
        }

        public void RemoveElementsByType(ResourceType resourceType, int count)
        {
            RemoveResourceAction?.Invoke(resourceType, count);
        }
    }
}