using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : EntityNEW {
    public Transform trunk;
    public float shakeSpeed = 10;
    public AudioClip hitSound;
    public float shakeModifier = 1;

    private bool shaking = false;
    private float amount = 0.6f;

    public int havestTier = 0;
    public int durability = 3;

    public bool shouldBreak = false;
    public Vector3 playerPos;

    public ParticleSystem leafExplosionEffect;
    public Item woodItem;
    public int itemDropAmount = 5;

    public float fallSpeed = 80;

    bool hasBird = false;
    public GameObject[] birdPrefab;

    public void Start()
    {
        hasBird = Random.Range(0, 10) == 1;
    }

    public override void OnHit(float p_amt)
    {
        Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = hitSound;
        Vector3 rn = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        Instantiate(GameManager.instance.genericHitObject, transform.position + (rn * 0.1f), Quaternion.identity);
        //durability--;
        shaking = true;
        if (hasBird)
        {
            Instantiate(birdPrefab[Random.Range(0, birdPrefab.Length)], transform.position + (Vector3.up * 2), Quaternion.identity);
            hasBird = false;
        }
        amount = 1f * shakeModifier;
        base.OnHit(p_amt);
    }

    float f = 0;
    float breakRot = 0;
    public override void Update()
    {
        if (shaking)
        {
            amount -= Time.deltaTime;
            f += Time.deltaTime * shakeSpeed;
            trunk.localRotation = Quaternion.AngleAxis(Mathf.Sin(f) * (amount * amount), Vector3.forward);
            if (amount <= 0)
            {
                shaking = false;
            }
        }

        if (shouldBreak && durability <= 0) {
            // Was the player to the left of us, or to the right?
            breakRot += (Time.deltaTime * fallSpeed) * (breakRot + 0.1f);
            if (Mathf.Abs(breakRot) >= 106)
            {
                Instantiate(GameManager.instance.genericItemDropObject, transform.position, Quaternion.identity).GetComponent<InventoryPickup>().item = woodItem;
                Destroy(gameObject);
            }
            trunk.localRotation = Quaternion.AngleAxis(breakRot, Vector3.forward);
        }

        base.Update();
    }
}
