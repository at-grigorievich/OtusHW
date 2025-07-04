using Client.Components;
using Client.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class ShootBulletRequestSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DetectedEnemyPosition, BulletWeapon, Team, FireDelay>> _filter;
        private readonly EcsCustomInject<SceneData> _sceneData;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Team> _teamPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Parent> _parentPool = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<DetectedEnemyPosition> detectedEnemiesPool = _filter.Pools.Inc1;
            EcsPool<BulletWeapon> weaponPool = _filter.Pools.Inc2;
            EcsPool<Team> teamPool = _filter.Pools.Inc3;
            EcsPool<FireDelay> fireDelayPool = _filter.Pools.Inc4;
            
            foreach (var i in _filter.Value)
            {
                ref FireDelay fireDelay = ref fireDelayPool.Get(i);
                DetectedEnemyPosition detectedEnemy = detectedEnemiesPool.Get(i);
                
                if(fireDelay.Value < fireDelay.Limit) continue;
                if(detectedEnemy.Value == false) continue;

                BulletWeapon weapon = weaponPool.Get(i);
                TeamType teamType = teamPool.Get(i).Value;
                
                int spawnBulletEvent = _eventWorld.Value.NewEntity();
                _spawnPool.Value.Add(spawnBulletEvent) = new SpawnRequest();
                _positionPool.Value.Add(spawnBulletEvent) = new Position { Value = weapon.FirePoint.position };
                _rotationPool.Value.Add(spawnBulletEvent) = new Rotation { Value = weapon.FirePoint.rotation };
                _prefabPool.Value.Add(spawnBulletEvent) = new Prefab { Value = weapon.BulletPrefab };
                _teamPool.Value.Add(spawnBulletEvent) = new Team { Value = teamType };
                _parentPool.Value.Add(spawnBulletEvent) = new Parent { Value = _sceneData.Value.bulletsRoot };
                
                fireDelay.Value = 0f;
            }
        }
    }
}