using ATG.Characters;
using MBT;
using UnityEngine;

namespace ATG.Nodes
{
    [AddComponentMenu("")]
    [MBTNode("BotAction/Attack Target")]
    public class BTNode_AttackTarget: Leaf
    {
        public TransformReference BotTransform;
        public TransformReference TargetTransform;

        private float _nextAttack;
        
        public override NodeResult Execute()
        {
            var bot = BotTransform.Value.GetComponent<Bot>();
            
            if(bot == null) return NodeResult.failure;

            var target = TargetTransform.Value.GetComponent<Player>();
            
            if(target == null) return NodeResult.failure;

            if (_nextAttack < Time.time)
            {
                target.DoDamage(1);
                _nextAttack = Time.time + 3f;
            }

            return NodeResult.success;
        }
    }
}