using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<PartyChrono> Party = new List<PartyChrono>();

    void Start()
    {
        foreach (PartyChrono chrono in Party)
        {
            chrono.Stats.Init();
        }
    }
}
