using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider _volumeSlider;

    void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
    }

    void Start()
    {
        _volumeSlider.value = SettingsManager.Instance.MusicVolume;
        _volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
    }

    private void HandleVolumeChange(float volume)
    {
        SettingsManager.Instance.SetMusicVolume(volume);
    }
}
