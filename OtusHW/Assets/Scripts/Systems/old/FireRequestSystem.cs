using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public sealed class FireRequestSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FireRequest, BulletWeapon>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;

        public void Run(IEcsSystems systems)
        {
            EcsPool<BulletWeapon> weaponPool = _filter.Pools.Inc2;
            EcsPool<FireRequest> requestPool = _filter.Pools.Inc1; 
            
            foreach (var entity in _filter.Value)
            {
                BulletWeapon weapon = weaponPool.Get(entity);

                int spawnEvent = _eventWorld.Value.NewEntity();

                _spawnPool.Value.Add(spawnEvent) = new SpawnRequest();
                _positionPool.Value.Add(spawnEvent) = new Position { Value = weapon.FirePoint.position };
                _rotationPool.Value.Add(spawnEvent) = new Rotation { Value = weapon.FirePoint.rotation };
                _prefabPool.Value.Add(spawnEvent) = new Prefab { Value = weapon.BulletPrefab };
                
                requestPool.Del(entity);
            }
        }
    }
}