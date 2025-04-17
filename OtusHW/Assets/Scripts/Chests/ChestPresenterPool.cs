using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.RealtimeChests
{
    [Serializable]
    public sealed class ChestPresenterPoolCreator
    {
        [SerializeField] private ChestPresenterCreator[] creators;

        public void Create(IContainerBuilder builder)
        {
            var data = creators.Select(creator => new KeyValuePair<string, ChestPresenter>
                (creator.Tag, creator.GetPresenter()));

            builder.Register<ChestPresenterPool>(Lifetime.Singleton)
                .WithParameter(data)
                .AsSelf().AsImplementedInterfaces();
        }
    }
    
    public sealed class ChestPresenterPool: IStartable
    {
        private readonly Dictionary<string, ChestPresenter> _pool;

        public ChestPresenterPool(IEnumerable<KeyValuePair<string,ChestPresenter>> chestsAndKeys)
        {
            _pool = new Dictionary<string, ChestPresenter>(chestsAndKeys);
        }
        
        public bool TryGetByTag(string tag, out ChestPresenter result)
        {
            return _pool.TryGetValue(tag, out result);
        }

        public bool TryGetByType(ChestType type, out ChestPresenter result)
        {
            return _pool.TryGetValue(type.ToString(), out result);
        }
        
        public void Start()
        {
            foreach (var chestPresenter in _pool.Values)
            {
                chestPresenter.Start();
            }
        }
    }
}