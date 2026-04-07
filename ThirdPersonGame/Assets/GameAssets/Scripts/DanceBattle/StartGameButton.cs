using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameAssets.Scripts.DanceBattle
{
    public class StartGameButton : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene("Dance");
        }
    }
}