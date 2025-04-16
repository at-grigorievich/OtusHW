using SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameEngine.Save
{
    public sealed class SaveHelper : MonoBehaviour
    {
        [Inject] private ISaveService _saveLoader;
        [Inject] private IClearable _clearing;

        [Button]
        public void Save()
        {
#if UNITY_EDITOR
            _saveLoader.Save();
#endif
        }

        [Button]
        public void Load()
        {
#if UNITY_EDITOR
            _saveLoader.Load();
#endif
        }

        [Button]
        public void Clear()
        {
#if UNITY_EDITOR
                _clearing.Clear();
                Debug.Log("serialize repository cleared");
#endif
        }
    }
}