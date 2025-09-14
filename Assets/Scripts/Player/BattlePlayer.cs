using UnityEditor.Rendering;
using UnityEngine;

class BattlePlayer : MonoBehaviour
{
    private GameObject allyChrono;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
    }

    public PartyChrono GetFirstChrono()
    {
        if (inventoryManager.party.Count <= 0)
        {
            return null;
        }

        return inventoryManager.party[0];
    }

    public void SpawnFirstChrono(Transform allyBattlePosition)
    {
        PartyChrono firstChrono = GetFirstChrono();
        if (firstChrono == null)
        {
            Debug.LogWarning("SpawnFirstChrono(): Player has no chrono(s).");
        }
        allyChrono = Instantiate(firstChrono.stats.data.prefab, allyBattlePosition.position, Quaternion.identity);
    }

    public void EndBattle()
    {
        Destroy(allyChrono);
    }
}
