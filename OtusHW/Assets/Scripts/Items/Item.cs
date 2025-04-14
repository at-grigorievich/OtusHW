using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ATG.Items
{
    [Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Item
    {
        public string Id;

        public ItemMetaData MetaData = new();
        public ItemFlags Flags;
        
        [SerializeReference]
        public IItemComponent[] Components;

        public Item() { }

        public Item(string id)
        {
            Id = id;
        }
        
        public Item(string id, params IItemComponent[] components): this(id)
        {
            Components = components;
        }

        public Item(string id, ItemFlags flags, params IItemComponent[] components) : this(id, components)
        {
            Flags = flags;
        }
        
        public Item Clone()
        {
            IItemComponent[] copiedComponents = Array.Empty<IItemComponent>();
            
            if (Components != null)
            {
                copiedComponents = new IItemComponent[Components.Length];
            
                for (var i = 0; i < copiedComponents.Length; i++)
                {
                    IItemComponent component = Components[i];
                    copiedComponents[i] = component.Clone();
                }
            }
            
            return new Item()
            {
                Id = Id,
                MetaData = MetaData.Clone(),
                Components = copiedComponents,
                Flags = Flags
            };
        }
        
        public bool TryGetComponent<T>(out T component) where T : IItemComponent
        {
            foreach (var itemComponent in Components)
            {
                if (itemComponent is T result)
                {
                    component = result;
                    return true;
                }
            }
            
            component = default(T);
            return false;
        }

        public bool TryGetComponents<T>(out IEnumerable<T> components) where T : IItemComponent
        {
            List<T> result = new List<T>();
            
            foreach (var itemComponent in Components)
            {
                if (itemComponent is T element)
                {
                    result.Add(element);
                }
            }

            components = result;

            return result.Count > 0;
        }
    }

    [Serializable]
    public class ItemMetaData
    {
        public string Name = string.Empty;
        public string Description = string.Empty;
        public Sprite Icon;

        public ItemMetaData Clone()
        {
            return new ItemMetaData()
            {
                Name = Name,
                Description = Description,
                Icon = Icon,
            };
        }
    }
}