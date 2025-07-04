using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public class DeathSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>> _filter;
        private readonly EcsPoolInject<DestroyRequest> _destroyRequestPool;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;
            
            foreach (var i in _filter.Value)
            {
                float health = healthPool.Get(i).Value;

                if (health <= 0)
                {
                    _destroyRequestPool.Value.Add(i);
                }
            }
        }
    }
}