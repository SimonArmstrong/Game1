﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() != null) {
            if (collision.GetComponent<Player>() != null) return;
            Entity e = collision.GetComponent<Entity>();
            //e.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
            e.OnHit(-10);
            Rigidbody2D rb = e.GetComponent<Rigidbody2D>();
            if(rb != null)
                rb.AddForce((e.transform.position - transform.position).normalized * 500);
        }
    }
}
