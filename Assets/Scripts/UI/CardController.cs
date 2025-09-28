using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardController : MonoBehaviour
{
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _healthText;
    private Button _button;
    
    private ChronoStats _stats;
    private UnityAction _handleClick;

    void Awake()
    {
        if (_avatarImage == null) Debug.LogWarning("_avatarImage is null");
        if (_nameText == null) Debug.LogWarning("_nameText is null");
        if (_healthText == null) Debug.LogWarning("_healthText is null");

        _button = GetComponent<Button>();
    }
    
    void OnDestroy()
    {
        _stats.HealthChanged -= SetHealth;
        _button.onClick.RemoveListener(_handleClick);
    }
    
    public void Init(ChronoStats stats, UnityAction handleClick)
    {
        _stats = stats;
        _handleClick = handleClick;
        
        SetAvatar(stats.Data.Avatar);
        SetName(stats.Data.Name);
        SetHealth(stats.Health);
        
        _stats.HealthChanged += SetHealth;
        _button.onClick.AddListener(_handleClick);
    }
    
    public void SetAvatar(Sprite avatar)
    {
        _avatarImage.sprite = avatar;
    }

    public void SetName(string name)
    {
        _nameText.text = name;
    }
    
    public void SetHealth(int health)
    {
        _healthText.text = health.ToString();
    }
}
