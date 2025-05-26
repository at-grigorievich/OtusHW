using System.Threading.Tasks;
using Providers;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private AsyncOperationHandle<SceneInstance> _loadedSceneOperation;

        public bool AlreadyLoaded => _loadedSceneOperation.IsValid();
        
        public void UnloadGame() => UnloadGameAsync();
        public void LoadGame() => LoadGameAsync();
        
        private async Task LoadGameAsync()
        {
            _loadedSceneOperation = SceneLoaderProvider.GetLoadAsyncOperation("Game");
            SceneInstance instance = await _loadedSceneOperation.Task;
            instance.ActivateAsync();

            SceneContext sceneContext = FindSceneContext(instance.Scene);
            sceneContext.Run();

        }

        private async Task UnloadGameAsync()
        {
            await SceneLoaderProvider.UnloadAsync(_loadedSceneOperation);
            _loadedSceneOperation = default;
        }
        
        private SceneContext FindSceneContext(Scene scene)
        {
            foreach (var root in scene.GetRootGameObjects())
            {
                var ctx = root.GetComponentInChildren<SceneContext>();
                if (ctx != null)
                    return ctx;
            }
            return null;
        }
    }
}