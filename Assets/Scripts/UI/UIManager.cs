using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject dialogueBox;
    public GameObject battleUI;

    void Start()
    {
        ShowMainUI();
    }

    public void HideAllUI()
    {
        mainUI.SetActive(false);
        dialogueBox.SetActive(false);
        battleUI.SetActive(false);
    }
    
    public void ShowMainUI()
    {
        HideAllUI();
        mainUI.SetActive(true);
    }
    
    public void ShowBattleUI()
    {
        HideAllUI();
        battleUI.SetActive(true);
    }
}
