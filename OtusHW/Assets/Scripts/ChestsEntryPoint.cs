using SaveSystem;
using VContainer.Unity;

public sealed class ChestsEntryPoint: IInitializable
{
    private readonly ISaveService _saveService;

    public ChestsEntryPoint(ISaveService saveService)
    {
        _saveService = saveService;
    }
    
    public void Initialize()
    {
        _saveService.Load();
    }
}