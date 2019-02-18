using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageCollider {
    public Vector3 direction;
    public float speed = 20;

    public AudioClip launchSound;
    public AudioClip hitSound;

    private void OnEnable()
    {
        AudioSource a = Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        a.clip = launchSound;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Entity e = collision.GetComponent<Entity>();
        if (e != null)
        {
            if (e != owner)
            {
                if (!doesPierce)
                {
                    QueDeath();
                }
            }
        }
        if (collision.gameObject.tag == "Wall") {
            QueDeath();
        }
    }

    void QueDeath() {
        speed = 0;
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        AudioSource a = Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        a.clip = hitSound;
    }

    // Update is called once per frame
    void FixedUpdate () {
        lifetime -= Time.deltaTime;
        direction.Normalize();
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        transform.position += direction * speed * Time.deltaTime;
        if (lifetime <= 0) {
            speed = 0;
            //GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
	}
}
