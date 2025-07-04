using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    public sealed class BulletInstaller: EntityInstaller
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float forceAmplitude = 3.0f;
        [SerializeField] private float damage;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new DestroyOnCollision());
            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new RigidbodyView { Value = rb });
            entity.AddData(new Attack() { Value = damage });
            entity.AddData(new ForceSensitive
            {
                ForceAmplitude = forceAmplitude,
                Direction = transform.forward
            });
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}