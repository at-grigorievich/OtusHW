using System.Collections.Generic;

namespace GameEngine
{
    public sealed class UnitPrefabAccessor
    {
        private readonly Dictionary<string, Unit> _unitsPrefab;

        public UnitPrefabAccessor(UnitLibrary library)
        {
            _unitsPrefab = new Dictionary<string, Unit>(library.PrefabsByTypes);
        }

        public bool TryGetUnitByType(string type, out Unit unit)
        {
            return _unitsPrefab.TryGetValue(type, out unit);
        }
    }
}