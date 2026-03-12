using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager  : MonoBehaviour
    {
        private const int _maxLevel = 1;

        public static bool IsLevelCorrect(int level)
        {
            return level > 0 && level <= _maxLevel;
        }

        public static void Load(int level)
        {
            SceneManager.LoadScene(level);
        }
    }
