using Client.Components;
using Client.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems
{
    public class DetectEnemySystem: IEcsRunSystem
    {
        private readonly int _layerMask = ~LayerMask.GetMask("bullet");
        private readonly EcsFilterInject<Inc<DetectedEnemyPosition, RigidbodyView, Team, AttackRange>> _filter;
        
        RaycastHit _hit;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<DetectedEnemyPosition> detectEnemyPool = _filter.Pools.Inc1;
            EcsPool<RigidbodyView> rbPool = _filter.Pools.Inc2;
            EcsPool<Team> teamPool = _filter.Pools.Inc3;
            EcsPool<AttackRange> attackRangePool = _filter.Pools.Inc4;

            foreach (var i in _filter.Value)
            {
                Rigidbody rb = rbPool.Get(i).Value;
                TeamType team = teamPool.Get(i).Value;
                float attackRange = attackRangePool.Get(i).Value;
                
                
                Vector3 pos = rb.worldCenterOfMass;
                Quaternion rot = rb.rotation;
                Vector3 forward = rb.transform.forward;
                Vector3 bounds = attackRangePool.Get(i).Bounds;
                
                ref DetectedEnemyPosition detectedEnemyPosition = ref detectEnemyPool.Get(i);

                bool castSuccessful = Physics.Raycast(pos, forward, out _hit, attackRange, _layerMask);

                if (castSuccessful == false)
                {
                    castSuccessful = Physics.BoxCast(pos, bounds, forward, out _hit, rot,
                        attackRange, _layerMask);
                }
                
                detectedEnemyPosition.Value = false;

                if (castSuccessful == true)
                {
                    if(_hit.transform.TryGetComponent(out Entity entity) == false) continue;
                    if(entity.TryGetData(out Team teamData) == false) continue;
                    if(teamData.Value == team) continue;
                    
                    detectedEnemyPosition.Value = true;
                    detectedEnemyPosition.EnemyPosition = _hit.transform.position;
                    
#if UNITY_EDITOR
                    Debug.DrawLine(pos, _hit.transform.position);
#endif
                }
            }
        }
    }
}