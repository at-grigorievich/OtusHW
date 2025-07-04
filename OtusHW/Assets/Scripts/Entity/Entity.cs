using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public sealed class Entity
    {
        private readonly Dictionary<Type, IComponent> _components = new();
        
        public Entity AddComponent(IComponent component)
        {
            Type cType = component.GetType();

            if (_components.ContainsKey(cType) == false)
            {
                _components.TryAdd(cType, component);
            }
            else
            {
                _components[cType] = component;
            }

            return this;
        }
        
        
        public void RemoveComponent<T>() where T: IComponent
        {
            Type cType = typeof(T);

            /*if (_components.ContainsKey(cType) == false)
            {
                throw new ArgumentException($"Component {cType} is not existed");
            }*/
            
            _components.Remove(cType);
        }

        public T GetComponent<T>() where T : IComponent
        {
            Type cType = typeof(T);

            if (_components.TryGetValue(cType, out IComponent result) == false)
                throw new NullReferenceException($"No component of type {cType} was found.");
            
            return (T)result;
        }
        
        public bool TryGetComponent<T>(out T component) where T: IComponent
        {
            Type cType = typeof(T);

            if (_components.TryGetValue(cType, out IComponent result) == false)
            {
                component = default;
                return false;
            }
            
            component = (T)result;
            
            return true;
        }
    }
}