using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup {
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p != null)
        {
            p.OnHit(1);
        }

        base.OnTriggerEnter2D(collision);
    }
}
