using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using UnityEngine;

namespace GameEngine.Save
{
    public struct UnitData
    {
        public string Type;
        
        public Vector3 Position;
        public Vector3 Rotation;
        
        public int HitPoints;
        
        public UnitData(Unit unit)
        {
            Type = unit.Type;
            
            Position = unit.Position;
            Rotation = unit.Rotation;

            HitPoints = unit.HitPoints;
        }
    }
    
    public class UnitsSaveLoader: SaveLoader<UnitManager, UnitData[]> 
    {
        private readonly UnitPrefabAccessor _unitPrefabAccessor;
        
        protected override string DATA_KEY => "units-data";
        
        public UnitsSaveLoader(ISerializableRepository repository, UnitManager unitManager, 
            UnitPrefabAccessor unitPrefabAccessor): base(repository, unitManager)
        {
            
            _unitPrefabAccessor = unitPrefabAccessor;
        }

        protected override UnitData[] ConvertToData()
        {
            Unit[] units = _dataService.GetAllUnits().ToArray();
            UnitData[] unitsData = new UnitData[units.Length];

            for (int i = 0; i < units.Length; i++)
            {
                unitsData[i] = new UnitData(units[i]);
            }

            return unitsData;
        }

        protected override void SetupData(UnitData[] resourcesSet)
        {
            HashSet<Unit> existUnits = _dataService.GetAllUnits().ToHashSet();
            
            for (int i = 0; i < resourcesSet.Length; i++)
            {
                string requiredType = resourcesSet[i].Type;
                    
                Unit selectedUnit = existUnits.FirstOrDefault(u => u.Type == requiredType);

                if (selectedUnit == null)
                {
                    if (_unitPrefabAccessor.TryGetUnitByType(requiredType, out Unit prefab) == false)
                        throw new KeyNotFoundException(requiredType);
                        
                    selectedUnit = _dataService.SpawnUnit(prefab, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    existUnits.Remove(selectedUnit);
                }
                    
                selectedUnit.Setup(resourcesSet[i]);
            }
                
            if (existUnits.Count > 0)
            {
                foreach (var existUnit in existUnits)
                {
                    _dataService.DestroyUnit(existUnit);
                }
            }
        }
    }
}