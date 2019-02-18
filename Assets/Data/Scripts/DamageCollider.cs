﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour {
    public float lifetime;
    public bool doesDestroy = false;
    public bool doesPierce = true;

    public float damage;
    public Entity owner;
    public List<StatusCondition> conditions = new List<StatusCondition>();
    public bool knockbackFromOwner = true;

    public float height = 0;

    public AnimatedDamageCollider adc;

    public float heightBonus = 0.3f;

    Vector3 prevPos;
    private void Update()
    {
        prevPos = ((owner != null && knockbackFromOwner) ? owner.transform.position : transform.position);
        if (!doesDestroy) return;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        EntityNEW e = collision.GetComponent<EntityNEW>();
        if (e != null) {
            if (e != owner)
            {
                if (Mathf.Abs((height - e.height2D)) <= heightBonus)
                {
                    // We can change total damage recieved here!!!!
                    damage = (damage + (Random.Range(-1f, 1f)));
                    e.OnHit(-damage);
                    if (e.GetComponent<Rigidbody2D>() != null)
                        e.GetComponent<Rigidbody2D>().AddForce((e.transform.position - prevPos).normalized * 100);
                    if (!doesPierce)
                    {
                        gameObject.SetActive(false);
                        //Destroy(gameObject);
                    }
                    if (adc != null)
                        adc.hitInfo.Add(e.gameObject);
                }
            }
        }
    }
}
