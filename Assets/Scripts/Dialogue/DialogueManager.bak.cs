// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using System;
// using UnityEngine.UI;

// public class DialogueManager : MonoBehaviour
// {
//   public UIManager UIManagerRef;
//   public TextMeshProUGUI nameText;
//   public TextMeshProUGUI contentText;
//   public GameObject dialogueBox;
//   [HideInInspector] public Button dialogueButton;

//   public float defaultTypingSpeed = 0.1f;
//   public float fastTypingSpeed = 0.01f;
//   private float typingSpeed;
//   private bool isTyping = false;

//   private Queue<DialogueItem> dialogue = new Queue<DialogueItem>();

//   void Awake()
//   {
//     dialogueButton = GetComponent<Button>();
//     Debug.Log("Awake DialogueManager" + dialogueButton);
//   }

//   void Start()
//   {
//     typingSpeed = defaultTypingSpeed;
//   }

//   public void StartDialogue(Dialogue dialogue)
//   {
//     Debug.Log("Starting dialogue...");
//     this.dialogue.Clear();
//     foreach (DialogueItem item in dialogue.dialogue)
//     {
//       this.dialogue.Enqueue(item);
//     }
//     dialogueBox.SetActive(true);
//     NextDialogueItem();
//   }

//   public void NextDialogueItem()
//   {
//     Debug.Log("Display next dialogue item...");
//     if (isTyping)
//     {
//       typingSpeed = fastTypingSpeed;
//       return;
//     }

//     if (dialogue.Count <= 0)
//     {
//       EndDialogue();
//       return;
//     }

//     DialogueItem item = dialogue.Dequeue();
//     nameText.text = item.name;
//     StartCoroutine(TypeContent(item.content));
//   }

//   public IEnumerator ShowAutoCloseDialogue(DialogueItem item)
//   {
//     UIManagerRef.HideAll();
//     dialogueButton.interactable = false;
//     dialogueBox.SetActive(true);
//     nameText.text = item.name;
//     yield return TypeContent(item.content);
//     typingSpeed = fastTypingSpeed;
//     yield return new WaitForSeconds(0.5f);
//     dialogueBox.SetActive(false);
//     UIManagerRef.ShowBattleUI();
//     dialogueButton.interactable = true;
//   }

//   IEnumerator TypeContent(string content)
//   {
//     isTyping = true;
//     typingSpeed = defaultTypingSpeed;
//     contentText.text = "";

//     foreach (char letter in content.ToCharArray())
//     {
//       contentText.text += letter;
//       yield return new WaitForSeconds(typingSpeed);
//     }
//     isTyping = false;
//   }

//   public void EndDialogue()
//   {
//     Debug.Log("Hide dialogue box...");
//     dialogueBox.SetActive(false);
//   }
// }