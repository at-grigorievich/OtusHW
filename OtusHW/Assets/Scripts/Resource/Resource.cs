using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ATG.Resources
{
    public enum ResourceType
    {
        None = 0,
        Iron = 1,
        Gold = 2,
        Wood = 3
    }
    
    public readonly struct Resource
    {
        public readonly ResourceType Type;
        public readonly int Amount;

        public Resource(ResourceType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }

    [Serializable]
    public sealed class ResourceGenerator
    {
        [SerializeField] private ResourceType type;
        [SerializeField] private int amount;
        
        public Resource Create() => new Resource(type, amount);
    }
}