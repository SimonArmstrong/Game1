using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rupee : Pickup {
    public int amount;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p != null)
        {
            p.money += amount;
        }
        base.OnTriggerEnter2D(collision);
    }
}
