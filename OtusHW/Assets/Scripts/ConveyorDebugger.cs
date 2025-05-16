using ATG.Characters;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

public class ConveyorDebugger: MonoBehaviour
{ 
    [Inject] private Conveyor _conveyor;
    [Inject] private BotCharacterPresenter _bot;
    
    [Button("Load Zone Level Up")]
    public void LoadZoneLevelUp() => _conveyor.LoadZoneLevelUp();
    
    [Button("Unload Zone Level Up")]
    public void UnloadZoneLevelUp() => _conveyor.UnloadZoneLevelUp();
    
    [Button("Convert time level up")]
    public void ProcessorLevelUp() => _conveyor.ProcessorLevelUp();

    [Button("Add amount to load zone")]
    public void AddAmount(int amount) => _conveyor.TryAddAmount(amount);
}