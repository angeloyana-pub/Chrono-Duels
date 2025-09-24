using System.Data;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public float TypingSpeed = 0.1f;
    public float FastTypingSpeed = 0.01f;

    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _contentText;

    private float _typingSpeed;
    private Button _dialogueButton;

    void Awake()
    {
        if (_dialogueBox == null) Debug.LogWarning("_dialogueBox is null");
        if (_nameText == null) Debug.LogWarning("_nameText is null");
        if (_contentText == null) Debug.LogWarning("_contentText is null");

        _typingSpeed = TypingSpeed;
        _dialogueButton = _dialogueBox.GetComponent<Button>();
        if (_dialogueButton == null) Debug.LogWarning("_dialogueButton is null");

    }

    public void StartDialogue(Dialogue dialogue)
    {

    }

    private IEnumerator TypeContent(string content)
    {
        yield return null;
    }
}