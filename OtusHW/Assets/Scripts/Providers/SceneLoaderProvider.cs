using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Providers
{
    public static class SceneLoaderProvider
    {
        public static AsyncOperationHandle<SceneInstance> GetLoadAsyncOperation(string sceneName)
        {
            var env = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single, true);
            return env;
        }

        public static async Task UnloadAsync(AsyncOperationHandle<SceneInstance> releasedScene)
        {
            if (releasedScene.IsValid() == false)
            {
                throw new InvalidOperationException("The scene is not loaded yet.");
            }
            
            var unloadHandle = Addressables.UnloadSceneAsync(releasedScene);
            await unloadHandle.Task;
            
            Addressables.Release(releasedScene);
        }
    }
}