using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

class DialogueManager : MonoBehaviour {
  public TextMeshProUGUI nameText;
  public TextMeshProUGUI contentText;
  public GameObject dialogueBox;
  
  public float defaultTypingSpeed = 0.1f;
  public float fastTypingSpeed = 0.01f;
  private float typingSpeed;
  private bool isTyping = false;
  
  private Queue<DialogueItem> dialogue = new Queue<DialogueItem>();
  
  void Start() {
    typingSpeed = defaultTypingSpeed;
  }
  
  public void StartDialogue(Dialogue dialogue) {
    Debug.Log("Starting dialogue...");
    this.dialogue.Clear();
    foreach (DialogueItem item in dialogue.dialogue) {
      this.dialogue.Enqueue(item);
    }
    dialogueBox.SetActive(true);
    NextDialogueItem();
  }
  
  public void NextDialogueItem() {
    Debug.Log("Display next dialogue item...");
    if (isTyping) {
      typingSpeed = fastTypingSpeed;
      return;
    }

    if (dialogue.Count <= 0) {
      EndDialogue();
      return;
    }
    
    DialogueItem item = dialogue.Dequeue();
    nameText.text = item.name;
    StartCoroutine(TypeContent(item.content));
  }
  
  IEnumerator TypeContent(string content) {
    isTyping = true;
    typingSpeed = defaultTypingSpeed;
    contentText.text = "";
    
    foreach (char letter in content.ToCharArray()) {
      contentText.text += letter;
      yield return new WaitForSeconds(typingSpeed);
    }
    isTyping = false;
  }
  
  public void EndDialogue() {
    Debug.Log("Hide dialogue box...");
    dialogueBox.SetActive(false);
  }
}