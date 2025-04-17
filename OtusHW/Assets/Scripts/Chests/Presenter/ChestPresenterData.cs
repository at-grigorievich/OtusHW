using System;
using UI;
using UnityEngine;

namespace ATG.RealtimeChests
{
    [Serializable]
    public sealed class ChestPresenterData
    {
        [SerializeField] private ChestConfig config;
        [SerializeField] private ChestView view;
        
        public ChestType Tag => config.Tag;
        
        public ChestConfig Config => config;
        public ChestView View => view;
    }
}