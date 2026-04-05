using UnityEngine;
using System.Collections.Generic;

namespace GameAssets.Scripts.DanceBattle
{
    public class TrackListGenerator : MonoBehaviour
    {
        [SerializeField] private TrackSelectButton _buttonPrefab;
        [SerializeField] private Transform _contentParent;

        private void Start()
        {
            GenerateList();
        }

        private void GenerateList()
        {
            foreach (Transform child in _contentParent)
                Destroy(child.gameObject);

            TrackConfig[] configs = Resources.LoadAll<TrackConfig>("Tracks");

            if (configs.Length == 0)
            {
                return;
            }

            foreach (var config in configs)
            {
                TrackSelectButton newButton = Instantiate(_buttonPrefab, _contentParent);

                newButton.Setup(config);
            }
        }
    }
}