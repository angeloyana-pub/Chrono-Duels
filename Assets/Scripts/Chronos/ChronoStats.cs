[System.Serializable]
public class ChronoStats
{
    public ChronoData data;

    public int level = 1;
    public int hp;
    public int dmg;

    public int maxHp => data.baseHp + (level > 1 ? level * 3 : 0);

    public void Init()
    {
        if (hp <= 0)
        {
            hp = data.baseHp + (level > 1 ? level * 3 : 0);
        }
        if (dmg <= 0)
        {
            dmg = data.baseDmg + (level > 1 ? level * 2 : 0);
        }
    }
}
