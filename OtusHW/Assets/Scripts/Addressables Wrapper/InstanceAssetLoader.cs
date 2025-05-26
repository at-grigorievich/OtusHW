using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ATG.AddressablesExample
{
    public class InstanceAssetLoader
    {
        private GameObject _cached;

        protected async Task<T> LoadInternal<T>(string assetId)
        {
            var handle = Addressables.InstantiateAsync(assetId);
            _cached = await handle.Task;

            if (_cached.TryGetComponent(out T cachedComponent) == false)
            {
                throw new NullReferenceException($"No component found for {assetId}");
            }

            return cachedComponent;
        }

        protected void UnloadInternal()
        {
            if (_cached == null) return;

            _cached.SetActive(false);
            Addressables.ReleaseInstance(_cached);
        }
    }
}
