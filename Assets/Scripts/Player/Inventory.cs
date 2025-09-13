using System.Collections.Generic;
using UnityEngine;

class Inventory : MonoBehaviour {
    public List<ChronoInventorySlot> chronos = new List<ChronoInventorySlot>();
    
    void Start() {
        // TODO: display UI for chronos.
        
        foreach (ChronoInventorySlot chrono in chronos) {
            if (chrono.isActive) {
                Instantiate(
                    chrono.chrono.data.prefab,
                    // transform.position - transform.right * 0.7f,
                    transform.position,
                    Quaternion.identity
                ).GetComponent<FollowPlayer>().SetPlayer(transform);
            }
        }
    }
}