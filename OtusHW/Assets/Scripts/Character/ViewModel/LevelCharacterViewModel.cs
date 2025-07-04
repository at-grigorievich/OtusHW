using System;
using System.Collections.Generic;
using Core.Model;
using UnityEngine;

namespace Core
{
    public sealed class LevelCharacterViewModel
    {
        private ILevelUpgradeable _levelUpgradeable;
        
        public int CurrentLevel => _levelUpgradeable?.CurrentLevel ?? 1;
        public int CurrentExperience => _levelUpgradeable?.CurrentExperience ?? 0;

        public float CurrentMaxExperience => _levelUpgradeable?.ExpByLevels[CurrentLevel] ?? 0;
        
        public float PreviousMaxExperience => CurrentLevel > 1
            ? (_levelUpgradeable?.ExpByLevels[CurrentLevel- 1] ?? 0)
            : 0f;
        
        public bool AvailableLevelUp => CurrentLevel < (_levelUpgradeable?.ExpByLevels.Count ?? -1)
                                        && CurrentExperience >= CurrentMaxExperience;

        public event Action OnChanged;
        public event Action OnExperienceChanged;
        public event Action OnLevelChanged;

        public void Setup(ILevelUpgradeable levelUpgradeable)
        {
            _levelUpgradeable = levelUpgradeable;
            
            OnChanged?.Invoke();
        }
        
        public void AddExperience(int experience)
        {
            if(_levelUpgradeable == null) return;
            
            _levelUpgradeable?.AddExperience(experience);
            
            OnExperienceChanged?.Invoke();
        }
        
        public void LevelUp()
        {
            if(_levelUpgradeable == null) return;

            int maxLevel = _levelUpgradeable.ExpByLevels.Count;
            int nextLevel = Mathf.Clamp(CurrentLevel + 1, 1, maxLevel);
            
            _levelUpgradeable.LevelUp(nextLevel);
            
            OnLevelChanged?.Invoke();
        }
    }
}