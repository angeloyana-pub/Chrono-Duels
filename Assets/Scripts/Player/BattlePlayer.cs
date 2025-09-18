using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattlePlayer : MonoBehaviour
{
    public Transform cardContainer;
    public GameObject cardPrefab;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;

    private SpriteRenderer sr;
    private PlayerController playerController;
    private ActiveChronoManager activeChronoManager;
    private InventoryManager inventoryManager;
    
    private Transform playerBattlePosition;
    private Transform allyBattlePosition;
    
    private PartyChrono allyChrono;
    private GameObject allyChronoObject;

    public ChronoStats stats => allyChrono.stats;

    private UnityAction<int> allyHpChangeListener;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        activeChronoManager = GetComponent<ActiveChronoManager>();
        inventoryManager = GetComponent<InventoryManager>();
    }
    
    public void BattleSetup(Transform playerBattlePosition, Transform allyBattlePosition)
    {
        // TODO: check if player is eligible to fight before continuing.
        this.playerBattlePosition = playerBattlePosition;
        this.allyBattlePosition = allyBattlePosition;
        
        playerController.enabled = false;
        activeChronoManager.enabled = false;
        
        transform.position = playerBattlePosition.position;
        sr.flipX = false;
        
        RenderCards();
        ChangeChrono(inventoryManager.party.FindIndex(c => !c.stats.isFainted));
    }
    
    private void RenderCards()
    {
        for (int i = 0; i < inventoryManager.party.Count; i++)
        {
            int index = i;
            PartyChrono chrono = inventoryManager.party[i];
            
            GameObject cardObject = Instantiate(cardPrefab, cardContainer);
            CardManager cardManager = cardObject.GetComponent<CardManager>();
            Button cardButton = cardObject.GetComponent<Button>();

            Debug.Log(chrono);
            Debug.Log(cardManager);
            cardManager.Init(chrono.stats);
            cardButton.onClick.AddListener(() => ChangeChrono(index));
        }
    }
    
    public void ChangeChrono(int index)
    {
        if (allyHpChangeListener != null)
        {
            foreach (PartyChrono c in inventoryManager.party)
            {
                c.stats.m_OnHpChange.RemoveListener(allyHpChangeListener);
            }
        }

        PartyChrono chrono = inventoryManager.party[index];
        nameText.text = chrono.stats.data.name;
        hpText.text = chrono.stats.hp.ToString();
        allyHpChangeListener = (x) => hpText.text = x.ToString();
        chrono.stats.m_OnHpChange.AddListener(allyHpChangeListener);

        if (allyChronoObject != null) Destroy(allyChronoObject);
        allyChrono = chrono;
        allyChronoObject = Instantiate(
            chrono.stats.data.prefab,
            allyBattlePosition.position,
            Quaternion.identity // TODO: must be facing right.
        );
    }
    
    public void Attack(BattleChrono enemy)
    {
        enemy.stats.ChangeHp(-allyChrono.stats.dmg);
    }
    
    public void PlayerEscape()
    {
        Destroy(allyChronoObject);
        foreach (Transform child in cardContainer.transform)
        {
            Destroy(child.gameObject);
        }
        
        playerController.enabled = true;
        activeChronoManager.enabled = true;
    }
}
