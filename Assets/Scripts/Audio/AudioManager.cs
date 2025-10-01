using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _overworldClip;
    [SerializeField] private AudioClip _battleClip;
    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        PlayOverworldClip();
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
