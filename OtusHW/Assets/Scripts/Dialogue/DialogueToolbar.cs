using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ATG.Dialogues
{
    public sealed class DialogueToolbar: Toolbar
    {
        internal DialogueToolbar(DialogueGraphView graphView)
        {
            ObjectField configField = CreateConfigField();
            Button loadButton = CreateButtonLoad(graphView, configField);
            Button saveButton = CreateButtonSave(graphView, configField);
            Button resetButton = CreateButtonReset(graphView, configField);   
            
            this.Add(configField);
            this.Add(loadButton);
            this.Add(saveButton);
            this.Add(resetButton);
        }

        private static ObjectField CreateConfigField()
        {
            return new ObjectField("Selected Dialogue")
            {
                objectType = typeof(DialogueConfig),
                allowSceneObjects = false
            };
        }

        private static Button CreateButtonSave(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Save config",
                clickable = new Clickable(() =>
                {
                    DialogueConfig config = configField.value as DialogueConfig;

                    if (config != null)
                    {
                        DialogueSaver.SaveDialogue(graphView, config);
                    }
                    else
                    {
                        DialogueSaver.SaveDialogueAsNew(graphView, out config);
                        configField.value = config;
                    }
                })
            };
        }

        private static Button CreateButtonLoad(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Load config",
                clickable = new Clickable(() =>
                {
                    DialogueLoader.LoadDialogue(configField.value as DialogueConfig, graphView);
                })
            };
        }

        private static Button CreateButtonReset(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Reset dialogue",
                clickable = new Clickable(() =>
                {
                    graphView.ResetState();
                    configField.value = null;
                })
            };
        }
    }
}