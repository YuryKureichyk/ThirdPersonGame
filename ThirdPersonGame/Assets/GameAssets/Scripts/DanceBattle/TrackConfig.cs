using System.Collections.Generic;
using UnityEngine;

namespace GameAssets.Scripts.DanceBattle
{
    [CreateAssetMenu(menuName = "DanceBattle/TrackConfig")]
    public class TrackConfig : ScriptableObject

    {
        public AudioClip Audio;
        public List<float> Intervals;
    }
}