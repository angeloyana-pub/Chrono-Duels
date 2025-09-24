using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class BattleChrono : MonoBehaviour
{
    public ChronoStats Stats;

    private SpriteRenderer _sr;
    private Animator _anim;

    private BattleManager _battleManager;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    public void Setup(BattleManager battleManager)
    {
        _battleManager = battleManager;

        transform.position = battleManager.EnemyBattlePosition.position;
        _sr.flipX = true;
    }

    public void TakeDamage()
    {
        Stats.TakeDamage(_battleManager.Player.AllyStats.Damage);
        _anim.SetTrigger("Hurt");
    }

    public void Escape()
    {
        transform.position = _battleManager.BattlePosition;
        _sr.flipX = true;
    }
}
