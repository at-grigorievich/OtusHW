using System;
using System.Threading;
using ATG.Inventory;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

namespace ATG.Resource
{
    public class ResourcePresenter: IDisposable
    {
        private readonly ResourceView _view;
        private CancellationTokenSource _cts;

        public ResourceType ResourceType => _view.ResType;
        
        public event Action<ResourcePresenter> OnResourceAvailable; 
        public event Action<ResourcePresenter> OnDropRequired;
        
        public ResourcePresenter(ResourceView view)
        {
            _view = view;
            _view.OnTriggerEntered += OnTriggerEntered;
        }

        public void SetActive(bool active)
        {
            _view.SetActive(active);
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            _view.OnTriggerEntered -= OnTriggerEntered;
        }
        
        public float GetDistanceTo(Vector3 target) => Vector3.Distance(_view.transform.position, target);
        
        public void StartBirth(float bornDurationInSec)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            _cts = new CancellationTokenSource();
            
            SetActive(false);
            BornDurationInSec().Forget();

            async UniTask BornDurationInSec()
            {
                await UniTask.Delay(TimeSpan.FromSeconds(bornDurationInSec), cancellationToken: _cts.Token);
                OnBorn();
            }
        }

        private void OnBorn()
        {
            SetActive(true);
            OnResourceAvailable?.Invoke(this);
        }
        
        private void OnTriggerEntered(Collider obj)
        {
            if(obj.TryGetComponent(out IInventoryOwner inventory) == false) return;
            inventory.AddElementsByType(_view.ResType, 1);
            
            OnDropRequired?.Invoke(this);
        }
    }
}