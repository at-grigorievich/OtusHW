using System;
using UI;
using UnityEngine;

namespace ATG.RealtimeChests
{
    [Serializable]
    public sealed class ChestPresenterCreator
    {
        [SerializeField] private ChestConfig config;
        [SerializeField] private ChestView view;
        
        public string Tag => config.Tag;
        public ChestPresenter GetPresenter() => new (config, view);
    }
}