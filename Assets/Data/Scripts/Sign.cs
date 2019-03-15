using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable {
    public Dialogue dialogue;
    public override void Interact(Player p)
    {
        DialogueManager.instance.StartDialogue(dialogue);
        base.Interact(p);
    }
}
