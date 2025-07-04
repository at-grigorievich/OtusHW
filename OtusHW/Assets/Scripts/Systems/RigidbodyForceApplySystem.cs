using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class RigidbodyForceApplySystem: IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<RigidbodyView,ForceSensitive>> _filter;


        public void PostRun(IEcsSystems systems)
        {
            EcsPool<RigidbodyView> viewPool = _filter.Pools.Inc1;
            EcsPool<ForceSensitive> forcePool = _filter.Pools.Inc2;

            foreach (var i in _filter.Value)
            {
                Rigidbody rb = viewPool.Get(i).Value;
                ForceSensitive force = forcePool.Get(i);

                Vector3 direction = force.Direction * force.ForceAmplitude;
                
                rb.AddForce(direction, ForceMode.Impulse);
                
                forcePool.Del(i);
            }
        }
    }
}