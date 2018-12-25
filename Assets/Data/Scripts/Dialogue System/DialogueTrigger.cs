using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue[] dialogue;

    public void TriggerDialogue()
    {
        if(dialogue.Length == 1)DialogueManager.instance.StartDialogue(dialogue[0]);
        if (dialogue.Length > 0)DialogueManager.instance.StartDialogue(dialogue);
    }
}
