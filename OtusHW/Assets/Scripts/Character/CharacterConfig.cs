using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Characters/New Character", order = 1)]
    public class CharacterConfig: ScriptableObject
    {
        [SerializeField] private string characterName;
        [SerializeField] private Sprite characterIcon;
        [SerializeField, TextArea] private string characterDescription;
        [SerializeField] private CharacterLevelConfig[] characterSpecsByLevel;

        public string Name => characterName;
        public string Description => characterDescription;
        public Sprite Icon => characterIcon;

        public IEnumerable<KeyValuePair<int, int>> ExperienceByLevel =>
            characterSpecsByLevel
                .Select(config => new KeyValuePair<int, int>(config.Level, config.MaxExperience))
                .OrderBy(pair => pair.Key);
        
        public IEnumerable<KeyValuePair<int, ICharacterSpecs>> SpecsByLevel =>
            characterSpecsByLevel
                .Select(config => new KeyValuePair<int, ICharacterSpecs>(config.Level, config.CharacterSpecsData))
                .OrderBy(pair => pair.Key);
    }
}