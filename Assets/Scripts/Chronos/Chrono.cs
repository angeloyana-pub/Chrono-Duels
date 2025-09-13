using UnityEngine;

[System.Serializable]
class Chrono {
    public ChronoData data;
    public int level = 1;
    
    public int Hp => hp;
    public int MaxHp => maxHp;
    public int Dmg => data.baseDmg + (level * 2);
    
    [SerializeField] private int hp;
    private int maxHp;
    
    public void Init() {
        if (data == null)
        {
            Debug.LogWarning("Chrono.Init(): data field is empty.");
        }
        maxHp = data.baseHp + (level > 1 ? level * 5 : 0);
        hp = Mathf.Clamp(hp, 0, maxHp);
    }
    
    public void LevelUp() {
        level += 1;
        maxHp = data.baseHp + (level * 5);
    }
    
    public void ChangeHp(int amount) {
        hp = Mathf.Clamp(hp + amount, 0, maxHp);
    }
}
