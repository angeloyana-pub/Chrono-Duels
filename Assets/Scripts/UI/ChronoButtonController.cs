using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChronoButtonController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    private Button _button;

    private UnityAction _handleClick;

    void Awake()
    {
        if (_nameText == null) Debug.LogWarning("_nameText is null");
        _button = GetComponent<Button>();
    }

    void OnDestroy()
    {
        _button.onClick.RemoveListener(_handleClick);
    }

    public void Init(PartyChrono chrono, UnityAction handleClick)
    {
        _handleClick = handleClick;
        _button.onClick.AddListener(handleClick);
        _nameText.text = chrono.Stats.Data.Name;
    }
}
