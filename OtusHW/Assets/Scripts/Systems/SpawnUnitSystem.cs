using Client.Components;
using Client.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems
{
    public class SpawnUnitSystem: IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab, Team>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Target> _targetPool = EcsWorlds.EVENTS;
        
        private readonly EcsCustomInject<EntityManager> _entityManager;
        
        public void Run(IEcsSystems systems)
        {
            foreach(var e in _filter.Value)
            {
                Vector3 position = _filter.Pools.Inc2.Get(e).Value;
                Quaternion rotation = _filter.Pools.Inc3.Get(e).Value;
                Entity prefab = _filter.Pools.Inc4.Get(e).Value;
                TeamType team = _filter.Pools.Inc5.Get(e).Value;
                
                Entity instance = _entityManager.Value.Create(prefab, position, rotation);
                
                instance.SetData(new Team { Value = team });

                if (_targetPool.Value.Has(e))
                {
                    var target = _targetPool.Value.Get(e).Value;
                    instance.SetData(new Target { Value = target});
                }
                
                
                _eventWorld.Value.DelEntity(e);
            }
        }
    }
}