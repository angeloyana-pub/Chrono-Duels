using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    public BattleManager battleManager;

    private EnemyChrono enemyChrono;

    void Start()
    {
        enemyChrono = GetComponent<EnemyChrono>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            battleManager.StartBattle(transform.position, other.transform, transform);
        }
    }
}
