using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;

namespace ATG.Dialogues
{
    public class DialogueEdgeView: Edge
    {
        [UsedImplicitly]
        public DialogueEdgeView()
        {
            
        }
        
        /*public string GetInputId() =>
            ((DialogueNodeView)this.input.node).GetId();
        
        public string GetInputId() =>
            ((DialogueNodeView)this.output.node).GetId();

        public int GetOutputIndex() =>
            ((DialogueNodeView)this.output.node).IndexOfOutputPort(this.output);*/
    }
}