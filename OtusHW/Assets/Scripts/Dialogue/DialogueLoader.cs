using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.Dialogues
{
    public static class DialogueLoader
    {
        public static void LoadDialogue(DialogueConfig config, DialogueGraphView graphView)
        {
            graphView.ResetState();

            if (config == null)
            {
                Debug.LogWarning("Dialogue config is null !");
                return;
            }
            
            List<DialogueNodeView> nodeViews = new List<DialogueNodeView>();
            foreach (DialogueConfig.Node node in config.nodes)
            {
                DialogueNodeView nodeView = graphView.CreateNode(node.id, node.editorPosition);
                nodeView.SetMessage(node.message);

                foreach (string choice in node.choices)
                {
                    nodeView.CreateChoice(choice);
                }
                
                nodeViews.Add(nodeView);
            }

            foreach (DialogueConfig.Edge edge in config.edges)
            {
                string outputId = edge.outputNodeId;
                DialogueNodeView outputNode = nodeViews.First(it => it.GetId() == outputId);
                
                string inputId = edge.inputNodeId;
                DialogueNodeView inputNode = nodeViews.First(it => it.GetId() == inputId);
                
                int outputIndex = edge.outputIndex;
                graphView.CreateEdge(outputNode, outputIndex, inputNode);
            }
            
            graphView.SetRootNode(config.rootId);
        }
    }
}