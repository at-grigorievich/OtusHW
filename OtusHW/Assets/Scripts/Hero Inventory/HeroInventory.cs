using System;
using System.Collections.Generic;
using ATG.Resources;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class HeroInventory
{
    [field: SerializeField, ReadOnly] public Dictionary<ResourceType, int> Resources { get; private set; }

    public HeroInventory()
    {
        Resources = new Dictionary<ResourceType, int>();
    }

    public void SetupResources(Dictionary<ResourceType, int> resources)
    {
        Resources = resources; 
    }
    
    public void AddResource(ResourceType resourceType, int amount)
    {
        if (Resources.TryAdd(resourceType, amount) == false)
        {
            Resources[resourceType] += amount;
        }
    }

    public void RemoveResource(ResourceType resourceType)
    {
        //TODO...
    }
}