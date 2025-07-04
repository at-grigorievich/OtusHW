using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "CharacterSpecsData", menuName = "Characters/New Level Specs Data")]
    public class CharacterLevelConfig: ScriptableObject
    {
        [SerializeField] private int level;
        [SerializeField] private int maxExperience;
        [SerializeField] private CharacterSpecsDataFactory characterSpecsDataFactory;

        public int Level => level;
        public int MaxExperience => maxExperience;
        
        public ICharacterSpecs CharacterSpecsData => characterSpecsDataFactory.Get();
    }
}