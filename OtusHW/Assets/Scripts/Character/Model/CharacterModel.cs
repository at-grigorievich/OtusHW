using System.Collections.Generic;
using UnityEngine;

namespace Core.Model
{
    public interface IDescriptive
    {
        string Nick { get; }
        string Description { get; }
        
        Sprite Avatar { get; }
    }

    public interface ILevelUpgradeable
    {
        int CurrentLevel { get; }
        int CurrentExperience { get; }
        
        Dictionary<int, int> ExpByLevels { get; }
        
        void LevelUp(int nextLevel);
        void AddExperience(int experience);
    }

    public interface ISpecUpgradeable
    {
        Dictionary<int, ICharacterSpecs> SpecByLevels { get; }
    }
    
    public class CharacterModel: IDescriptive, ILevelUpgradeable, ISpecUpgradeable
    {
        public string Nick { get; private set; }
        public Sprite Avatar { get; private set; }
        public string Description { get; private set; }
        
        public int CurrentLevel { get; private set; }
        public int CurrentExperience { get; private set; }

        public Dictionary<int, int> ExpByLevels { get; private set; }
        public Dictionary<int, ICharacterSpecs> SpecByLevels { get; private set; }

        public CharacterModel(CharacterConfig config)
        {
            Nick = config.Name;
            Avatar = config.Icon;
            Description = config.Description;

            CurrentLevel = 1;
            CurrentExperience = 0;
            
            ExpByLevels = new Dictionary<int, int>(config.ExperienceByLevel);
            SpecByLevels = new Dictionary<int, ICharacterSpecs>(config.SpecsByLevel);
        }

        public void LevelUp(int nextLevel) => CurrentLevel = nextLevel;
        public void AddExperience(int experience) => CurrentExperience += experience;
    }
}