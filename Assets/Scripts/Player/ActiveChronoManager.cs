using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InventoryManager))]
public class ActiveChronoManager : MonoBehaviour
{
    public float SpawnDistance = 2f;
    [SerializeField] private Transform _chronoButtonContainer;
    [SerializeField] private GameObject _chronoButtonPrefab;

    private SpriteRenderer _sr;
    private InventoryManager _inventoryManager;
    
    private GameObject _activeChrono;
    
    void Awake()
    {
        if (_chronoButtonContainer == null) Debug.LogWarning("_chronoButtonContainer is null");
        if (_chronoButtonPrefab == null) Debug.LogWarning("_chronoButtonPrefab is null");
        _sr = GetComponent<SpriteRenderer>();
        _inventoryManager = GetComponent<InventoryManager>();
    }

    void Start()
    {
        for (int i = 0; i < _inventoryManager.Party.Count; i++)
        {
            int index = i;
            PartyChrono chrono = _inventoryManager.Party[index];
            GameObject chronoButtonObject = Instantiate(_chronoButtonPrefab, _chronoButtonContainer);
            ChronoButtonController chronoButtonController = chronoButtonObject.GetComponent<ChronoButtonController>();
            if (chronoButtonController == null) Debug.LogWarning("chronoButtonController is null");
            chronoButtonController.Init(chrono, () => SpawnChrono(index));
        }
    }

    void OnEnable()
    {
        int index = _inventoryManager.Party.FindIndex(c => c.IsActive);
        SpawnChrono(index);
    }
    
    void OnDisable()
    {
        if (_activeChrono != null) {
            Destroy(_activeChrono);
            _activeChrono = null;
        }
    }
    
    public void SpawnChrono(int index)
    {
        if (_inventoryManager.Party.Count <= 0 || index < 0 || index >= _inventoryManager.Party.Count) {
            Debug.LogWarning("Invalid index or party is empty.");
            return;
        }
        
        if (_activeChrono != null) {
            Destroy(_activeChrono);
            _activeChrono = null;
        }

        foreach (PartyChrono partyChrono in _inventoryManager.Party)
        {
            partyChrono.IsActive = false;
        }

        PartyChrono chrono = _inventoryManager.Party[index];
        chrono.IsActive = true;
        _activeChrono = Instantiate(
            chrono.Stats.Data.Prefab,
            GetSpawnPosition(),
            Quaternion.identity
        );
        _activeChrono.AddComponent<FollowPlayer>().SetPlayer(transform);
    }
    
    private Vector3 GetSpawnPosition()
    {
        return transform.position - transform.right * (_sr.flipX ? -SpawnDistance : SpawnDistance);
    }
}
