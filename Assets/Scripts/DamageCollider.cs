using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour {
    float lifetime = 0.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity e = collision.GetComponent<Entity>();
        if (e != null) {
            e.Health(-1);
            e.GetComponent<Rigidbody2D>().AddForce((e.shadow.transform.position - transform.position).normalized * 300);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }
}
