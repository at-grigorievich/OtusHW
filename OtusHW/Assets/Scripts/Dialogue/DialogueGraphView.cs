using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

            //List<Node> nodes = this.nodes.ToList();
            //nodeView.SetRoot(nodes.Count == 1);
        }

        public DialogueNodeView CreateNode(string id, Vector2 position)
        {
            DialogueNodeView node = new DialogueNodeView();
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
    }
}