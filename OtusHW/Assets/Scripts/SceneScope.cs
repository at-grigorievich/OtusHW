using ATG.Characters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneScope: LifetimeScope
    {
        [SerializeField] private ConveyorCreator conveyorCreator;
        [SerializeField] private PlayerCharacterCreator playerCreator;

        protected override void Configure(IContainerBuilder builder)
        {
            conveyorCreator.Create(builder);
            playerCreator.Create(builder);
        }
    }
}