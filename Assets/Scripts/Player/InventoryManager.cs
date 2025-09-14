using System.Collections.Generic;
using UnityEngine;

// NOTE: must be attached to player.
class InventoryManager : MonoBehaviour
{
    public List<PartyChrono> party = new List<PartyChrono>();

    void Start()
    {
        foreach (PartyChrono partyChrono in party)
        {
            partyChrono.stats.Init();
        }
    }
}
