using ATG.Characters;
using ATG.Resource;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneScope: LifetimeScope
    {
        [SerializeField] private ConveyorCreator conveyorCreator;
        [SerializeField] private BotCharacterCreator botCreator;
        [SerializeField] private ResourcePoolCreator resourcePoolCreator;

        protected override void Configure(IContainerBuilder builder)
        {
            conveyorCreator.Create(builder);
            botCreator.Create(builder);
            resourcePoolCreator.Create(builder);
        }
    }
}