using Client.Data;
using Client.Systems;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour
    {
        public SceneData sceneData;
        
        private EcsWorld _world;
        private EcsWorld _events;

        private IEcsSystems _systems;
        private IEcsSystems _fixedSystems;
        
        private EntityManager _entityManager;

        private void Awake()
        {
            _entityManager = new EntityManager();
            
            _world = new EcsWorld();
            _events = new EcsWorld();
            
            _systems = new EcsSystems (_world);
            _fixedSystems = new EcsSystems (_world);
            
            _systems.AddWorld(_events, EcsWorlds.EVENTS);
            _fixedSystems.AddWorld(_events, EcsWorlds.EVENTS);
            
            EcsPhysicsEvents.ecsWorld = _events;
            
            _systems
                // register your systems here, for example:
                .Add(new SpawnUnitRequestSystem())
                .Add(new SpawnSystem())
                .Add(new TargetMovementSystem())
                .Add(new TargetRotationSystem())
                .Add(new ReloadFireDelaySystem())
                .Add(new GetDamageSystem())
                .Add(new DeathSystem())
                .Add(new DestroySystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS));
#endif

            _fixedSystems
                .Add(new DetectEnemySystem())
                .Add(new ShootBulletRequestSystem())
                .Add(new RigidbodyForceApplySystem())
                .Add(new BulletCollisionSystem())
                .Add(new RigidbodyViewMoveSystem());
        }
        
        private void Start ()
        {
            _entityManager.Initialize(_world);
            
            _systems
                .Inject(_entityManager)
                .Inject(sceneData);
            
            _fixedSystems
                .Inject(sceneData);
            
            _systems.Init ();
            _fixedSystems.Init();
        }

        private void Update () 
        {
            // process systems here.
            _systems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedSystems?.Run();
        }

        private void OnDestroy () 
        {
            if (_systems != null) 
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
                
                _fixedSystems.Destroy();
                _systems = null;
                
                EcsPhysicsEvents.ecsWorld = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }

            if (_events != null)
            {
                _events.Destroy();
                _events = null;
            }
        }
    }
}