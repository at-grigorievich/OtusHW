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
        [SerializeField] private ChestPresenterData[] data;

        public void Create(IContainerBuilder builder)
        {
            var presenters = this.data.Select(e => new KeyValuePair<ChestType, ChestPresenterData>
                (e.Tag, e));

            builder.Register<ChestPresenterPool>(Lifetime.Singleton)
                .WithParameter(presenters)
                .AsSelf().AsImplementedInterfaces();
        }
    }

    public sealed class ChestPresenterPool : IDisposable
    {
        private readonly EventBus _eventBus;
        
        private readonly Dictionary<ChestType, ChestPresenterData> _data;

        private readonly HashSet<Chest> _chests;
        private readonly HashSet<ChestPresenter> _presenters;
    
        
        public IReadOnlyCollection<Chest> Chests => _chests;
        public IEnumerable<ChestConfig> Configs => _data.Values.Select(e => e.Config);

        public ChestPresenterPool(IEnumerable<KeyValuePair<ChestType, ChestPresenterData>> dataByKeys, EventBus eventBus)
        {
            _data = new Dictionary<ChestType, ChestPresenterData>(dataByKeys);
            _chests = new HashSet<Chest>();
            _presenters = new HashSet<ChestPresenter>();

            _eventBus = eventBus;
        }

        public void Init()
        {
            AddChestsFromConfigIfEmpty();
            InitializePresenters();
        }

        public void Dispose()
        {
            foreach (var chestPresenter in _presenters)
            {
                chestPresenter.Dispose();
            }
        }

        public void AddChest(Chest chest)
        {
            _chests.Add(chest);
        }

        private void AddChestsFromConfigIfEmpty()
        {
            if (_chests.Count > 0) return;

            foreach (var chestConfig in Configs)
            {
                _chests.Add(chestConfig.Create());
            }
        }

        private void InitializePresenters()
        {
            foreach (var chest in _chests)
            {
                var presenter = new ChestPresenter(chest, _data[chest.Tag].View, _eventBus);

                presenter.Start();

                _presenters.Add(presenter);
            }
        }
    }
}