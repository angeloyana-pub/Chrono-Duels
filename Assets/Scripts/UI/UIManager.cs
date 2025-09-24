using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float _crossfadeDuration = 0.5f;

    [Header("UI Objects")]
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _battleUI;
    [SerializeField] private Image _crossfade;

    void Awake()
    {
        if (_mainUI == null) Debug.LogWarning("_mainUI is null");
        if (_battleUI == null) Debug.LogWarning("_battleUI is null");
        if (_crossfade == null) Debug.LogWarning("_crossfade is null");
    }

    void Start()
    {
        _crossfade.color = new Color(0f, 0f, 0f, 0f);
        _crossfade.gameObject.SetActive(true);
        ShowMainUI();
    }

    public void HideAll()
    {
        _mainUI.SetActive(false);
        _battleUI.SetActive(false);
    }

    public void ShowMainUI()
    {
        HideAll();
        _mainUI.SetActive(true);
    }

    public void ShowBattleUI()
    {
        HideAll();
        _battleUI.SetActive(true);
    }

    public IEnumerator ShowCrossfade(Action callback, bool autoClose = true)
    {
        _crossfade.color = new Color(0f, 0f, 0f, 0f);

        float timer = 0f;

        while (timer < _crossfadeDuration)
        {
            timer += Time.deltaTime;
            _crossfade.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, timer / _crossfadeDuration));
            yield return null;
        }

        _crossfade.color = new Color(0f, 0f, 0f, 1f);

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
            _crossfade.color = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, timer / _crossfadeDuration));
            yield return null;
        }
        _crossfade.color = new Color(0f, 0f, 0f, 0f);
    }
}
