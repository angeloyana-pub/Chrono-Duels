using UnityEngine;

// NOTE: must be attached to player.
class ActiveChronoManager : MonoBehaviour
{
    private GameObject activeChrono;
    private PlayerController playerController;
    private InventoryManager inventoryManager;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        inventoryManager = GetComponent<InventoryManager>();
        ShowActiveChrono();
    }

    public void ShowActiveChrono()
    {
        if (activeChrono != null) return;

        PartyChrono activePartyChrono = inventoryManager.party.Find((x) => x.isActive);
        if (activePartyChrono != null)
        {
            activeChrono = Instantiate(
                activePartyChrono.stats.data.prefab,
                transform.position - transform.right * (playerController.isFacingRight ? 0.5f : -0.5f),
                Quaternion.identity
            );
            activeChrono.AddComponent<FollowPlayer>().SetPlayer(transform);
        }
    }

    public void HideActiveChrono()
    {
        // TODO: show smoke animation before destroy.
        Destroy(activeChrono);
    }
}
