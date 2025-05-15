using ATG.Characters;
using MBT;
using UnityEngine;

namespace ATG.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "BotAction/Patrol Node")]
    public class PatrollingNode: Leaf
    {
        public TransformReference BotTransform;
        public override NodeResult Execute()
        {
            var bot = BotTransform.Value.GetComponent<Bot>();
            if (bot == null)
            {
                return NodeResult.failure;
            }
            
            bot.PatrolData.UpdateFromNode();
            
            return NodeResult.success;
        }
    }
}