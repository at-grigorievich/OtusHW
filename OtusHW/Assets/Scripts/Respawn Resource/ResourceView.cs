using System;
using UnityEngine;

namespace ATG.Resource
{
    [RequireComponent(typeof(Collider))]
    public class ResourceView: MonoBehaviour
    {
        [SerializeField] private Renderer _mainRenderer;
        
        private Collider _collider;
        
        public event Action<Collider> OnTriggerEntered; 
        
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