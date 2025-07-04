using System.Collections.Generic;
using GameEngine.Save;
using UnityEngine;

namespace GameEngine
{
    public static class UnitsExtension
    {
        public static string UNIT_PREFIX_TAG = "unit";
        
        public static void Setup(this Unit unit, UnitData unitData)
        {
            unit.name = unitData.Type;
            
            unit.HitPoints = unitData.HitPoints;
            
            unit.transform.position = unitData.Position;
            unit.transform.rotation = Quaternion.Euler(unitData.Rotation);
        }

        public static string GetUnitTag(this Unit unit, string postfix = "")
        {
            return $"{UNIT_PREFIX_TAG}_{unit.Type}_{postfix}";
        }
        
        public static IEnumerable<KeyValuePair<string, Unit>> GetKeyUnitPairs(this UnitManager manager)
        {
            int counter = 0;

            HashSet<KeyValuePair<string, Unit>> pairs = new();
            
            foreach (var unit in manager.GetAllUnits())
            {
                string unitKey = unit.GetUnitTag(counter.ToString());
                
                pairs.Add(new KeyValuePair<string, Unit>(unitKey, unit));
                
                counter++;
            }

            return pairs;
        }
    }
}