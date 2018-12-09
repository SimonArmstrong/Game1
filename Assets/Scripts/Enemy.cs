using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {
    public GameObject[] drops;
    public AudioClip deathSound;
    public override void Start() {
        base.Start();

        mover = StandStill;
    }
    public override void Die()
    {
        for (int i = 0; i < drops.Length; i++) {
            Instantiate(drops[i], transform.position, Quaternion.identity);
        }
        Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = deathSound;
        base.Die();
    }
}
