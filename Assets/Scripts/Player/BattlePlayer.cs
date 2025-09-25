using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(ActiveChronoManager))]
public class BattlePlayer : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Animator _anim;
    private PlayerController _playerController;
    private InventoryManager _inventoryManager;
    private ActiveChronoManager _activeChronoManager;

    private BattleManager _battleManager;

    private GameObject _allyObject;
    private SpriteRenderer _allySr;
    private Animator _allyAnim;
    private ChronoStats _allyStats;

    public ChronoStats AllyStats => _allyStats;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _inventoryManager = GetComponent<InventoryManager>();
        _activeChronoManager = GetComponent<ActiveChronoManager>();
    }

    public void Setup(BattleManager battleManager)
    {
        _battleManager = battleManager;

        _playerController.enabled = false;
        _activeChronoManager.enabled = false;

        transform.position = battleManager.PlayerBattlePosition.position;
        _sr.flipX = false;


        for (int i = 0; i < _inventoryManager.Party.Count; i++)
        {
            int index = i;
            PartyChrono chrono = _inventoryManager.Party[index];

            GameObject cardObject = Instantiate(_battleManager.CardPrefab, _battleManager.CardContainer);
            CardController cardController = cardObject.GetComponent<CardController>();
            if (cardController == null) Debug.LogWarning("cardController is null");
            cardController.Init(chrono.Stats, () => StartCoroutine(ChangeAlly(index)));
        }

        StartCoroutine(ChangeAlly(_inventoryManager.Party.FindIndex(c => !c.Stats.IsFainted)));
    }

    public IEnumerator ChangeAlly(int index)
    {
        if (index < 0 || index > _inventoryManager.Party.Count) yield break;
        
        _anim.SetTrigger("Throw");
        yield return new WaitForSeconds(0.3f);
        Destroy(_allyObject);

        PartyChrono chrono = _inventoryManager.Party[index];
        _allyObject = Instantiate(
            chrono.Stats.Data.Prefab,
            _battleManager.AllyBattlePosition,
            Quaternion.identity
        );
        _allySr = _allyObject.GetComponent<SpriteRenderer>();
        _allyAnim = _allyObject.GetComponent<Animator>();
        _allyStats = chrono.Stats;
    }

    public void Attack()
    {
        StartCoroutine(_attack());
    }

    public IEnumerator _attack()
    {
        _allyAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        _battleManager.Enemy.TakeDamage();
        yield return new WaitForSeconds(0.5f);

        _battleManager.DialogueManagerRef.ShowDialogueBox();
        _battleManager.DialogueManagerRef.SetDialogueName("Battle");
        _battleManager.DialogueManagerRef.SetTypingSpeed(0.06f);
        yield return _battleManager.DialogueManagerRef.TypeContent("Enemy took " + _allyStats.Damage + " damage.");
        yield return new WaitForSeconds(0.5f);
        yield return _battleManager.Enemy.Attack();
    }

    public void TakeDamage()
    {
        _allyStats.TakeDamage(_battleManager.Enemy.Stats.Damage);
        _allyAnim.SetTrigger("Hurt");
    }

    public void Escape()
    {
        Destroy(_allyObject);
        foreach (Transform child in _battleManager.CardContainer.transform)
        {
            Destroy(child.gameObject);
        }

        _playerController.enabled = true;
        _activeChronoManager.enabled = true;
    }
}
