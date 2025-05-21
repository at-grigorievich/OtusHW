using UnityEngine.SceneManagement;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        //TODO: Сделать через Addressables
        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}