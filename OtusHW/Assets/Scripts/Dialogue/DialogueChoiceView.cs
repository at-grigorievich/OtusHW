using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace ATG.Dialogues
{
    public sealed class DialogueChoiceView: VisualElement
    {
        public event Action<DialogueChoiceView> OnDelete;

        private TextField textAnswer;
        private Port port;

        public DialogueChoiceView(string answer)
        {
            CreateButtonDelete();
            CreateTextAnswer(answer);
            CreatePortOutput();
            this.style.flexDirection = FlexDirection.Row;
        }

        private void CreateButtonDelete()
        {
            Button btn = new Button()
            {
                text = "X",
                clickable = new Clickable(this.OnDeleteClicked)
            };
            //btn.AddToClassList("dialogue-node-remove-choice-button");
            this.Add(btn);
        }

        private void CreateTextAnswer(string answer)
        {
            this.textAnswer = new TextField
            {
                
            }
        }
        
        private void OnDeleteClicked(EventBase obj)
        {
            throw new NotImplementedException();
        }
    }
}