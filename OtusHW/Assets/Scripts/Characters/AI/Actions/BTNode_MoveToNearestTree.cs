using ATG.Resource;
using DefaultNamespace;
using MBT;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Characters.AI.Actions
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Move To Nearest Tree")]
    public sealed class BTNode_MoveToNearestTree: Leaf
    {
        [SerializeField] private BotCharacterView ownerView;
        [SerializeField] private LifetimeScope scope;

        private BotCharacterPresenter _presenter;
        private IResourceChecker _resourceChecker;

        private ResourcePresenter _nearestTree;
        
        private void Awake()
        {
            _presenter = ownerView.Presenter;
            _resourceChecker = scope.Container.Resolve<IResourceChecker>();

            if (_resourceChecker == null)
            {
                throw new VContainerException(typeof(IResourceChecker), "No IResourceChecker found");
            }
        }

        public override NodeResult Execute() 
        {
            if(_presenter == null) return NodeResult.failure;
            if (_resourceChecker == null) return NodeResult.failure;

            if (_nearestTree == null)
            {
                if (_resourceChecker.TryGetNearestResourceByType(ResourceType.Wood, _presenter.CurrentPosition,
                        out _nearestTree) == false)
                {
                    return NodeResult.failure;
                }
            }
            
            float distance = _presenter.DistanceTo(_nearestTree.Position);
            
            if (distance < 0.9f)
            {
                _nearestTree = null;
                _presenter.MoveStop();
                return NodeResult.success;
            }
            
            _presenter.MoveTo(_nearestTree.Position);
            
            return NodeResult.running;
        }
    }
}