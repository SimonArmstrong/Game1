using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour {
    public GameObject damageCollider;
    public AudioClip crackSound;
    public AudioClip breakSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity e = collision.GetComponent<Entity>();
        if (e != null) {
            GetComponent<Animator>().SetTrigger("fall");
        }
    }

    public void StartCrack() {
        AudioSource a;
        (a = Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>()).clip = crackSound;
        a.volume = 0.3f;
    }

    public void DamageCollider() {
        AudioSource a;
        Instantiate(damageCollider, transform.position, Quaternion.identity);
        (a = Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>()).clip = breakSound;
        a.volume = 0.3f;
    }
}
