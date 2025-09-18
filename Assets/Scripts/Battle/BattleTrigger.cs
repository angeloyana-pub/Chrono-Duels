using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    public BattleManager battleManager;

    private BattleChrono battleChrono;

    void Start()
    {
        battleChrono = GetComponent<BattleChrono>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            battleManager.StartBattle(transform.position, other.GetComponent<BattlePlayer>(), battleChrono);
        }
    }
}
