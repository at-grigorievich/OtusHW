using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEngine
{
    [CreateAssetMenu(fileName = "units-library", menuName = "Units/New Library")]
    public sealed class UnitLibrary: ScriptableObject
    {
        [SerializeField] private Unit[] unitsPrefab;

        public IEnumerable<KeyValuePair<string, Unit>> PrefabsByTypes =>
            unitsPrefab.Select(prefab => new KeyValuePair<string, Unit>(prefab.Type, prefab));
    }
}