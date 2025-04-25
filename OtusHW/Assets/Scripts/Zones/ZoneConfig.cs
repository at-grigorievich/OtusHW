using DefaultNamespace;
using UnityEngine;

namespace ATG.Zone
{
    [CreateAssetMenu(fileName = "new zone", menuName = "Configs/new zone")]
    public class ZoneConfig: ScriptableObject
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; } 
        [field: SerializeField] public int CurrentAmount { get; private set; }
        [field: SerializeField] public int MaxAmount { get; private set; }
    }
}