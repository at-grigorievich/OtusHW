using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class GetDamageSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Damage, Health>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<Damage> dmgPool = _filter.Pools.Inc1;
            EcsPool<Health> healthPool = _filter.Pools.Inc2;

            foreach (var i in _filter.Value)
            {
                Damage damage = dmgPool.Get(i);
                
                float dmg = damage.Value;
                
                ref Health health = ref healthPool.Get(i);
                
                health.Value = Mathf.Clamp(health.Value - dmg, 0, health.MaxValue);

                dmgPool.Del(i);
            }
        }
    }
}