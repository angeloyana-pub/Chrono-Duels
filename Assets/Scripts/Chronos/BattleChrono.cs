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

        Stats.Init();
    }

    public void Setup(BattleManager battleManager)
    {
        _battleManager = battleManager;

        _battleManager.EnemyNameText.text = Stats.Data.Name;
        _battleManager.EnemyHealthSlider.maxValue = Stats.MaxHealth;
        HandleChangeHealth(Stats.Health);
        Stats.HealthChanged += HandleChangeHealth;

        transform.position = battleManager.EnemyBattlePosition.position;
        _sr.flipX = true;

    }

    private void HandleChangeHealth(int health)
    {
        _battleManager.EnemyHealthSlider.value = health;
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }

    public void TakeDamage()
    {
        Stats.TakeDamage(_battleManager.Player.AllyStats.Damage);
        _anim.SetTrigger(Stats.IsFainted ? "Death" : "Hurt");
    }

    public void EndBattle(bool isFainted)
    {
        Stats.HealthChanged -= HandleChangeHealth;

        if (!isFainted)
        {
            transform.position = _battleManager.BattlePosition;
            _sr.flipX = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
