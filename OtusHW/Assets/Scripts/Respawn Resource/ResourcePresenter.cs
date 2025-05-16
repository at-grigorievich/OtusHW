using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ATG.Resource
{
    public class ResourcePresenter: IDisposable
    {
        private readonly ResourceView _view;
        private CancellationTokenSource _cts;

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
            Debug.Log(obj.name);
            OnDropRequired?.Invoke(this);
        }
    }
}