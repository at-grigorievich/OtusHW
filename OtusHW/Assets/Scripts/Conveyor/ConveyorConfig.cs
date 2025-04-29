using ATG.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "new conveyor", menuName = "Configs/new conveyor")]
public sealed class ConveyorConfig: ScriptableObject
{ 
    [field: SerializeField] public int ProceedDelay { get; set; } = 3;
    [field: SerializeField] public IntStatConfig ConvertDelayStat { get; private set; }
}