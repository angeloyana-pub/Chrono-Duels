using UnityEngine;
using System;

[System.Serializable]
public class ChronoStats
{
    public ChronoData Data;
    
    public int Level = 1;
    public int Health = 0;
    public bool HasCustomHealth = false;

    public int Damage => Data.BaseDamage + ((Level - 1) * 2);
    public int MaxHealth => Data.BaseHealth + ((Level - 1) * 3);
    public bool IsFainted => Health <= 0;
    
    public event Action<int> HealthChanged;
    
    public void Init()
    {
        if (Data == null) Debug.LogWarning("Data is required");
        Health = HasCustomHealth ? Health : MaxHealth;
        HealthChanged?.Invoke(Health);
    }
    
    public void LevelUp()
    {
        Level++;
    }
    
    public void ChangeHealth(int amount)
    {
        Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
        HealthChanged?.Invoke(Health);
    }
    
    public void TakeDamage(int amount)
    {
        ChangeHealth(-amount);
    }
    
    public void Heal(int amount)
    {
        ChangeHealth(amount);
    }
}
