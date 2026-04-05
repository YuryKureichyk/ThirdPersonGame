using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace GameAssets.Scripts.DanceBattle
{
    public class TrackSelectButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        private TrackConfig _config;

        public void Setup(TrackConfig config)
        {
            _config = config;
            _nameText.text = config.name;
            _nameText.color = Color.black;
        }

        public void SelectTrack()
        {
            SelectedTrackManager.Instance.SelectedTrack = _config;
        }
    }
}