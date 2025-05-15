using ATG.Characters;
using MBT;
using UnityEngine;

namespace ATG.Nodes
{
    [AddComponentMenu("")]
    [MBTNode("BotAction/Move To Target")]
    public class BTNode_MoveToTarget: Leaf
    {
        public TransformReference BotTransform;
        public TransformReference TargetTransform;
        
        public override NodeResult Execute()
        {
            var bot = BotTransform.Value.GetComponent<Player>();
            
            if(bot == null) return NodeResult.failure;

            var target = TargetTransform.Value.GetComponent<Player>();
            
            if(target == null) return NodeResult.failure;

            var dist = (target.transform.position - bot.transform.position).sqrMagnitude;
            if(dist < 1f) return NodeResult.success;
            
            bot.DoMove(target.transform);
            return NodeResult.running;
        }
    }
}