using UnityEngine;

public class ActiveChronoManager : MonoBehaviour
{
    public float spawnDistance = 2f;

    private SpriteRenderer sr;
    private InventoryManager inventoryManager;
    
    private GameObject activeChrono;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        inventoryManager = GetComponent<InventoryManager>();
    }
    
    void OnEnable() {
        Debug.Log("Enabled");
        SpawnActiveChrono();
    }
    
    void OnDisable() {
        Debug.Log("Disabled");
        if (activeChrono != null) {
            Destroy(activeChrono);
            activeChrono = null;
        }
    }
    
    private void SpawnActiveChrono() {
        if (inventoryManager.party.Count <= 0 || activeChrono != null) return;
        
        PartyChrono chrono = inventoryManager.party.Find(c => c.isActive);
        if (chrono == null) return;
        
        activeChrono = Instantiate(
            chrono.stats.data.prefab,
            GetSpawnPosition(),
            Quaternion.identity
        );
        activeChrono.AddComponent<FollowPlayer>().SetPlayer(transform);
    }
    
    private Vector3 GetSpawnPosition()
    {
        return transform.position - transform.right * (sr.flipX ? -spawnDistance : spawnDistance);
    }
}