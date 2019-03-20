using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : Hittable {
    public float shakeModifier = 5;
    public float amount = 0;
    public float shakeSpeed = 80;
    public bool shaking = false;
    public Transform axis;
    public AudioClip hitSound;

    public override void OnHit(float p_amt)
    {
        GameManager.instance.SpawnDmgNum(transform, Mathf.Abs(Mathf.RoundToInt(p_amt)));
        shaking = true;
        Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = hitSound;
        Vector3 rn = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        Instantiate(GameManager.instance.genericHitObject, transform.position + (rn * 0.1f), Quaternion.identity);
        amount = 1f * shakeModifier;
        base.OnHit(p_amt);
    }

    float f = 0;
    public void Update()
    {
        if (shaking)
        {
            amount -= Time.deltaTime;
            f += Time.deltaTime * shakeSpeed;
            axis.localRotation = Quaternion.AngleAxis(Mathf.Sin(f) * (amount * amount), Vector3.forward);
            if (amount <= 0)
            {
                shaking = false;
            }
        }
    }
}
