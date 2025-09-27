using System.Data;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public float DefaultTypingSpeed = 0.08f;

    [SerializeField] private UIManager _uiManager;

    [Header("Dialogue Objects")]
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _contentText;

    private float _typingSpeed;
    private Button _dialogueButton;
    private bool _isTyping = false;

    private Queue<DialogueItem> _dialogue = new Queue<DialogueItem>();
    private DialogueItem _currentItem;

    void Awake()
    {
        if (_uiManager == null) Debug.LogWarning("_uiManager is null");
        if (_dialogueBox == null) Debug.LogWarning("_dialogueBox is null");
        if (_nameText == null) Debug.LogWarning("_nameText is null");
        if (_contentText == null) Debug.LogWarning("_contentText is null");

        _typingSpeed = DefaultTypingSpeed;
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
        _typingSpeed = DefaultTypingSpeed;
        NextDialogueItem();
    }

    public void NextDialogueItem()
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            _contentText.text = _currentItem.content;
            _isTyping = false;
            return;
        }

        if (_dialogue.Count <= 0)
        {
            _dialogueBox.SetActive(false);
            return;
        }

        _currentItem = _dialogue.Dequeue();
        if (!string.IsNullOrEmpty(_currentItem.name))
        {
            ShowName();
            SetName(_currentItem.name);
        }
        else
        {
            HideName();
        }
        StartCoroutine(TypeContent(_currentItem.content));
    }

    public void ShowDialogueBox(bool hideAllUI = true)
    {
        if (hideAllUI) _uiManager.HideAll();
        _dialogueBox.SetActive(true);
    }

    public void HideDialogueBox(UIGroup uiGroup)
    {
        _dialogueBox.SetActive(false);
        _uiManager.ShowUI(uiGroup);
    }

    public void ShowName()
    {
        _nameText.gameObject.SetActive(true);
    }
    
    public void HideName()
    {
        _nameText.gameObject.SetActive(false);
    }

    public void SetName(string name)
    {
        _nameText.text = name;
    }

    public void SetInteractable(bool value)
    {
        _dialogueButton.interactable = value;
    }

    public void SetTypingSpeed(float typingSpeed)
    {
        _typingSpeed = typingSpeed;
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
