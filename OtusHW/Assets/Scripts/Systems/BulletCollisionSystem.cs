using Client.Components;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems
{
    public class BulletCollisionSystem: IEcsRunSystem
    {
        private readonly EcsCustomInject<EntityManager> _entityManager;
        
        public void Run(IEcsSystems systems)
        {
            var filter = systems.GetWorld(EcsWorlds.EVENTS).Filter<OnCollisionEnterEvent>().End();
            var pool = systems.GetWorld(EcsWorlds.EVENTS).GetPool<OnCollisionEnterEvent>();
            
            foreach (var entity in filter)
            {
                ref var eventData = ref pool.Get(entity);
                
                if(eventData.senderGameObject == null || eventData.collider == null) continue;
                
                if (eventData.senderGameObject.TryGetComponent(out Entity sourceEntity))
                {
                    if (sourceEntity.HasData<DestroyOnCollision>())
                    {
                        if(sourceEntity.HasData<DestroyRequest>() == false)
                            sourceEntity.AddData(new DestroyRequest());
                    }
                    
                    if (eventData.collider.TryGetComponent(out Entity collisionEntity) == false) continue;
                    
                    if(sourceEntity.TryGetData(out Attack attack) == false) continue;
                    if(sourceEntity.TryGetData(out Team srcTeam) == false) continue;
                    if(collisionEntity.TryGetData(out Team collisionTeam) == false) continue;
                    
                    if(collisionTeam.Value == srcTeam.Value) continue;
                    
                    if (collisionEntity.HasData<Damage>())
                    {
                        ref Damage dmg = ref collisionEntity.GetData<Damage>();
                        
                        dmg.Value += attack.Value;
                    }
                    else
                    {
                        collisionEntity.AddData(new Damage
                        {
                            Value = attack.Value
                        });
                    }
                }
                
                pool.Del(entity);
            }
        }
    }
}