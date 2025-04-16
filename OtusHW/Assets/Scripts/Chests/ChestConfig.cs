using System;
using ATG.DateTimers;
using UnityEngine;

namespace ATG.RealtimeChests
{
    [CreateAssetMenu(menuName = "Configs/New Chest Config", fileName = "New Chest Config")]
    public sealed class ChestConfig: ScriptableObject
    {
        [SerializeField] private ChestType tag;
        [Header("Hours:Minutes:Seconds")]
        [SerializeField] private Vector3Int cooldown;
        
        public Chest Create() =>
            new Chest(tag, new CooldownTimer(Vector3IntToTimeSpan(cooldown)));
        
        private static TimeSpan Vector3IntToTimeSpan(Vector3Int cooldown) =>
            new TimeSpan(cooldown.x, cooldown.y, cooldown.z);
    }
}