using System;
using System.Collections.Generic;
using System.Linq;
using ATG.Observable;
using UnityEngine;

namespace ATG.Stats
{

    [Serializable]
    public sealed class IntStatUpdateCreator : StatUpdaterCreator<int> {}
    
    public abstract class StatUpdaterCreator<T>
    {
        [SerializeField, Range(1,100)] private int requireLevel;
        [SerializeField] private T value;
        
        public int RequiredLevel => requireLevel;
        
        public StatUpdater<T> Create() => new(requireLevel, value);
    }

    public class StatUpdater<T>
    {
        public readonly int RequireLevel;
        public readonly T Value;

        public StatUpdater(int requireLevel, T value)
        {
            RequireLevel = requireLevel;
            Value = value;
        }
    }

    public class Stat<T>
    {
        public readonly Dictionary<int, StatUpdater<T>> _updatesByLevel;
        public readonly ObservableVar<int> CurrentLevel;
        public readonly ObservableVar<bool> IsMaxLevel;
        public T CurrentValue;
        
        public Stat(IEnumerable<KeyValuePair<int,StatUpdater<T>>> updates)
        {
            _updatesByLevel = new Dictionary<int, StatUpdater<T>>(updates);
            CurrentLevel = new ObservableVar<int>(1);
            IsMaxLevel = new ObservableVar<bool>(false);
            
            ChangeLevel(1);
        }

        public void ChangeLevel(int level)
        {
            int maxLevel = _updatesByLevel.Max(upd => upd.Key);

            bool isMax = level >= maxLevel;
            
            if(level >= maxLevel) level = maxLevel;

            IsMaxLevel.Value = isMax;
            
            CurrentValue = _updatesByLevel[level].Value;
            CurrentLevel.Value = level;
        }
    }
    
    [CreateAssetMenu(fileName = "new int stat", menuName = "Configs/new int stat config")]
    public class IntStatConfig: ScriptableObject
    {
        [SerializeField] private IntStatUpdateCreator[] updatesByLevel;

        public Stat<int> Create()
        {
            IEnumerable<KeyValuePair<int, StatUpdater<int>>> updts = updatesByLevel
                .Select(upd => new KeyValuePair<int,StatUpdater<int>>(upd.RequiredLevel, upd.Create()))
                .OrderBy(kvp => kvp.Key);
            
            return new Stat<int>(updts);
        }
    }
}