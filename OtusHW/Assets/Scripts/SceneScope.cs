using ATG.RealtimeChests;
using ATG.RealtimeChests.Saving;
using Event_Bus.Handlers;
using SaveSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class SceneScope : LifetimeScope
{
    [SerializeField] private ChestPresenterPoolCreator chestPoolCreator;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton);
        builder.Register<SerializableRepository>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ChestSaveLoader>(Lifetime.Singleton).As<ISaveLoader>();
        builder.Register<ISaveService, SaveLoadersService>(Lifetime.Singleton);
        
        builder.Register<SaveGameStateHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<LoadGameStateHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        
        chestPoolCreator.Create(builder);

        builder.RegisterEntryPoint<ChestsEntryPoint>();
    }

    [ContextMenu("Clear prefs")]
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}