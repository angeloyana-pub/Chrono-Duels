using TMPro;
using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System.Collections;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public AudioManager AudioManagerRef;

    [Header("UI Components")]
    public UIManager UIManagerRef;
    public DialogueManager DialogueManagerRef;
    public Transform CardContainer;
    public GameObject CardPrefab;

    [Header("Stats UI Components")]
    public TextMeshProUGUI AllyNameText;
    public Slider AllyHealthSlider;
    public TextMeshProUGUI EnemyNameText;
    public Slider EnemyHealthSlider;

    [Header("Battle Settings Components")]
    public Transform BattleGround;
    public Transform PlayerBattlePosition;
    public Transform EnemyBattlePosition;
    public CinemachineCamera BattleCamera;
    public Transform SpawnPosition;

    [HideInInspector] public Vector3 BattlePosition;
    public Vector3 AllyBattlePosition => PlayerBattlePosition.position + PlayerBattlePosition.right * 2f;

    [HideInInspector] public BattlePlayer Player;
    [HideInInspector] public BattleChrono Enemy;

    private bool HasStarted = false;
    [HideInInspector] public bool IsBusy = false;

    void Awake()
    {
        if (UIManagerRef == null) Debug.LogWarning("UIManagerRef is null");
        if (DialogueManagerRef == null) Debug.LogWarning("DialogueManagerRef is null");
        if (CardContainer == null) Debug.LogWarning("CardContainer is null");
        if (CardPrefab == null) Debug.LogWarning("CardPrefab is null");

        if (AllyNameText == null) Debug.LogWarning("AllyNameText is null");
        if (AllyHealthSlider == null) Debug.LogWarning("AllyHealthSlider is null");
        if (EnemyNameText == null) Debug.LogWarning("EnemyNameText is null");
        if (EnemyHealthSlider == null) Debug.LogWarning("EnemyHealthSlider is null");

        if (BattleGround == null) Debug.LogWarning("BattleGround is null");
        if (PlayerBattlePosition == null) Debug.LogWarning("PlayerBattlePosition is null");
        if (EnemyBattlePosition == null) Debug.LogWarning("EnemyBattlePosition is null");
        if (BattleCamera == null) Debug.LogWarning("BattleCamera is null");
        if (SpawnPosition == null) Debug.LogWarning("SpawnPosition is null");
    }

    public void StartBattle(Vector3 battlePosition, BattlePlayer player, BattleChrono enemy)
    {
        if (HasStarted) return;
        HasStarted = true;
        StartCoroutine(UIManagerRef.ShowCrossfade(() => _startBattle(battlePosition, player, enemy)));
    }

    private void _startBattle(Vector3 battlePosition, BattlePlayer player, BattleChrono enemy)
    {
        BattlePosition = battlePosition;
        Player = player;
        Enemy = enemy;
        IsBusy = false;

        BattleGround.position = battlePosition;
        BattleCamera.Priority = 5;
        UIManagerRef.ShowUI(UIGroup.Battle);

        player.Setup(this);
        enemy.Setup(this);
        AudioManagerRef.PlayBattleClip();
    }

    public void StartAttackSequence()
    {
        if (IsBusy) return;
        IsBusy = true;
        StartCoroutine(_startAttackSequence());
    }

    private IEnumerator _startAttackSequence()
    {
        Player.Attack();
        yield return new WaitForSeconds(0.5f);
        Enemy.TakeDamage();
        yield return new WaitForSeconds(0.5f);

        DialogueManagerRef.ShowDialogueBox();
        DialogueManagerRef.SetInteractable(false);
        DialogueManagerRef.HideName();
        DialogueManagerRef.SetTypingSpeed(0.03f);
        yield return DialogueManagerRef.TypeContent($"Enemy took {Player.AllyStats.Damage} damage.");
        yield return new WaitForSeconds(0.5f);

        if (Enemy.Stats.IsFainted)
        {
            yield return DialogueManagerRef.TypeContent($"You won the battle!");
            yield return new WaitForSeconds(0.5f);
            Player.AllyStats.LevelUp();
            yield return DialogueManagerRef.TypeContent($"{Player.AllyStats.Data.Name} is now level {Player.AllyStats.Level}.");
            yield return UIManagerRef.ShowCrossfade(null, false);

            Player.TakeEnemy();
            Player.EndBattle(false);
            Enemy.EndBattle(true);
            EndBattle();

            DialogueManagerRef.HideDialogueBox(UIGroup.Main);

            yield return UIManagerRef.HideCrossfade();
            yield break;
        }

        Enemy.Attack();
        yield return new WaitForSeconds(0.5f);
        Player.TakeDamage();
        yield return new WaitForSeconds(0.5f);

        yield return DialogueManagerRef.TypeContent($"Ally took {Enemy.Stats.Damage} damage.");
        yield return new WaitForSeconds(0.5f);

        if (Player.AllyStats.IsFainted)
        {
            int index = Player.FindFirstNonFaintedIndex();
            if (index != -1)
            {
                string prevAllyName = Player.AllyStats.Data.Name;
                yield return Player.ChangeAlly(index, true);
                yield return DialogueManagerRef.TypeContent($"Your {prevAllyName} fainted! Go {Player.AllyStats.Data.Name}!");
            }
            else
            {
                yield return DialogueManagerRef.TypeContent($"You lose the battle.");
                yield return new WaitForSeconds(0.5f);
                yield return UIManagerRef.ShowCrossfade(null, false);

                Player.EndBattle(true);
                Enemy.EndBattle(false);
                EndBattle();

                DialogueManagerRef.HideDialogueBox(UIGroup.Main);

                yield return UIManagerRef.HideCrossfade();
                yield break;
            }
        }

        DialogueManagerRef.HideDialogueBox(UIGroup.Battle);
        IsBusy = false;
    }

    public void Escape()
    {
        StartCoroutine(UIManagerRef.ShowCrossfade(() => _escape()));
    }

    private void _escape()
    {
        Player.EndBattle(false);
        Enemy.EndBattle(false);
        EndBattle();
    }

    public void EndBattle()
    {
        AudioManagerRef.PlayOverworldClip();
        BattleCamera.Priority = 0;
        UIManagerRef.ShowUI(UIGroup.Main);
        HasStarted = false;
    }
}
