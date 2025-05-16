using System;
using DefaultNamespace;
using UnityEngine;

namespace ATG.Resource
{
    [RequireComponent(typeof(Collider))]
    public class ResourceView: MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private Renderer _mainRenderer;
        
        private Collider _collider;
        
        public event Action<Collider> OnTriggerEntered;

        public ResourceType ResType => resourceType;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        public void SetActive(bool active)
        {
            _collider.enabled = active;
            _mainRenderer.enabled = active;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other);
        }
    }
}