using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class PauseScreen : MonoBehaviour
    {
        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button exitButton;

        private MenuLoader menuLoader;

        [Inject]
        public void Construct(MenuLoader menuLoader, GameLoader gameLoader)
        {
            this.menuLoader = menuLoader;
            this.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            this.resumeButton.onClick.AddListener(this.Hide);
            this.exitButton.onClick.AddListener(this.menuLoader.LoadMenu);
        }

        private void OnDisable()
        {
            this.resumeButton.onClick.RemoveListener(this.Hide);
            this.exitButton.onClick.RemoveListener(this.menuLoader.LoadMenu);

            Time.timeScale = 1f;
        }

        public void Show() => SetPause(true);

        public void Hide() => SetPause(false);

        private void SetPause(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
            this.gameObject.SetActive(isPaused);
        }
    }
}