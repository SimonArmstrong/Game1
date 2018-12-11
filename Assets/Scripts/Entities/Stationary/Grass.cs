using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Entity {
    public override void Die()
    {
        GetComponent<TinkerAnimatorBasic>().currentAnimation = 1;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<TinkerAnimatorBasic>().destroyAfterPlayed = true;
    }
}
