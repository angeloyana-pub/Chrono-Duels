using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChronoButtonController : MonoBehaviour
{
    [SerializeField] private Image _avatarImage;
    private Button _button;

    private UnityAction _handleClick;

    void Awake()
    {
        if (_avatarImage == null) Debug.LogWarning("_avatarImage is null");
        _button = GetComponent<Button>();
    }

    void OnDestroy()
    {
        _button.onClick.RemoveListener(_handleClick);
    }

    public void Init(PartyChrono chrono, UnityAction handleClick)
    {
        // NOTE: temporary fix for when Init runs before Awake.
        if (_button == null) _button = GetComponent<Button>();

        _handleClick = handleClick;
        _button.onClick.AddListener(handleClick);
        _avatarImage.sprite = chrono.Stats.Data.Avatar;
    }
}
