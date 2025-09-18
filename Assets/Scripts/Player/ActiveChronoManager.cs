using UnityEngine;

public class ActiveChronoManager : MonoBehaviour
{
    private SpriteRenderer sr;
    private InventoryManager inventoryManager;
    
    private GameObject activeChrono;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        inventoryManager = GetComponent<InventoryManager>();
    }
    
    void OnEnable() {
        SpawnActiveChrono();
    }
    
    void OnDisable() {
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
        return transform.position - transform.right * (sr.flipX ? -0.5f : 0.5f);
    }
}