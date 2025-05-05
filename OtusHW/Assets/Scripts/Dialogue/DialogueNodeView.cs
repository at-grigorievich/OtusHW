using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ATG.Dialogues
{
    public sealed class DialogueNodeView : Node
    {
        private readonly List<DialogueChoiceView> _choices;
        private readonly string _id;

        private TextField _textMessage;
        private Port _inputPort;
        private bool _isRoot;
        
        public bool IsRoot => _isRoot;
        
        public DialogueNodeView(string id)
        {
            _id = id;
            title = "Dialogue Node";
            _choices = new List<DialogueChoiceView>();
            
            CreateTextMessage();
            CreatePortInput();
            CreateButtonAddChoice();
            RefreshExpandedState();
            ApplyStyles();
        }

        public void SetRoot(bool isRoot)
        {
            _isRoot = isRoot;
            
            style.backgroundColor = isRoot 
                ? new Color(0.92f, 0.76f, 0f)
                : new Color(0.53f, 0.53f, 0.56f);
        }

        public string GetId()
        {
            return _id;
        }

        public string GetMessage()
        {
            return _textMessage.value;
        }

        public void SetMessage(string message)
        {
            _textMessage.value = message;
        }

        public DialogueChoiceView[] GetChoices()
        {
            return _choices.ToArray();
        }
        
        public Port GetOutputPort(int index)
        {
            return _choices[index].GetPort();
        }

        public Port GetInputPort()
        {
            return _inputPort;
        }

        public int IndexOfOutputPort(Port port)
        {
            for (int i = 0; i < _choices.Count; i++)
            {
                var choice = _choices[i];
                if (choice.IsPort(port))
                    return i;
            }
            
            throw new Exception("Index of port is not found !");
        }
        
        private void CreateTextMessage()
        {
            _textMessage = new TextField
            {
                value = "Enter message",
                multiline = true
            };
            
            _textMessage.AddToClassList("dialogue-node-text-message");
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

        public void CreateChoice(string answer)
        {
            DialogueChoiceView choice = new DialogueChoiceView(answer);
            choice.OnDelete += DeleteChoice;
            _choices.Add(choice);
            outputContainer.Add(choice);
            RefreshExpandedState();
        }

        public void DeleteChoice(DialogueChoiceView choice)
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