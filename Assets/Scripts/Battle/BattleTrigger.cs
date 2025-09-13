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
            TriggerBattle();
        }
    }

    void TriggerBattle()
    {
        battleManager.StartBattle(transform.position, battleChrono);
    }
}
