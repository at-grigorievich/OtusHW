using System;
using ATG.Zone;
using DefaultNamespace.Conveyor;
using UnityEngine;
using VContainer;

[Serializable]
public sealed class ConveyorCreator
{
    [SerializeField] private ConveyorView view;
    [SerializeField] private ConveyorConfig config;
    [SerializeField] private ZoneFactory loadZoneFactory;
    [SerializeField] private ZoneFactory unloadZoneFactory;

    public void Create(IContainerBuilder builder)
    {
        ZonePresenter loadZone = loadZoneFactory.Create();
        ZonePresenter unloadZone = unloadZoneFactory.Create();

        builder.Register<Conveyor>(Lifetime.Singleton)
            .WithParameter(view)
            .WithParameter("loadZone", loadZone)
            .WithParameter("unloadZone", unloadZone)
            .WithParameter(config.ProduceDurationInSeconds)
            .AsSelf().AsImplementedInterfaces();
    }
}