using System;
using GameEngine;
using GameEngine.Save;
using SaveSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public sealed class UnitManagerRegister
{
    [SerializeField] private Transform unitContainer;
    [SerializeField] private UnitLibrary unitLibrary;
    
    public void Register(IContainerBuilder builder)
    {
        builder.Register<UnitManager>(Lifetime.Singleton)
            .WithParameter(unitContainer);

        builder.Register<UnitPrefabAccessor>(Lifetime.Singleton)
            .WithParameter(unitLibrary);
    }
}

[Serializable]
public sealed class ResourceServiceRegister
{
    public void Register(IContainerBuilder builder)
    {
        builder.Register<ResourceService>(Lifetime.Singleton);
    }
}

public sealed class GameSceneScope: LifetimeScope
{
    [SerializeField] private UnitManagerRegister unitManagerRegister;
    [SerializeField] private ResourceServiceRegister resourceServiceRegister;

    protected override void Configure(IContainerBuilder builder)
    {
        unitManagerRegister.Register(builder);
        resourceServiceRegister.Register(builder);

        builder.Register<SerializableRepository>(Lifetime.Singleton).AsImplementedInterfaces();

        builder.Register<UnitsSaveLoader>(Lifetime.Singleton).As<ISaveLoader>();
        builder.Register<ResourceSaveLoader>(Lifetime.Singleton).As<ISaveLoader>();
        
        builder.Register<ISaveService, SaveLoadersService>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<GameSceneEntryPoint>();
    }
}