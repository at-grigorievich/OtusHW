using System.Threading.Tasks;
using Providers;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using VContainer;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        private readonly GameLoader _gameLoader;
        
        [Inject]
        public MenuLoader(GameLoader gameLoader)
        {
            _gameLoader = gameLoader;
        }
        
        public void LoadMenu()
        {
            LoadMenuAsync();
        }

        private async Task LoadMenuAsync()
        {
            AsyncOperationHandle<SceneInstance> handle = SceneLoaderProvider.GetLoadAsyncOperation("Menu");
            SceneInstance instance = await handle.Task;
            instance.ActivateAsync();
        }
    }
}