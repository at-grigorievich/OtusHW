using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class TargetRotationSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RigidbodyView, Rotation, Target>> _filter;
        private readonly EcsPoolInject<DetectedEnemyPosition> _detectedEnemiesPool;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<RigidbodyView> rbPool = _filter.Pools.Inc1;
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc2;
            EcsPool<Target> targetPool = _filter.Pools.Inc3;
            
            foreach (var i in _filter.Value)
            {
                Rigidbody rb = rbPool.Get(i).Value;
                Target target = targetPool.Get(i);
                
                ref Rotation rotation = ref rotationPool.Get(i);

                Vector3 direction = (target.Value.position - rb.position).normalized;
                
                if (_detectedEnemiesPool.Value.Has(i))
                {
                    DetectedEnemyPosition detectedEnemyPosition = _detectedEnemiesPool.Value.Get(i);
                    bool hasDetected = detectedEnemyPosition.Value;

                    if (hasDetected)
                    {
                        direction = (detectedEnemyPosition.EnemyPosition - rb.position).normalized;
                    }
                }

                rotation.Value = Quaternion.LookRotation(direction);
            }
        }
    }
}