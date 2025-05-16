using MBT;
using UnityEngine;

namespace ATG.Characters.AI.Actions
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Move To Patrol Point")]
    public sealed class BTNode_MoveToNextPatrolPoint: Leaf
    {
        [SerializeField] private BotCharacterView ownerView;
        [SerializeField] private Transform[] patrolPoints;
        
        private BotCharacterPresenter _presenter;
        
        private int _curIndex = 0;
        
        private void Awake()
        {
            _presenter = ownerView.Presenter;
        }
        
        public override NodeResult Execute()
        {
            if(_presenter == null) return NodeResult.failure;
            if (patrolPoints.Length == 0) return NodeResult.failure;
            
            Transform nextTarget = patrolPoints[_curIndex];
            
            float distance = _presenter.DistanceTo(nextTarget.position);

            if (distance < 0.1f)
            {
                SwitchToNextPatrolPoint();
                
                _presenter.AnimateIdle();
                return NodeResult.success;
            }
            
            _presenter.MoveTo(nextTarget.position);
            _presenter.AnimateWalk();
            
            return NodeResult.running;
        }

        private void SwitchToNextPatrolPoint()
        {
            _curIndex = (_curIndex + 1) % patrolPoints.Length;
        }
    }
}