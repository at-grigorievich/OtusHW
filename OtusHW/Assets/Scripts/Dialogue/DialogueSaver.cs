using UnityEditor;
using UnityEngine;

namespace ATG.Dialogues
{
    public static class DialogueSaver
    {
        public static void SaveDialogueAsNew(DialogueGraphView graphView, out DialogueConfig config)
        {
            string path = EditorUtility.SaveFilePanelInProject("Save file", "Dialogue", "asset", "");
            config = ScriptableObject.CreateInstance<DialogueConfig>();
            AssetDatabase.CreateAsset(config, path);

            SaveDialogue(graphView, config);
        }

        public static void SaveDialogue(DialogueGraphView graphView, DialogueConfig config)
        {
            config.nodes = ConvertNodes(graphView);
            config.edges = ConvertEdges(graphView);

            if (graphView.TryGetRootNode(out DialogueNodeView rootNode))
            {
                config.rootId = rootNode.GetId();
            }
            
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static DialogueConfig.Node[] ConvertNodes(DialogueGraphView graphView)
        {
            DialogueNodeView[] nodeViews = graphView.GetNodes();
            
            int count = nodeViews.Length;
            
            DialogueConfig.Node[] nodes = new DialogueConfig.Node[count];

            for (int i = 0; i < count; i++)
            {
                DialogueNodeView nodeView = nodeViews[i];
                DialogueConfig.Node node = new DialogueConfig.Node
                {
                    id = nodeView.GetId(),
                    message = nodeView.GetMessage(),
                    editorPosition = nodeView.GetPosition().position,
                    choices = ConvertChoices(nodeView),
                };
                
                nodes[i] = node;
            }
            
            return nodes;
        }

        private static string[] ConvertChoices(DialogueNodeView nodeView)
        {
            DialogueChoiceView[] choiceViews = nodeView.GetChoices();
            int count = choiceViews.Length;
            
            string[] choices = new string[count];

            for (int i = 0; i < count; i++)
            {
                DialogueChoiceView choiceView = choiceViews[i];
                string choice = choiceView.GetMessage();
                choices[i] = choice;    
            }

            return choices;
        }

        private static DialogueConfig.Edge[] ConvertEdges(DialogueGraphView graphView)
        {
            DialogueEdgeView[] edgeViews = graphView.GetEdges();
            int count = edgeViews.Length;
            
            DialogueConfig.Edge[] edges = new DialogueConfig.Edge[count];

            for (int i = 0; i < count; i++)
            {
                DialogueEdgeView edgeView = edgeViews[i];
                DialogueConfig.Edge edge = new DialogueConfig.Edge
                {
                    inputNodeId = edgeView.GetInputId(),
                    outputNodeId = edgeView.GetOutputId(),
                    outputIndex = edgeView.GetOutputIndex()
                };
                
                edges[i] = edge;
            }

            return edges;
        }
    }
}