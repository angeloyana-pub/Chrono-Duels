using UnityEditor.SettingsManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _overworldClip;
    [SerializeField] private AudioClip _battleClip;
    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        _audioSource.volume = SettingsManager.Instance.MusicVolume;
        SettingsManager.Instance.MusicVolumeChanged += HandleVolumeChange;
        PlayOverworldClip();
    }

    void OnDestroy()
    {
        SettingsManager.Instance.MusicVolumeChanged -= HandleVolumeChange;
    }

    private void HandleVolumeChange(float volume)
    {
        _audioSource.volume = volume;
    }

    public void PlayOverworldClip()
    {
        // _audioSource.Pause();
        _audioSource.clip = _overworldClip;
        _audioSource.Play();
    }

    public void PlayBattleClip()
    {
        // _audioSource.Pause();
        _audioSource.clip = _battleClip;
        _audioSource.Play();
    }
}
