using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Resource
{
    [Serializable]
    public sealed class ResourcePoolCreator
    {
        [SerializeField] private ResourceView[] resourceViews;
        [SerializeField] private float startBirthDurationInSeconds;
        [SerializeField] private float restartBirthDurationInSeconds;

        public void Create(IContainerBuilder builder)
        {
            ResourcePresenter[] presenters = resourceViews.Select(view => new ResourcePresenter(view)).ToArray();

            builder.Register<ResourcesPool>(Lifetime.Singleton)
                .WithParameter(presenters)
                .WithParameter("sbd", startBirthDurationInSeconds)
                .WithParameter("rbd", restartBirthDurationInSeconds)
                .AsSelf().AsImplementedInterfaces();
        }
    }
    
    public class ResourcesPool: IStartable, IDisposable, IResourceChecker
    {
        private readonly float _startBirthDurationInSeconds;
        private readonly float _restartBirthDurationInSeconds;
        private readonly ResourcePresenter[] _resources;

        private readonly Dictionary<ResourceType, HashSet<ResourcePresenter>> _availableResources;
        
        public event Action<ResourceCheckerEventArgs> OnAvailableResourceChanged;
        
        public ResourcesPool(ResourcePresenter[] resources, float sbd, float rbd)
        {
            _resources = resources;
            
            _startBirthDurationInSeconds = sbd;
            _restartBirthDurationInSeconds = rbd;
            
            _availableResources = new ();
            
            foreach (var resourcePresenter in _resources)
            {
                resourcePresenter.OnResourceAvailable += OnResourceAvailableChanged;
                resourcePresenter.OnDropRequired += OnResourceDropRequired;
            }
        }
        
        public void Start()
        {
            foreach (var resource in _resources)
            {
                int rndAddRangeSec = UnityEngine.Random.Range(0, (int)_startBirthDurationInSeconds);
                resource.StartBirth(_startBirthDurationInSeconds + rndAddRangeSec);
            }
        }
        
        public void Dispose()
        {
            foreach (var resourcePresenter in _resources)
            {
                resourcePresenter.OnDropRequired -= OnResourceDropRequired;
                resourcePresenter.OnResourceAvailable -= OnResourceAvailableChanged;
            }
        }

        public bool HasResourceByType(ResourceType resourceType)
        {
            if (_availableResources.TryGetValue(resourceType, out var resource) == false) 
                return false;
            
            return resource.Count > 0;
        }
        
        public bool TryGetNearestResourceByType(ResourceType resourceType, Vector3 targetPosition,
            out ResourcePresenter resourcePresenter)
        {
            resourcePresenter = null;
            
            if (HasResourceByType(resourceType) == false) return false;

            var allResources = _availableResources[resourceType];

            resourcePresenter = allResources
                .OrderBy(res => res.GetDistanceTo(targetPosition))
                .First();

            return true;
        }
        
        private void OnResourceAvailableChanged(ResourcePresenter availableResource)
        {
            if (_availableResources.ContainsKey(availableResource.ResourceType) == false)
            {
                _availableResources.Add(availableResource.ResourceType, new HashSet<ResourcePresenter>());
            }
            _availableResources[availableResource.ResourceType].Add(availableResource);
            
            int availableResourcesCount = _availableResources[availableResource.ResourceType].Count;
            OnAvailableResourceChanged?.Invoke(
                new ResourceCheckerEventArgs(availableResource.ResourceType, availableResourcesCount));
        }
        
        private void OnResourceDropRequired(ResourcePresenter resourcePresenter)
        {
            if(_availableResources.TryGetValue(resourcePresenter.ResourceType, 
                   out var resourceSet) == false) return;
            
            resourceSet.Remove(resourcePresenter);
            resourcePresenter.StartBirth(_restartBirthDurationInSeconds);
            
            int availableResourcesCount = resourceSet.Count;
            OnAvailableResourceChanged?.Invoke(
                new ResourceCheckerEventArgs(resourcePresenter.ResourceType, availableResourcesCount));
        }
    }
}