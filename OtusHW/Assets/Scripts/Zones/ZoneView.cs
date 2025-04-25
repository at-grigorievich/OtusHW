using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ATG.Zone
{
    public class ZoneView: MonoBehaviour
    {
        [SerializeField] private TMP_Text counterOutput;
        [SerializeField] private Renderer[] resourceViews;
        
        private void Awake()
        {
            UpdateAmounts(0, resourceViews.Length);   
        }
        
        [Button]
        public void UpdateAmounts(int count, int maxCount)
        {
            for (var i = 0; i < resourceViews.Length; i++)
            {
                resourceViews[i].enabled = i < count;
            }
            
            counterOutput.text = $"{count}/{maxCount}";
        }
    }
}