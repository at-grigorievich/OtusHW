using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Conveyor
{
    public class ConveyorView: MonoBehaviour
    {
        [SerializeField] private Renderer woodBlank;
        [SerializeField] private Image convertProgressOutput;

        private void Awake() => StopConvert();

        public void StartConvert()
        {
            woodBlank.enabled = true;
            convertProgressOutput.enabled = true;
        }

        public void StopConvert()
        {
            woodBlank.enabled = false;   
            convertProgressOutput.enabled = false;
        }

        public void UpdateProgress(float rate)
        {
            convertProgressOutput.fillAmount = rate;
        }
    }
}