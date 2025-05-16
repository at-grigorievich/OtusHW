using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace ATG.Resource
{
    [RequireComponent(typeof(Collider))]
    public class ResourceView: MonoBehaviour
    {
        private const float STAY_IN_TRIGGER_DURATION = 1.8f;
        
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private Renderer _mainRenderer;
        
        private Collider _collider;
        private Dictionary<Collider, float> _stayingColliders = new ();
        
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
            
            _stayingColliders.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            _stayingColliders.TryAdd(other, 0.0f);
        }

        private void OnTriggerStay(Collider other)
        {
            if(_stayingColliders.ContainsKey(other) == false) return;
            _stayingColliders[other] += Time.deltaTime;

            if (_stayingColliders[other] >= STAY_IN_TRIGGER_DURATION)
            {
                OnTriggerEntered?.Invoke(other);
                _stayingColliders.Remove(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(_stayingColliders.ContainsKey(other) == false) return;
            _stayingColliders.Remove(other);
        }
    }
}