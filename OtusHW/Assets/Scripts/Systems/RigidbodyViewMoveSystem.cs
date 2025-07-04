using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class RigidbodyViewMoveSystem: IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<RigidbodyView, Direction, Rotation, MoveSpeed>> _filter;
        private readonly EcsPoolInject<DetectedEnemyPosition> _detectedEnemiesPool;
        
        public void PostRun(IEcsSystems systems)
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            
            EcsPool<RigidbodyView> rbPool = _filter.Pools.Inc1;
            EcsPool<Direction> moveDirectionPool = _filter.Pools.Inc2;
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc3;
            EcsPool<MoveSpeed> moveSpeedPool = _filter.Pools.Inc4;

            foreach (var i in _filter.Value)
            {
                float speed = moveSpeedPool.Get(i).Value;
                Vector3 direction = moveDirectionPool.Get(i).Value;
                Quaternion rotation = rotationPool.Get(i).Value;
                
                
                ref RigidbodyView rb = ref rbPool.Get(i);
                
                rb.Value.MovePosition(rb.Value.position + direction * speed * fixedDeltaTime);
                rb.Value.MoveRotation(Quaternion.Slerp(rb.Value.rotation, rotation, 2f * speed * fixedDeltaTime));
            }
        }
    }
}