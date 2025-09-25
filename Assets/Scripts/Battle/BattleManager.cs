using TMPro;
using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class BattleManager : MonoBehaviour
{
    [Header("UI Components")]
    public UIManager UIManagerRef;
    public DialogueManager DialogueManagerRef;
    public Transform CardContainer;
    public GameObject CardPrefab;

    [Header("Battle Settings Components")]
    public Transform BattleGround;
    public Transform PlayerBattlePosition;
    public Transform EnemyBattlePosition;
    public CinemachineCamera BattleCamera;

    [HideInInspector] public Vector3 BattlePosition;
    public Vector3 AllyBattlePosition => PlayerBattlePosition.position + PlayerBattlePosition.right * 2f;

    [HideInInspector] public BattlePlayer Player;
    [HideInInspector] public BattleChrono Enemy;

    void Awake()
    {
        if (UIManagerRef == null) Debug.LogWarning("UIManagerRef is null");
        if (DialogueManagerRef == null) Debug.LogWarning("DialogueManagerRef is null");
        if (CardContainer == null) Debug.LogWarning("CardContainer is null");
        if (CardPrefab == null) Debug.LogWarning("CardPrefab is null");

        if (BattleGround == null) Debug.LogWarning("BattleGround is null");
        if (PlayerBattlePosition == null) Debug.LogWarning("PlayerBattlePosition is null");
        if (EnemyBattlePosition == null) Debug.LogWarning("EnemyBattlePosition is null");
        if (BattleCamera == null) Debug.LogWarning("BattleCamera is null");
    }

    public void StartBattle(Vector3 battlePosition, BattlePlayer player, BattleChrono enemy)
    {
        StartCoroutine(UIManagerRef.ShowCrossfade(() => _startBattle(battlePosition, player, enemy)));
    }

    private void _startBattle(Vector3 battlePosition, BattlePlayer player, BattleChrono enemy)
    {
        BattlePosition = battlePosition;
        Player = player;
        Enemy = enemy;

        BattleGround.position = battlePosition;
        BattleCamera.Priority = 5;
        UIManagerRef.ShowBattleUI();

        player.Setup(this);
        enemy.Setup(this);
    }

    public void Attack()
    {
        Player.Attack();
    }

    public void Escape()
    {
        StartCoroutine(UIManagerRef.ShowCrossfade(() => _escape()));
    }

    public void _escape()
    {
        Player.Escape();
        Enemy.Escape();

        BattleCamera.Priority = 0;
        UIManagerRef.ShowMainUI();
    }
}
