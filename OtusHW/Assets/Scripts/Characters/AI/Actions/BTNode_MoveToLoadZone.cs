using MBT;
using UnityEngine;

namespace ATG.Characters.AI.Actions
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Move To Load Zone")]
    public sealed class BTNode_MoveToLoadZone: Leaf
    {
        [SerializeField] private BotCharacterView ownerView;
        [SerializeField] private Transform targetTransform;
        
        private BotCharacterPresenter _presenter;

        private void Awake()
        {
            _presenter = ownerView.Presenter;
        }

        public override NodeResult Execute()
        {
            if(_presenter == null) return NodeResult.failure;

            float distance = _presenter.DistanceTo(targetTransform.position);

            if (distance < 0.1f)
            {
                _presenter.AnimateIdle();
                return NodeResult.success;
            }
            
            _presenter.MoveTo(targetTransform.position);
            _presenter.AnimateWalk();
            
            return NodeResult.running;
        }
    }
}