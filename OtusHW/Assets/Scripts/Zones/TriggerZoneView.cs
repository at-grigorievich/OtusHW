using System;
using UnityEngine;

namespace ATG.Zone
{
    [RequireComponent(typeof(Collider))]
    public class TriggerZoneView: ZoneView
    {
        private Collider _collider;

        public event Action<Collider> OnTriggerEntered;
        
        private new void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other);
        }
    }
}