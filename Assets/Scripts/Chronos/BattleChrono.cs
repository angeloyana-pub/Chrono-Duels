using System.Collections;
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

    public IEnumerator Attack()
    {
        _anim.SetTrigger("Attack");
        _battleManager.Player.TakeDamage();
        yield return _battleManager.DialogueManagerRef.TypeContent("Your chrono took " + Stats.Damage + " damage.");
        yield return new WaitForSeconds(0.5f);
        _battleManager.DialogueManagerRef.HideDialogueBox(UI.Battle);
    }

    public void Escape()
    {
        transform.position = _battleManager.BattlePosition;
        _sr.flipX = true;
    }
}
