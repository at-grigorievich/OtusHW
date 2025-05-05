using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ATG.Dialogues
{
    [CreateAssetMenu(fileName = "Dialogue Config", menuName = "Dialogues/New Dialogue")]
    public class DialogueConfig: ScriptableObject
    {
        public string rootId;

        public Node[] nodes;
        public Edge[] edges;
        
        [Serializable]
        public struct Node
        {
            public string id;
            public string message;
            public string[] choices;
            public Vector2 editorPosition;
        }
        
        [Serializable]
        public struct Edge
        {
            public string inputNodeId;
            public string outputNodeId;
            public int outputIndex;
        }

        public bool FindRootNode(out Node node)
        {
            return FindNode(rootId, out node);
        }
        
        public bool FindNode(string id, out Node result)
        {
            for (int i = 0, count = nodes.Length; i < count; i++)
            {
                Node node = nodes[i];
                if (node.id == id)
                {
                    result = node;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public bool FindNextNode(string currentNodeId, int choiceIndex, out Node result)
        {
            for (int i = 0, count = nodes.Length; i < count; i++)
            {
                Edge edge = edges[i];

                if (edge.outputNodeId == currentNodeId && edge.outputIndex == choiceIndex)
                {
                    if (FindNode(edge.inputNodeId, out result)) return true;
                    return false;
                }
            }
            result = default;
            return false;
        }
    }
}