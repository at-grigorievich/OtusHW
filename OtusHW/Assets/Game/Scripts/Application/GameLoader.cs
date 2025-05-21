using UnityEngine.SceneManagement;

namespace SampleGame
{
    public sealed class GameLoader
    {
        //TODO: Сделать через Addressables
        public void UnloadGame()
        {
            SceneManager.UnloadSceneAsync("Game");
        }
        
        //TODO: Сделать через Addressables
        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}