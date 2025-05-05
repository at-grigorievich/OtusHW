using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ATG.Dialogues
{
    public class DialogueGraphView : GraphView
    {
        public DialogueGraphView()
        {
            CreateBackground();
            CreateManipulators();
            ApplyStyles();
        }

        private void CreateManipulators()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContextualMenuManipulator(OnContextMenuClicked));   
        }

        private void OnContextMenuClicked(ContextualMenuPopulateEvent menuEvent)
        {
            menuEvent.menu.AppendAction("Create Node", OnCreateNode);
        }

        private void OnCreateNode(DropdownMenuAction menuAction)
        {
            Vector2 mousePosition = menuAction.eventInfo.localMousePosition;
            Vector2 worldMousePosition = this.ChangeCoordinatesTo(parent, mousePosition);
            Vector2 fixedLocalPosition = contentViewContainer.WorldToLocal(worldMousePosition);

            var nodeId = Guid.NewGuid().ToString();
            DialogueNodeView nodeView = CreateNode(nodeId, fixedLocalPosition);

            List<Node> nodes = this.nodes.ToList();
            nodeView.SetRoot(nodes.Count == 1);
        }

        public DialogueNodeView CreateNode(string id, Vector2 position)
        {
            DialogueNodeView node = new DialogueNodeView(id);
            node.SetPosition(new Rect(position, Vector2.zero));
            
            AddElement(node);

            return node;
        }
        
        private void CreateBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }

        private void ApplyStyles()
        {
            var gridBackgroundStyleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Dialogue/Styles/DialogueGrid.uss");
            var nodeBackgroundStyleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Dialogue/Styles/DialogueNode.uss");
            
            this.styleSheets.Add(gridBackgroundStyleSheet);
            this.styleSheets.Add(nodeBackgroundStyleSheet);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> result = new();

            foreach (var port in ports)
            {
                if(port == startPort) continue;
                if(port.node == startPort.node) continue;
                if(port.direction == startPort.direction) continue;
                
                result.Add(port);
            }

            return result;
        }

        public void SetRootNode(string rootNodeId)
        {
            foreach (var node in nodes)
            {
                DialogueNodeView dialogueNode = (DialogueNodeView)node;
                dialogueNode.SetRoot(dialogueNode.GetId() == rootNodeId);
            }
        }

        public void SetRootNode(DialogueNodeView nodeView)
        {
            foreach (var node in nodes)
            {
                DialogueNodeView dialogueNode = (DialogueNodeView)node;
                dialogueNode.SetRoot(dialogueNode == nodeView);
            }
        }

        public bool TryGetRootNode(out DialogueNodeView result)
        {
            foreach (var node in nodes)
            {
                result = (DialogueNodeView)node;
                if(result.IsRoot) return true;
            }

            result = default;
            return false;
        }

        public void ResetState()
        {
            foreach (var node in nodes)
            {
                RemoveElement(node);
            }

            foreach (var edge in edges)
            {
                RemoveElement(edge );
            }
        }

        public DialogueNodeView[] GetNodes()
        {
            List<DialogueNodeView> res = new List<DialogueNodeView>();
            foreach (var node in nodes)
            {
                res.Add((DialogueNodeView)node);
            }

            return res.ToArray();
        }

        public DialogueEdgeView[] GetEdges()
        {
            var edges = this.edges.ToList();
            
            DialogueEdgeView[] res = new DialogueEdgeView[edges.Count];

            for (int i = 0; i < edges.Count; i++)
            {
                res[i] = (DialogueEdgeView)edges[i];
            }

            return res;
        }

        public void CreateEdge(DialogueNodeView outputNode, int outputIndex, DialogueNodeView inputNode)
        {
            Port outputPort = outputNode.GetOutputPort(outputIndex);
            Port inputPort = inputNode.GetInputPort();

            DialogueEdgeView edge = new DialogueEdgeView()
            {
                input = inputPort,
                output = outputPort
            };
            AddElement(edge);
        }
    }
}