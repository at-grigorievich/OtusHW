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
            this.AddManipulator(new ContextualMenuManipulator(OnContextMenuClicked));
        }

        private void OnContextMenuClicked(ContextualMenuPopulateEvent menuEvent)
        {
            menuEvent.menu.AppendAction("Create Node", OnCreateNode);
        }

        private void OnCreateNode(DropdownMenuAction obj)
        {
            var nodeId = "";
            var fixedLocalPosition = Vector2.zero;
            
            DialogueNodeView node = CreateNode(nodeId, fixedLocalPosition);
        }

        public DialogueNodeView CreateNode(string id, Vector2 position)
        {
            DialogueNodeView node = new DialogueNodeView();
            //node.SetPosition(new Rect(position, Vector2.zero));
            
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

            this.styleSheets.Add(gridBackgroundStyleSheet);
        }
    }
    
}