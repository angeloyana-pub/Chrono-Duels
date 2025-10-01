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
            cardController.Init(chrono.Stats, () =>
            {
                if (!chrono.Stats.IsFainted)
                {
                    StartCoroutine(ChangeAlly(index));
                }
            });
        }

        StartCoroutine(ChangeAlly(FindFirstNonFaintedIndex()));
    }

    private void HandleChangeHealth(int health)
    {
        _battleManager.AllyHealthSlider.value = health;
    }

    public int FindFirstNonFaintedIndex()
    {
        return _inventoryManager.Party.FindIndex(c => !c.Stats.IsFainted);
    }

    public IEnumerator ChangeAlly(int index, bool ignoreBusy = false)
    {
        if ((_battleManager.IsBusy && !ignoreBusy) || index < 0 || index > _inventoryManager.Party.Count) yield break;

        if (_allyStats != null)
        {
            _allyStats.HealthChanged -= HandleChangeHealth;
        }

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

        _battleManager.AllyNameText.text = _allyStats.Data.Name;
        _battleManager.AllyHealthSlider.maxValue = _allyStats.MaxHealth;
        HandleChangeHealth(_allyStats.Health);
        _allyStats.HealthChanged += HandleChangeHealth;
    }

    public void Attack()
    {
        _allyAnim.SetTrigger("Attack");
    }

    public void TakeDamage()
    {
        _allyStats.TakeDamage(_battleManager.Enemy.Stats.Damage);
        _allyAnim.SetTrigger(_allyStats.IsFainted ? "Death" : "Hurt");
    }

    public void TakeEnemy()
    {
        _inventoryManager.Party.Add(new PartyChrono
        {
            Stats = _battleManager.Enemy.Stats,
            IsActive = true
        });
        _activeChronoManager.RenderChronoButtons();
    }

    public void EndBattle(bool isFainted)
    {
        if (_allyStats != null)
        {
            _allyStats.HealthChanged -= HandleChangeHealth;
        }

        Destroy(_allyObject);
        foreach (Transform child in _battleManager.CardContainer.transform)
        {
            Destroy(child.gameObject);
        }

        _playerController.enabled = true;
        _activeChronoManager.enabled = true;

        if (isFainted)
        {
            transform.position = _battleManager.SpawnPosition.position;
            foreach (PartyChrono chrono in _inventoryManager.Party)
            {
                chrono.Stats.ToFullHealth();
            }
        }
    }
}
