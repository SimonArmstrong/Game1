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

    public override void FixedUpdate() { 
        if(model != null)
            rootMotionSpeed = model.baseModel.GetRootMotion();
        base.FixedUpdate();
    }

    public override void Die()
    {
        for (int i = 0; i < drops.Length; i++) {
            Instantiate(drops[i], transform.position, Quaternion.identity);
        }
        Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = deathSound;

        GetComponent<Collider2D>().enabled = false;
        base.Die();
    }
}
