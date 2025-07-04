using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    public class CharacterInstaller: EntityInstaller
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private Transform firePoint;
        
        [SerializeField] private Entity bulletPrefab;
        
        protected override void Install(Entity entity)
        {
            /*entity.AddData(new Position { Value = transform.position});
            entity.AddData(new Rotation { Value = transform.rotation});
            entity.AddData(new Direction { Value = Vector3.forward});
            entity.AddData(new MoveSpeed { Value = moveSpeed});
            entity.AddData(new TransformView { Value = transform});
            entity.AddData(new BulletWeapon()
            {
                FirePoint = firePoint,
                BulletPrefab = bulletPrefab
            });
            entity.AddData(new Health()
            {
                Value = maxHealth,
                MaxValue = maxHealth
            });*/
        }

        protected override void Dispose(Entity entity)
        {
        
        }
    }
}