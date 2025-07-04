using System;
using Core.Model;

namespace Core
{
    public sealed class CharacterViewModel
    {
        private CharacterModel _selectedModel;
        
        public readonly AboutCharacterViewModel CharacterDescription = new();
        public readonly LevelCharacterViewModel CharacterLevel = new();
        public readonly SpecSetViewModel SpecSet = new();
        
        public bool AvailableLevelUp => CharacterLevel.AvailableLevelUp;
        
        public event Action<bool> OnLevelUpAllowed;
        
        public void ChangeModel(CharacterModel newModel)
        {
            if(ReferenceEquals(_selectedModel, newModel) == true) return;
            
            _selectedModel = newModel;
            
            CharacterDescription.Setup(_selectedModel);
            CharacterLevel.Setup(_selectedModel);
            SpecSet.Setup(_selectedModel.SpecByLevels[_selectedModel.CurrentLevel]);
            
            OnLevelUpAllowed?.Invoke(CharacterLevel.AvailableLevelUp);
        }

        public void LevelUp()
        {
            CharacterLevel.LevelUp();
            SpecSet.Setup(_selectedModel.SpecByLevels[_selectedModel.CurrentLevel]);
            
            OnLevelUpAllowed?.Invoke(CharacterLevel.AvailableLevelUp);
        }

        public void AddExperience(int exp)
        {
            CharacterLevel.AddExperience(exp);
            OnLevelUpAllowed?.Invoke(CharacterLevel.AvailableLevelUp);
        }
    }
}