using System;
using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems
{
    public sealed class DestroySystem: IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<DestroyRequest>> _filter;
        
        private readonly EcsCustomInject<EntityManager> _entityManager;
        
        public void PostRun(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _entityManager.Value.Destroy(i);
            }
        }
    }
}