using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public sealed class ChestViewPool: MonoBehaviour
    {
        private Queue<ChestView> _pool;

        private void Awake()
        {
            var chestViews = GetComponentsInChildren<ChestView>();
            
            if(chestViews.Length <= 0)
                throw new NullReferenceException("No ChestViews found");
            
            _pool = new Queue<ChestView>(chestViews);
        }

        public bool TryGetChestView(out ChestView chestView)
        {
            return _pool.TryDequeue(out chestView);
        }
    }
}