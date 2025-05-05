using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ATG.Dialogues
{
    public class DialogueDebug: MonoBehaviour
    {
        [SerializeField] private DialogueConfig dialogueConfig;
        
        private Dialogue _dialogue;

        private void Start()
        {
            _dialogue = new Dialogue(dialogueConfig);
            Debug.Log($"Message = {_dialogue.CurrentMessage}");
        }

        [Button]
        public void MoveNext(int answerIndex)
        {
            _dialogue.MoveNext(answerIndex);
            Debug.Log($"Message = {_dialogue.CurrentMessage}");
        }
    }

    public sealed class Dialogue
    {
        public string CurrentMessage => _node.message;
        public string[] CurrentChoices => _node.choices;
        
        private readonly DialogueConfig _config;
        private DialogueConfig.Node _node;

        public Dialogue(DialogueConfig config)
        {
            _config = config;
            if (_config.FindRootNode(out _node) == false)
            {
                throw new Exception("Root node not found");
            }
        }

        public bool MoveNext(int answerIndex)
        {
            return this._config.FindNextNode(_node.id, answerIndex, out _node);
        }
    }
}