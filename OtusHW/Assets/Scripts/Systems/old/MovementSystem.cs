using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Direction, MoveSpeed, Position>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            float deltaTime = UnityEngine.Time.deltaTime;
            
            #region old
            //EcsWorld world = systems.GetWorld();
            //
            //var ecsFilter = world.Filter<MoveDirection>().Inc<MoveSpeed>().Inc<Position>().End();
            //
            //EcsPool<MoveDirection> moveDirectionPool = world.GetPool<MoveDirection>();
            //EcsPool<MoveSpeed> moveSpeedPool = world.GetPool<MoveSpeed>();
            //EcsPool<Position> positionPool = world.GetPool<Position>();
            #endregion

            EcsPool<Direction> moveDirectionPool = _filter.Pools.Inc1;
            EcsPool<MoveSpeed> moveSpeedPool = _filter.Pools.Inc2;
            EcsPool<Position> positionPool = _filter.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                Direction direction = moveDirectionPool.Get(entity);
                MoveSpeed moveSpeed = moveSpeedPool.Get(entity);
                
                ref Position position = ref positionPool.Get(entity);
                position.Value += direction.Value * (moveSpeed.Value * deltaTime);
            }
        }
    }
}