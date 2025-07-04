using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameEngine
{
    public class UnitSpawnerDebug: MonoBehaviour
    {
        [Header("Spawn prefab")]
        [SerializeField] private Unit spawnedPrefab;
        [SerializeField] private Vector3 spawnPosition;
        [SerializeField] private Vector3 spawnRotation;

        [Header("Remove instance")] 
        [SerializeField] private Unit removedInstance;
        
        [Inject] private UnitManager _manager;
        
        [Button("Spawn Unit")]
        public void Spawn()
        {
            if(spawnedPrefab == null)
                throw new System.ArgumentNullException("spawnedPrefab is null");

            _manager.SpawnUnit(spawnedPrefab, spawnPosition, Quaternion.Euler(spawnRotation));
        }

        [Button("Remove Instance")]
        public void Remove()
        {
            if(removedInstance == null) return;
            
            _manager.DestroyUnit(removedInstance);
        }
    }
}