using System;
using System.Collections.Generic;
using System.Linq;
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
    
    public class ResourcesPool: IStartable, IDisposable
    {
        private readonly float _startBirthDurationInSeconds;
        private readonly float _restartBirthDurationInSeconds;
        private readonly ResourcePresenter[] _resources;

        public readonly HashSet<ResourcePresenter> AvailableResources;
        
        public int AvailableResourcesCount => AvailableResources.Count;
        
        public ResourcesPool(ResourcePresenter[] resources, float sbd, float rbd)
        {
            _resources = resources;
            
            _startBirthDurationInSeconds = sbd;
            _restartBirthDurationInSeconds = rbd;
            
            AvailableResources = new HashSet<ResourcePresenter>();
            
            foreach (var resourcePresenter in _resources)
            {
                resourcePresenter.OnResourceAvailable += OnResourceAvailable;
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
                resourcePresenter.OnResourceAvailable -= OnResourceAvailable;
            }
        }

        private void OnResourceAvailable(ResourcePresenter availableResource)
        {
            AvailableResources.Add(availableResource);
        }
        
        private void OnResourceDropRequired(ResourcePresenter resourcePresenter)
        {
            if(AvailableResources.Contains(resourcePresenter) == false) return;
            
            AvailableResources.Remove(resourcePresenter);
            resourcePresenter.StartBirth(_restartBirthDurationInSeconds);
        }
    }
}