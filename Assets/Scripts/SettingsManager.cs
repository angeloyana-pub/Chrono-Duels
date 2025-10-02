using UnityEngine;
using System;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public float MusicVolume = 0.5f;
    public Action<float> MusicVolumeChanged;

    void Awake()
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

    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        MusicVolumeChanged?.Invoke(volume);
    }
}
