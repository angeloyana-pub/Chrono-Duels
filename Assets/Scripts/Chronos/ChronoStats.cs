using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChronoStats
{
    public ChronoData data;

    public int level = 1;
    public int hp;
    public int dmg;

    public int maxHp => data.baseHp + (level > 1 ? level * 3 : 0);
    public bool isFainted => hp <= 0;
    [HideInInspector] public UnityEvent<int> m_OnHpChange;

    public void Init()
    {
        m_OnHpChange = new UnityEvent<int>();
        if (hp <= 0)
        {
            hp = data.baseHp + (level > 1 ? level * 3 : 0);
        }
        if (dmg <= 0)
        {
            dmg = data.baseDmg + (level > 1 ? level * 2 : 0);
        }
    }
    
    public void LevelUp() {
        level += 1;
        dmg = data.baseDmg + (level > 1 ? level * 2 : 0);
    }
    
    public void ChangeHp(int amount)
    {
        hp = Mathf.Clamp(hp + amount, 0, maxHp);
        m_OnHpChange.Invoke(hp);
    }
}
