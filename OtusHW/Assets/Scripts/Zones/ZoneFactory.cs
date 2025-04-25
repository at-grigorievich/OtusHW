using System;
using UnityEngine;

namespace ATG.Zone
{
    [Serializable]
    public sealed class ZoneFactory
    {
        [SerializeField] private ZoneConfig config;
        [SerializeField] private ZoneView view;

        public ZonePresenter Create()
        {
            ZoneStorage storage = new(config.ResourceType, config.CurrentAmount, config.MaxAmount);
            return new(storage, view); 
        }
    }
}