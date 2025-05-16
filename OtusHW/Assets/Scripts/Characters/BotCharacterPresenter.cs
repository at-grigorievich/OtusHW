using System;
using ATG.Animators;
using ATG.Inventory;
using ATG.Move;
using DefaultNamespace;
using UnityEngine;
using VContainer;

namespace ATG.Characters
{
    [Serializable]
    public sealed class BotCharacterCreator
    {
        [SerializeField] private BotCharacterView view;
        [SerializeField] private CrossfadeAnimatorFactory crossfadeAnimatorFactory;
        [SerializeField] private NavMeshMoveServiceFactory navMeshMoveServiceFactory;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<BotCharacterPresenter>(Lifetime.Singleton)
                .WithParameter<BotCharacterView>(view)
                .WithParameter<IMoveableService>(navMeshMoveServiceFactory.Create())
                .WithParameter<IAnimatorService>(crossfadeAnimatorFactory.Create())
                .AsSelf().AsImplementedInterfaces();
        }
    }
    
    public sealed class BotCharacterPresenter: CharacterPresenter, IInventoryOwner, IDisposable
    {
        private readonly BotCharacterView _view;
        private readonly IInventoryOwner _inventory;
        
        public BotCharacterPresenter(BotCharacterView view,
            IMoveableService moveService, IAnimatorService animatorService) 
            : base(moveService, animatorService)
        {
            _inventory = new Bag();
            _view = view;
            
            _view.GetResourceAmountFunc += GetResourceAmount;
            _view.AddResourceAction += AddElementsByType;
            _view.RemoveResourceAction += RemoveElementsByType;
        }

        public void Dispose()
        {
            _view.GetResourceAmountFunc -= GetResourceAmount;
            _view.AddResourceAction -= AddElementsByType;
            _view.RemoveResourceAction -= RemoveElementsByType;
        }
        
        public int GetResourceAmount(ResourceType resourceType) => 
            _inventory.GetResourceAmount(resourceType);

        public void AddElementsByType(ResourceType resourceType, int count)
        {
            _inventory.AddElementsByType(resourceType, count);
        }
        
        public void RemoveElementsByType(ResourceType resourceType, int count) => 
            _inventory.RemoveElementsByType(resourceType, count);
    }
}