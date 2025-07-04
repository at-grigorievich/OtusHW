using System;
using Client.Data;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components
{
    [Serializable]
    public struct Team
    {
        public TeamType Value;
    }

    [Serializable]
    public struct DetectedEnemyPosition
    {
        public bool Value;
        public Vector3 EnemyPosition;
    }
    
    [Serializable]
    public struct AttackRange
    {
        public Vector3 Bounds;
        public float Value;
    }
    
    [Serializable]
    public struct Position
    {
        public Vector3 Value;
    }
    
    [Serializable]
    public struct Rotation
    {
        public Quaternion Value;
    }

    [Serializable]
    public struct Health
    {
        public float MaxValue;
        public float Value;
    }

    [Serializable]
    public struct Attack
    {
        public float Value;
    }
    
    [Serializable]
    public struct Damage
    {
        public float Value;
    }

    [Serializable]
    public struct FireDelay
    {
        public float Limit;
        public float Value;
    }

    [Serializable]
    public struct ForceSensitive
    {
        public Vector3 Direction;
        public float ForceAmplitude;
    }

    [Serializable]
    public struct Parent
    {
        public Transform Value;
    }
    
    [Serializable]
    public struct Target
    {
        public Transform Value;
    }

    [Serializable]
    public struct RigidbodyView
    {
        public Rigidbody Value;
    }

    [Serializable]
    public struct Direction
    {
        public Vector3 Value;
    }

    [Serializable]
    public struct MoveSpeed
    {
        public float Value;
    }

    [Serializable]
    public struct TransformView
    {
        public Transform Value;
    }
    
    [Serializable]
    public struct Prefab
    {
        public Entity Value;
    }

    [Serializable]
    public struct BulletWeapon
    {
        public Transform FirePoint;
        public Entity BulletPrefab;
    }
}