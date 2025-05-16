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

        public BotCharacterPresenter Presenter { get; private set; }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _collider.isTrigger = false;
            _rb.useGravity = false;
            _rb.isKinematic = true;
        }
        
        public void Initialize(BotCharacterPresenter presenter) => Presenter = presenter;
        
        public int GetResourceAmount(ResourceType resourceType) => 
            Presenter.GetResourceAmount(resourceType);
        
        public void AddElementsByType(ResourceType resourceType, int count) =>
            Presenter.AddElementsByType(resourceType, count);
        
        public void RemoveElementsByType(ResourceType resourceType, int count) => 
            Presenter.RemoveElementsByType(resourceType, count);
    }
}