using ATG.RealtimeChests;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class SceneScope : LifetimeScope
{
    [SerializeField] private ChestPresenterPoolCreator chestPoolCreator;

    protected override void Configure(IContainerBuilder builder)
    {
        chestPoolCreator.Create(builder);
    }
}