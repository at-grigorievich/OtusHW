using Core;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

public class PopupSceneScope : LifetimeScope
{
    [FormerlySerializedAs("characterStorageRegister")] [FormerlySerializedAs("characterStorageRegistor")] [FormerlySerializedAs("characterStorageFactory")] [SerializeField] private CharacterStorageCreator characterStorageCreator;
    [FormerlySerializedAs("characterCardPresenterFactory")] [SerializeField] private CharacterCardCreator characterCardCreator;

    protected override void Configure(IContainerBuilder builder)
    {
        characterStorageCreator.Create(builder);
        characterCardCreator.Create(builder);
    }
}