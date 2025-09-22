using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject dialogueBox;
    public GameObject battleUI;
    public GameObject crossfade;

    void Start()
    {
        crossfade.SetActive(true);
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
