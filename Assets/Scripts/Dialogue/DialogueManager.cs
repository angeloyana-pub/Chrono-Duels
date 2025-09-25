using System.Data;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public float TypingSpeed = 0.08f;
    public float FastTypingSpeed = 0.01f;

    [SerializeField] private UIManager _uiManager;

    [Header("Dialogue Objects")]
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _contentText;

    private float _typingSpeed;
    private Button _dialogueButton;
    private bool _isTyping = false;

    private Queue<DialogueItem> _dialogue = new Queue<DialogueItem>();

    void Awake()
    {
        if (_uiManager == null) Debug.LogWarning("_uiManager is null");
        if (_dialogueBox == null) Debug.LogWarning("_dialogueBox is null");
        if (_nameText == null) Debug.LogWarning("_nameText is null");
        if (_contentText == null) Debug.LogWarning("_contentText is null");

        _typingSpeed = TypingSpeed;
        _dialogueButton = _dialogueBox.GetComponent<Button>();
        if (_dialogueButton == null) Debug.LogWarning("_dialogueButton is null");

    }

    public void StartDialogue(Dialogue dialogue)
    {
        _dialogue.Clear();
        foreach (DialogueItem item in dialogue.dialogue)
        {
            _dialogue.Enqueue(item);
        }
        _dialogueBox.SetActive(true);
        NextDialogueItem();
    }

    public void NextDialogueItem()
    {
        if (_isTyping)
        {
            _typingSpeed = FastTypingSpeed;
            return;
        }

        if (_dialogue.Count <= 0)
        {
            _dialogueBox.SetActive(false);
            return;
        }

        DialogueItem item = _dialogue.Dequeue();
        _nameText.text = item.name;
        _typingSpeed = TypingSpeed;
        StartCoroutine(TypeContent(item.content));
    }

    public void ShowDialogueBox(bool hideAllUI = true)
    {
        if (hideAllUI) _uiManager.HideAll();
        _dialogueButton.interactable = false;
        _dialogueBox.SetActive(true);
    }

    public void HideDialogueBox(UI uiToShow)
    {
        _dialogueBox.SetActive(false);
        _uiManager.ShowUI(uiToShow);
    }

    public void SetDialogueName(string name)
    {
        _nameText.text = name;
    }

    public void SetTypingSpeed(float typingSpeed)
    {
        _typingSpeed = typingSpeed;
    }

    public void UseFastTypingSpeed()
    {
        SetTypingSpeed(FastTypingSpeed);
    }

    public IEnumerator TypeContent(string content)
    {
        _isTyping = true;
        _contentText.text = "";

        foreach (char letter in content.ToCharArray())
        {
            _contentText.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }
        _isTyping = false;
    }
}
