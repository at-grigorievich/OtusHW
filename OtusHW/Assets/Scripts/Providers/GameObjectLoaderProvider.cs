using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Providers
{
    public class GameObjectLoaderProvider: IDisposable
    {
        private readonly AssetReferenceGameObject _reference;
        
        private GameObject _instance;

        public GameObjectLoaderProvider(AssetReferenceGameObject reference)
        {
            _reference = reference;
        }
        
        public async Task<T> LoadAsync<T>(Transform root = null)
        {
            var instantiateHandle = _reference.InstantiateAsync(root);

            await instantiateHandle.Task;

            if (instantiateHandle.Status == AsyncOperationStatus.Succeeded)
            {
                _instance = instantiateHandle.Result;

                if (_instance.TryGetComponent(out T result) == false)
                    throw new NullReferenceException($"No {typeof(T)} component found in {_reference}");

                return result;
            }
            
            throw new Exception($"Failed to load {typeof(T)}");
        }

        private void Unload()
        {
            if (_instance == null) return;

            _instance.SetActive(false);
            Addressables.ReleaseInstance(_instance);
            
            _instance = null;
        }

        public void Dispose()
        {
            Unload();
        }

        public override int GetHashCode()
        {
            return _reference.GetHashCode();
        }
    }
}