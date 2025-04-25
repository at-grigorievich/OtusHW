using UnityEngine;

[CreateAssetMenu(fileName = "new conveyor", menuName = "Configs/new conveyor")]
public sealed class ConveyorConfig: ScriptableObject
{ 
    [field: SerializeField] public int ProduceDurationInSeconds { get; private set; }
}