using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    [RequireComponent(typeof(Collider))]
    public class UnlockLocationTrigger: MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject locationReference;
        
        private Collider _collider;

        private bool _alreadyUnlocked;

        public event Action<AssetReferenceGameObject> OnLocationUnlocked; 
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_alreadyUnlocked == true) return;
            OnLocationUnlocked?.Invoke(locationReference);
        }
    }
}