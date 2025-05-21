using SampleGame;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class MenuScreen : MonoBehaviour
    {
        [SerializeField]
        private Button startButton;

        [SerializeField]
        private Button exitButton;
        
        private ApplicationExiter applicationExiter;
        private GameLoader gameLoader;
        
        [Inject]
        public void Construct(ApplicationExiter applicationFinisher, GameLoader gameLoader)
        {
            this.gameLoader = gameLoader;
            this.applicationExiter = applicationFinisher;
        }

        private void OnEnable()
        {
            this.startButton.onClick.AddListener(this.gameLoader.LoadGame);
            this.exitButton.onClick.AddListener(this.applicationExiter.ExitApp);
        }

        private void OnDisable()
        {
            this.startButton.onClick.RemoveListener(this.gameLoader.LoadGame);
            this.exitButton.onClick.RemoveListener(this.applicationExiter.ExitApp);
        }
    }
}