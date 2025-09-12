using UnityEngine;

class DialogueTrigger : MonoBehaviour {
  public DialogueManager manager;
  public Dialogue dialogue;
  
  public void TriggerDialogue() {
    manager.StartDialogue(dialogue);
  }
}
