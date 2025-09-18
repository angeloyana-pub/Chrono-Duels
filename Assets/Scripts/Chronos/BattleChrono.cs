using TMPro;
using UnityEngine;

public class BattleChrono : MonoBehaviour
{
    public ChronoStats stats;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;

    private SpriteRenderer sr;
    private Vector3 battlePosition;
    private Transform enemyBattlePosition;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        stats.Init();
    }
    
    public void BattleSetup(Vector3 battlePosition, Transform enemyBattlePosition)
    {
        this.battlePosition = battlePosition;
        this.enemyBattlePosition = enemyBattlePosition;

        nameText.text = stats.data.name;
        hpText.text = stats.hp.ToString();
        stats.m_OnHpChange.AddListener((x) => hpText.text = x.ToString());

        transform.position = enemyBattlePosition.position;
        sr.flipX = true;
    }

    public void Attack(BattlePlayer player)
    {
        player.stats.ChangeHp(-stats.dmg);
    }

    public void PlayerEscape()
    {
        transform.position = battlePosition;
        sr.flipX = true;
    }
}
