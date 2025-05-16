using ATG.Characters;
using ATG.Resource;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneScope: LifetimeScope
    {
        [SerializeField] private ConveyorCreator conveyorCreator;
        [SerializeField] private PlayerCharacterCreator playerCreator;
        [SerializeField] private ResourcePoolCreator resourcePoolCreator;

        protected override void Configure(IContainerBuilder builder)
        {
            conveyorCreator.Create(builder);
            playerCreator.Create(builder);
            resourcePoolCreator.Create(builder);
        }
    }
}