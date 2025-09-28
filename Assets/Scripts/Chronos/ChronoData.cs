using UnityEngine;

[CreateAssetMenu(fileName = "NewChronoData", menuName = "Game/Chrono Data")]
public class ChronoData : ScriptableObject
{
    public GameObject Prefab;
    public Sprite Avatar;
    
    public string Name;
    public int BaseHealth;
    public int BaseDamage;
}
