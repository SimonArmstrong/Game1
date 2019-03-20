using System.Collections;
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
        {
            if (adc != null) {
                adc.hitInfo.Clear();
            }
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        EntityNEW e = collision.GetComponent<EntityNEW>();
        if (e != null) {
            if (e != owner)
            {
                if (adc != null) {
                    for (int i = 0; i < adc.hitInfo.Count; i++) {
                        if (adc.hitInfo[i] == e.gameObject) return;
                    }
                }
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
                        if (adc == null) Destroy(gameObject);
                    }
                    if (adc != null)
                        adc.hitInfo.Add(e.gameObject);
                }
            }
            return;
        }

        Hittable h = collision.GetComponent<Hittable>();
        if (h != null) {
            if (adc != null)
            {
                for (int i = 0; i < adc.hitInfo.Count; i++)
                {
                    if (adc.hitInfo[i] == h.gameObject) return; // Add to hit objects for this swing
                }
            }

            damage = (damage + (Random.Range(-1f, 1f)));
            h.OnHit(-damage);

            if (adc != null)
                adc.hitInfo.Add(h.gameObject);
        }
    }
}
