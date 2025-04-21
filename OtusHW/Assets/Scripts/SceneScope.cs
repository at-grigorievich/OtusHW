using ATG.RealtimeChests;
using ATG.RealtimeChests.Saving;
using ATG.Session;
using Event_Bus.Handlers;
using SaveSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class SceneScope : LifetimeScope
{
    [SerializeField] private SessionServiceCreator sessionServiceCreator;
    [SerializeField] private ChestPresenterPoolCreator chestPoolCreator;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton);
        
        builder.Register<SerializableRepository>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ChestSaveLoader>(Lifetime.Singleton).As<ISaveLoader>();
        builder.Register<HeroInventorySaveLoader>(Lifetime.Singleton).As<ISaveLoader>();
        builder.Register<SessionsSaveLoader>(Lifetime.Singleton).As<ISaveLoader>();
        builder.Register<ISaveService, SaveLoadersService>(Lifetime.Singleton);

        builder.Register<HeroInventory>(Lifetime.Singleton).AsSelf();
        
        builder.Register<SaveGameStateHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<LoadGameStateHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<GetResourcesRewardHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        
        chestPoolCreator.Create(builder);
        sessionServiceCreator.Create(builder);

        builder.RegisterEntryPoint<ChestsEntryPoint>();
    }

    [ContextMenu("Clear prefs")]
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}