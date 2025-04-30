using UnityEditor;

namespace ATG.Dialogues
{
    public static class DialogueMenu
    {
        [MenuItem("Window/Dialogues")]
        public static void OpenWindow()
        {
            EditorWindow.GetWindow<DialogueWindow>("Dialogue");
        }
    }
}