using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject dialogueBox;
    public GameObject battleUI;

    public void FocusMainUI()
    {
        dialogueBox.SetActive(false);
        battleUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void FocusBattleUI()
    {
        mainUI.SetActive(false);
        dialogueBox.SetActive(false);
        battleUI.SetActive(true);
    }
}
