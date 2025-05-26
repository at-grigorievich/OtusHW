using System;
using System.Collections.Generic;
using Providers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    [Serializable]
    public sealed class UnlockLocationServiceCreator
    {
        [SerializeField] private Transform locationsRoot;
        [SerializeField] private AssetReferenceGameObject startedLocationAsset;
        [SerializeField] private UnlockLocationTrigger[] unlockTriggers;

        public void Create(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<UnlockLocationService>()
                .AsSingle()
                .WithArguments(startedLocationAsset, unlockTriggers, locationsRoot)
                .NonLazy();
        }
    }
    
    public sealed class UnlockLocationService: IInitializable, IDisposable
    {
        private readonly Transform _locationsRoot;
        private readonly UnlockLocationTrigger[] _unlockTriggers;
        private readonly AssetReferenceGameObject _startedLocationReference;
        
        private readonly HashSet<GameObjectLoaderProvider> _loadedLocations;

        [Inject]
        public UnlockLocationService(AssetReferenceGameObject startedLocationReference,
            UnlockLocationTrigger[] unlockTriggers, Transform root)
        {
            _locationsRoot = root;
            _unlockTriggers = unlockTriggers;
            _startedLocationReference = startedLocationReference;
            
            _loadedLocations = new HashSet<GameObjectLoaderProvider>();
        }
        
        public void Initialize()
        {
            foreach (var trigger in _unlockTriggers)
            {
                trigger.OnLocationUnlocked += OnLocationUnlocked;
            }
            
            OnLocationUnlocked(_startedLocationReference);
        }
        
        public void Dispose()
        {
            foreach (var trigger in _unlockTriggers)
            {
                trigger.OnLocationUnlocked -= OnLocationUnlocked;
            }

            foreach (var assetProvider in _loadedLocations)
            {
                assetProvider.Dispose();
            }
            
            _loadedLocations.Clear();
        }

        private void OnLocationUnlocked(AssetReferenceGameObject assetReference)
        {
            Debug.Log("asfsafasafasf");
            var provider = new GameObjectLoaderProvider(assetReference);
            _loadedLocations.Add(provider);
            
            provider.LoadAsync<GameObject>(_locationsRoot);
        }
    }
}