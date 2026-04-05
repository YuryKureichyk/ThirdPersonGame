using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace GameAssets.Scripts.DanceBattle
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private TextMeshProUGUI _statisticsText;
        [SerializeField] private TextMeshProUGUI _rankText;

        private GameCore _gameCore;
        private bool _isPaused;

        public void Init(GameCore core)
        {
            _gameCore = core;
            _pausePanel.SetActive(false);
            _resultPanel.SetActive(false);
        }

        public void TogglePause()
        {
            _isPaused = !_isPaused;
            _pausePanel.SetActive(_isPaused);

            Time.timeScale = _isPaused ? 0f : 1f;
            _gameCore.SetPauseState(_isPaused);
        }

        public void ShowResults(string report, string rank)
        {
            _statisticsText.text = report;
            _rankText.text = rank;
            _resultPanel.SetActive(true);
        }

        public void Continue() => TogglePause();

        public void ChangeTrack()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Exit()

        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            return;
#endif
            Application.Quit();
        }
    }
}