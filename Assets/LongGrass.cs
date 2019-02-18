using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGrass : MonoBehaviour2D
{
    public Transform axis;
    public float shakeSpeed = 10;
    public AudioClip hitSound;
    public float shakeModifier = 1;

    private bool shaking = false;
    private float amount = 0.6f;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() != null)
        {
            Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = hitSound;
            shaking = true;
            amount = 1f * shakeModifier;
        }
        
        if (collision.GetComponent<DamageCollider>() != null)
        {
            Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = hitSound;
            axis.gameObject.SetActive(false);
        }
    }

    float f = 0;
    public override void Update()
    {
        if (shaking)
        {
            amount -= Time.deltaTime;
            f += Time.deltaTime * shakeSpeed;
            axis.localRotation = Quaternion.AngleAxis(Mathf.Sin(f) * (amount * amount), Vector3.forward);
            if (amount <= 0)
                shaking = false;
        }
        base.Update();
    }
}
