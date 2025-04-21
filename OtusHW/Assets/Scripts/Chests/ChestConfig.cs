using System;
using ATG.DateTimers;
using ATG.RealtimeChests.Reward;
using UnityEngine;

namespace ATG.RealtimeChests
{
    public sealed class ChestConfig: ScriptableObject
    {
        [SerializeField] private ChestType tag;
        [SerializeField] private ChestMetaDataCreator meta;
        [SerializeField] private RewardConfig reward;
        [Header("Hours:Minutes:Seconds")]
        [SerializeField] private Vector3Int cooldown;
        
        public ChestType Tag => tag;
        public ChestMetaDataCreator MetaCreator => meta;
        public RewardConfig Reward => reward;
        public Vector3Int Cooldown => cooldown;
        
        public Chest Create() =>
            new (tag, meta.Create(), reward, new CooldownTimer(cooldown.ToTimeSpan()));
        
        private static TimeSpan Vector3IntToTimeSpan(Vector3Int cooldown) =>
            new TimeSpan(cooldown.x, cooldown.y, cooldown.z);
    }
}