using ATG.RealtimeChests;
using ATG.RealtimeChests.Saving;
using SaveSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class SceneScope : LifetimeScope
{
    [SerializeField] private ChestPresenterPoolCreator chestPoolCreator;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<SerializableRepository>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ChestSaveLoader>(Lifetime.Singleton).As<ISaveLoader>();
        builder.Register<ISaveService, SaveLoadersService>(Lifetime.Singleton);
        chestPoolCreator.Create(builder);

        builder.RegisterEntryPoint<ChestsEntryPoint>();
    }
}