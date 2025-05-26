using UnityEngine;
using Zenject;

namespace SampleGame
{
    public class BootSceneEntryPoint: MonoBehaviour
    {
        [Inject] private MenuLoader _menuLoader;

        private void Awake()
        {
            _menuLoader.LoadMenu();
        }
    }
}