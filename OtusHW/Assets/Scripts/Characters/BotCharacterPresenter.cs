using System;
using ATG.Animators;
using ATG.Characters.AI;
using ATG.Inventory;
using ATG.Move;
using ATG.Resource;
using DefaultNamespace;
using DefaultNamespace.Conveyor;
using MBT;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Characters
{
    [Serializable]
    public sealed class BotCharacterCreator
    {
        [SerializeField] private BotCharacterView view;
        [SerializeField] private CrossfadeAnimatorFactory crossfadeAnimatorFactory;
        [SerializeField] private NavMeshMoveServiceFactory navMeshMoveServiceFactory;
        [SerializeField] private Blackboard blackboard;
        
        public void Create(IContainerBuilder builder)
        {
            builder.Register<BotCharacterPresenter>(Lifetime.Singleton)
                .WithParameter<BotCharacterView>(view)
                .WithParameter<IMoveableService>(navMeshMoveServiceFactory.Create())
                .WithParameter<IAnimatorService>(crossfadeAnimatorFactory.Create())
                .WithParameter<Blackboard>(blackboard)
                .AsSelf().AsImplementedInterfaces();
        }
    }
    
    public sealed class BotCharacterPresenter: CharacterPresenter, IInventoryOwner, IStartable, IDisposable
    {
        private readonly BotCharacterView _view;
        private readonly Bag _inventory;

        private readonly BTSensor_HasWoodInBag _hasWoodInBagSensor;
        private readonly BTSensor_LoadZoneAvailable _loadZoneAvailableSensor;
        private readonly BTSensor_ForestHasTree _forestHasTreeSensor;
        
        public BotCharacterPresenter(BotCharacterView view,
            IMoveableService moveService, IAnimatorService animatorService, Blackboard blackboard,
            ILoadedZoneChecker loadedZoneChecker, IResourceChecker resourceChecker) 
            : base(moveService, animatorService)
        {
            _view = view;
            _view.Initialize(this);
            
            _inventory = new Bag();

            _hasWoodInBagSensor = new BTSensor_HasWoodInBag(blackboard, _inventory);
            _loadZoneAvailableSensor = new BTSensor_LoadZoneAvailable(blackboard, loadedZoneChecker);
            _forestHasTreeSensor = new BTSensor_ForestHasTree(blackboard, resourceChecker);
        }

        public void Start()
        {
            _hasWoodInBagSensor.Update();
            _loadZoneAvailableSensor.Update();
            _forestHasTreeSensor.Update();
        }
        
        public void Dispose()
        {
            _hasWoodInBagSensor.Dispose();
            _loadZoneAvailableSensor.Dispose();
            _forestHasTreeSensor.Dispose();
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