using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class TransformViewSystem: IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> _filter;
        private readonly EcsPoolInject<Rotation> _rotationPool;
        
        public void PostRun(IEcsSystems systems)
        {
            EcsPool<Rotation> rotationPool = _rotationPool.Value;
            
            foreach (var entity in _filter.Value)
            {
                ref TransformView transform = ref _filter.Pools.Inc1.Get(entity);
                Position position = _filter.Pools.Inc2.Get(entity);

                transform.Value.position = position.Value;

                if (rotationPool.Has(entity))
                {
                    Quaternion rotation = rotationPool.Get(entity).Value;
                    transform.Value.rotation = rotation;
                }
            }
        }
    }
}