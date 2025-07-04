using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class ReloadFireDelaySystem: IEcsRunSystem
    {
        private EcsFilterInject<Inc<FireDelay>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            EcsPool<FireDelay> fireDelayPool = _filter.Pools.Inc1;
            
            foreach (var i in _filter.Value)
            {
                ref FireDelay fireDelay = ref fireDelayPool.Get(i);

                fireDelay.Value = Mathf.Clamp(fireDelay.Value + Time.deltaTime, 0f, fireDelay.Limit);
            }
        }
    }
}