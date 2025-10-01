using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private BattleManager _battleManager;

    private BattleChrono _battleChrono;

    void Start()
    {
        _battleChrono = GetComponent<BattleChrono>();
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         _battleManager.StartBattle(transform.position, other.GetComponent<BattlePlayer>(), _battleChrono);
    //     }
    // }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _battleManager.StartBattle(transform.position, collision.gameObject.GetComponent<BattlePlayer>(), _battleChrono);
        }
    }
}
