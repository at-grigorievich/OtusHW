using Client.Components;
using Client.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems
{
    public class SpawnUnitRequestSystem: IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Team> _teamPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Target> _targetPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Parent> _parentPool = EcsWorlds.EVENTS;
        
        public void Init(IEcsSystems systems)
        {
            for (int i = 0; i < _sceneData.Value.unitsPerTeam * 2; i++)
            {
                bool isRedTeam = i % 2 == 0;
                
                Transform point = isRedTeam 
                    ? _sceneData.Value.redTeamSpawnPoint 
                    : _sceneData.Value.blueTeamSpawnPoint;
                
                Transform enemyPoint = isRedTeam
                    ? _sceneData.Value.blueTeamSpawnPoint
                    : _sceneData.Value.redTeamSpawnPoint;
                
                Entity prefab = isRedTeam
                    ? _sceneData.Value.redSoldier
                    : _sceneData.Value.blueSoldier;
                
                int spawnEvent = _eventWorld.Value.NewEntity();

                _spawnPool.Value.Add(spawnEvent) = new SpawnRequest();
                _positionPool.Value.Add(spawnEvent) = new Position { Value = CalculateRndPosition(point.position)};
                _rotationPool.Value.Add(spawnEvent) = new Rotation { Value = Quaternion.LookRotation(point.forward)};
                _prefabPool.Value.Add(spawnEvent) = new Prefab() { Value = prefab };
                _teamPool.Value.Add(spawnEvent) = new Team() { Value = isRedTeam ? TeamType.Red : TeamType.Blue };
                _targetPool.Value.Add(spawnEvent) = new Target { Value = enemyPoint };
                _parentPool.Value.Add(spawnEvent) = new Parent { Value = point };
            }
        }

        private Vector3 CalculateRndPosition(Vector3 point)
        {
            Vector3 rnd = Random.insideUnitSphere * _sceneData.Value.spawnRadius;
            rnd.y = 0;
            
            return point + rnd;
        }
    }
}