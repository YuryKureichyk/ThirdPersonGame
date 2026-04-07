using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using GameAssets.Scripts.DanceBattle;

[CustomEditor(typeof(TrackConfig))]
public class TrackConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TrackConfig config = (TrackConfig)target;

        if (GUILayout.Button("Generate intervals (FFT)"))
        {
            config.Intervals = AnalyzeAudio(config.Audio);
            EditorUtility.SetDirty(config);
            AssetDatabase.SaveAssets();
        }
    }

    private List<float> AnalyzeAudio(AudioClip clip)
    {
        List<float> times = new List<float>();
        if (clip == null) return times;

        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        int windowSize = 1024;
        float threshold = 0.2f; 
        float lastPeakTime = -1f;

        for (int i = 0; i < samples.Length - windowSize; i += windowSize)
        {
            float energy = 0;
            for (int j = 0; j < windowSize; j++) energy += Mathf.Abs(samples[i + j]);
            energy /= windowSize;

            if (energy > threshold)
            {
                float currentTime = (float)i / clip.frequency;
                if (currentTime - lastPeakTime > 0.8f) 
                {
                    times.Add(currentTime);
                    lastPeakTime = currentTime;
                }
            }
        }
        return times;
    }
}