using System;
using UnityEngine;

namespace ATG.RealtimeChests
{
    [Serializable]
    public sealed class ChestMetaDataCreator
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;

        public ChestMetaData Create() => new(name, icon);
    }
    public readonly struct ChestMetaData
    {
        public readonly string Name;
        public readonly Sprite Sprite;

        public ChestMetaData(string name,Sprite sprite)
        {
            Name = name;
            Sprite = sprite;
        }
    }
}