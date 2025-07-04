using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    public class UnitInstaller: EntityInstaller
    {
        [SerializeField] private float moveSpeed = 2.0f;
        [SerializeField] private int health = 10;
        [SerializeField] private float attackRange = 10f;
        [SerializeField] private Vector2 attackRangeBounds = new Vector2(10f, 5f);
        [Space(5)]
        [SerializeField] private Rigidbody rb;
        [Space(5)] 
        [Header("Weapon setup")] 
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Entity bulletPrefab;
        [SerializeField] private float reloadDelay;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Direction { Value = Vector2.zero });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new MoveSpeed { Value = moveSpeed });
            entity.AddData(new RigidbodyView { Value = rb });
            entity.AddData(new DetectedEnemyPosition());
            
            entity.AddData(new AttackRange
            {
                Value = attackRange,
                Bounds = new Vector3(attackRangeBounds.x / 2f, attackRangeBounds.y / 2f, 0.1f)
            });
            
            entity.AddData(new FireDelay
            {
                Limit = reloadDelay, 
                Value = reloadDelay
            }); 
            entity.AddData(new BulletWeapon()
            {
                FirePoint = bulletSpawnPoint, 
                BulletPrefab = bulletPrefab
            });
            entity.AddData(new Health()
            {
                Value = health,
                MaxValue = health
            });
        }

        protected override void Dispose(Entity entity)
        {
            
        }
        
        
        void OnDrawGizmosSelected()
        {
            // Получаем центр BoxCast
            Vector3 origin = rb.worldCenterOfMass;
            Quaternion rot = rb.rotation;
            Vector3 direction = rb.transform.forward;
            Vector3 bounds = new Vector3(attackRangeBounds.x, attackRangeBounds.y, attackRange);
            
            // Визуализируем сам BoxCast (область)
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(origin + direction * attackRange / 2, bounds); // Отрисовываем куб, который отображает BoxCast
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(origin, direction * attackRange);
        }
    }
}