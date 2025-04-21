using System.Collections.Generic;
using ATG.Resources;
using SaveSystem;

public sealed class HeroInventorySaveLoader: SaveLoader<HeroInventory, Dictionary<ResourceType, int>>
{
    protected override string DATA_KEY => "hero-inventory-data";
    
    public HeroInventorySaveLoader(ISerializableRepository serializableRepository, HeroInventory dataService) 
        : base(serializableRepository, dataService)
    {
    }

    protected override Dictionary<ResourceType, int> ConvertToData()
    {
        return _dataService.Resources;
    }

    protected override void SetupData(Dictionary<ResourceType, int> resourcesSet)
    {
        _dataService.SetupResources(resourcesSet);
    }
}