using GameEngine;
using SaveSystem;
using UnityEngine;
using VContainer.Unity;

public sealed class GameSceneEntryPoint : IStartable, ITickable
{
    private readonly UnitManager _unitManager;
    private readonly ResourceService _resourceService;
    
    private readonly ISaveService _saveService;

    public GameSceneEntryPoint(UnitManager unitManager, ResourceService resourceService,
        ISaveService saveService)
    {
        _unitManager = unitManager;
        _resourceService = resourceService;
        
        _saveService = saveService;
    }
    
    public void Start()
    {
        _unitManager.SetupUnits(GameObject.FindObjectsOfType<Unit>());
        _resourceService.SetResources(GameObject.FindObjectsOfType<Resource>());
        
        _saveService.Load();
    }

    public void Tick()
    {
        //for perfect future...
    }
}