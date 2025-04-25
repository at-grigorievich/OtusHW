using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneScope: LifetimeScope
    {
        [SerializeField] private ConveyorCreator conveyorCreator;

        protected override void Configure(IContainerBuilder builder)
        {
            conveyorCreator.Create(builder);
        }
    }
}