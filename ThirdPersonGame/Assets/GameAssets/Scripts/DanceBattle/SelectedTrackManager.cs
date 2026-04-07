using GameAssets.Scripts.DanceBattle;
using UnityEngine;

public class SelectedTrackManager : MonoBehaviour
{
    public static SelectedTrackManager Instance;
    public TrackConfig SelectedTrack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}