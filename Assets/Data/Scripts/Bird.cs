using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : EntityNEW {
    public float x, y;
    public float amplitude;
    public float moveSpeed = 5;

    float n, w;
    float randY, randX;

    float lifetime = 60;
    public void Start()
    {
        randX = Random.Range(-100f, 100f);
        randY = Random.Range(-100f, 100f);
    }

    public override void OnHit(float p_amt)
    {
        Kill();
        base.OnHit(p_amt);
    }

    public void Kill() {
        Destroy(gameObject);
    }

    public override void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Kill();
        n += Time.deltaTime;
        w -= Time.deltaTime;

        y = (Mathf.PerlinNoise((randX - n) * 0.1f, (randY + w) * 0.1f) - 0.5f) * amplitude;
        x = (Mathf.PerlinNoise((randX + n) * 0.1f, (randY - w) * 0.1f) - 0.5f) * amplitude;

        Vector3 movement = new Vector3(x, y, 0).normalized;
        transform.position += movement * Time.deltaTime * moveSpeed;

        if (movement.x > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (movement.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        base.Update();
    }
}
