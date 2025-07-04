using System;
using Core.Model;
using UnityEngine;
using VContainer;

namespace Core
{
    [Serializable]
    public sealed class CharacterStorageCreator
    {
        [SerializeField] private CharacterConfig[] configs;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<CharactersStorage>(Lifetime.Singleton)
                .WithParameter(configs);
        }
    }
    
    public sealed class CharactersStorage
    {
        private readonly CharacterModel[] _characters;

        private int _currentSelectedIndex;
        
        public CharactersStorage(CharacterConfig[] configs)
        {
            _characters = new CharacterModel[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                _characters[i] = new CharacterModel(configs[i]);
            }

            _currentSelectedIndex = -1;
        }

        public CharacterModel GetNext()
        {
            UpdateSelectedIndex();
            
            return _characters[_currentSelectedIndex];
        }

        private void UpdateSelectedIndex()
        {
            if (++_currentSelectedIndex >= _characters.Length)
                _currentSelectedIndex = 0;
        }
    }
}