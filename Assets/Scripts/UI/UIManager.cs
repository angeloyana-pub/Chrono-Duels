using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum UIGroup
{
    Main,
    Battle
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private float _crossfadeDuration = 0.5f;

    [Header("UI Objects")]
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _battleUI;
    
    [Header("Crossfade Settings")]
    [SerializeField] private Image _crossfade;
    [SerializeField] private Color _crossfadeColor = Color.black;

    void Awake()
    {
        if (_mainUI == null) Debug.LogWarning("_mainUI is null");
        if (_battleUI == null) Debug.LogWarning("_battleUI is null");
        if (_crossfade == null) Debug.LogWarning("_crossfade is null");
    }

    void Start()
    {
        _crossfade.color = GetCrossfadeColorWithAlpha(0f);
        _crossfade.gameObject.SetActive(true);
        ShowUI(UIGroup.Main);
    }

    public void HideAll()
    {
        _mainUI.SetActive(false);
        _battleUI.SetActive(false);
    }

    public void ShowUI(UIGroup uiGroup)
    {
        HideAll();
        switch (uiGroup)
        {
            case UIGroup.Main:
                _mainUI.SetActive(true);
                break;
            case UIGroup.Battle:
                _battleUI.SetActive(true);
                break;
        }
    }

    public IEnumerator ShowCrossfade(Action callback, bool autoClose = true)
    {
        _crossfade.color = GetCrossfadeColorWithAlpha(0f);

        float timer = 0f;

        while (timer < _crossfadeDuration)
        {
            timer += Time.deltaTime;
            _crossfade.color = GetCrossfadeColorWithAlpha(Mathf.Lerp(0f, 1f, timer / _crossfadeDuration));
            yield return null;
        }

        _crossfade.color = GetCrossfadeColorWithAlpha(1f);

        callback?.Invoke();

        if (autoClose)
        {
            yield return HideCrossfade();
        }
    }

    public IEnumerator HideCrossfade()
    {
        float timer = 0f;
        while (timer < _crossfadeDuration)
        {
            timer += Time.deltaTime;
            _crossfade.color = GetCrossfadeColorWithAlpha(Mathf.Lerp(1f, 0f, timer / _crossfadeDuration));
            yield return null;
        }
        _crossfade.color = GetCrossfadeColorWithAlpha(0f);
    }
    
    private Color GetCrossfadeColorWithAlpha(float alpha)
    {
        return new Color(
            _crossfadeColor.r,
            _crossfadeColor.g,
            _crossfadeColor.b,
            alpha
        );
    }
}
