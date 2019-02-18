using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : Interactable {
    public GameObject miniGamePrefab;
    public override void Interact(Player p) {
        // Begin Fishing Mini Game
        Instantiate(miniGamePrefab);
        base.Interact(p);
    }
}
