using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    
    public void Init(ChronoStats stats)
    {
        Debug.Log("CardManager.Init()");
        SetName(stats.data.name);
        SetHp(stats.hp);
        stats.m_OnHpChange.AddListener(SetHp);
    }
    
    public void SetName(string name)
    {
        nameText.text = name;
    }
    
    public void SetHp(int hp)
    {
        hpText.text = hp.ToString();
    }
}
