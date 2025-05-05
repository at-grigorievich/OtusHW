using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace ATG.Dialogues
{
    public sealed class DialogueNodeView : Node
    {
        private readonly List<DialogueChoiceView> _choices;
        //private readonly string _id;

        private TextField _textMessage;
        private Port _inputPort;
        private bool _isRoot;

        public DialogueNodeView()
        {
            //_id = id;
            title = "Dialogue Node";
            _choices = new List<DialogueChoiceView>();
            
            CreateTextMessage();
            CreatePortInput();
            CreateButtonAddChoice();
            RefreshExpandedState();
        }

        private void CreateTextMessage()
        {
            _textMessage = new TextField
            {
                value = "Enter message",
                multiline = true
            };
            
            //_textMessage.AddToClassList("dialogue-node-text-message");
            extensionContainer.Add(_textMessage);
        }

        private void CreatePortInput()
        {
            _inputPort = Port.Create<DialogueEdgeView>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi,null);
            _inputPort.portName = "Input";
            inputContainer.Add(_inputPort);
        }
        
        private void CreateButtonAddChoice()
        {
            Button button = new Button
            {
                text = "Add Choice",
                clickable = new Clickable(OnCreateChoiceClicked)
            };
            button.AddToClassList("dialogue-node-add-choice-button");
            mainContainer.Add(button);
        }

        private void OnCreateChoiceClicked()
        {
            CreateChoice("Enter answer");
        }

        private void CreateChoice(string answer)
        {
            DialogueChoiceView choice = new DialogueChoiceView(answer);
            choice.OnDelete += DeleteChoice;
            _choices.Add(choice);
            outputContainer.Add(choice);
            RefreshExpandedState();
        }

        private void DeleteChoice(DialogueChoiceView choice)
        {
            choice.OnDelete -= DeleteChoice;
            _choices.Remove(choice);
            outputContainer.Remove(choice);
            RefreshExpandedState();
        }

        private void ApplyStyles()
        {
            mainContainer.AddToClassList("dialogue_node_main-container");
        }
    }
}