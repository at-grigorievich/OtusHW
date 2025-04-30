using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace ATG.Dialogues
{
    public sealed class DialogueNodeView : Node
    {
        //private readonly string _id;

        private TextField _textMessage;
        private Port _inputPort;
        private bool _isRoot;

        public DialogueNodeView()
        {
            //_id = id;
            title = "Dialogue Node";

            CreateTextMessage();
            CreatePortInput();
            CreateButtonAddChoice();
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
            _inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi,null);
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
            //button.AddToClassList();
            mainContainer.Add(button);
        }

        private void OnCreateChoiceClicked()
        {
            CreateChoice("Enter answer");
        }

        private void CreateChoice(string answer)
        {
            DialogueChoiceView choice = new DialogueChoiceView(answer);
            choice.OnDelete + DeleteChoice;
            _choices.Add(choice);
            outputContainer.Add(choice);
            RefreshExpandedState();
        }
    }
}