using UnityEngine;

[CreateAssetMenu(fileName = "NewChronoData", menuName = "Game/Chrono Data")]
public class ChronoData : ScriptableObject
{
    public GameObject prefab;

    public string name;
    public int baseHp;
    public int baseDmg;
}
