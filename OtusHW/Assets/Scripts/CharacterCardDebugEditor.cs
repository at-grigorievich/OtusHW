using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterCardDebug))]
public class CharacterCardDebugEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharacterCardDebug script = (CharacterCardDebug)target;

        if (GUILayout.Button("Show Next Character Card"))
        {
            script.ShowNextCharacterCard();
        }

        if (GUILayout.Button("Open Popup"))
        {
            script.OpenPopup();
        }
        
        if (GUILayout.Button("Add experience"))
        {
            script.AddExperienceToCurrentCharacter();
        }
    }
}