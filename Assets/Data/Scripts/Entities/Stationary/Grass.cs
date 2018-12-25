using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Entity {
    [System.Serializable]
    public class Drop {
        public GameObject drop;
        public float dropChance = 0.1f;
    }

    public Drop[] drops;
    public bool dropped;

    void DropRandomItem() {
        dropped = true;
        for (int i = 0; i < drops.Length; i++)
        {
            float rng = Random.Range(0.0f, 100.0f) * 00.1f;
            if (rng <= drops[i].dropChance)
            {
                Instantiate(drops[i].drop, transform.position, Quaternion.identity);
                return;
            }
        }
    }

    public override void Die()
    {
        if (dropped != true)
        {
            DropRandomItem();
        }


        GetComponent<TinkerAnimatorBasic>().currentAnimation = 1;
        GetComponent<TinkerAnimatorBasic>().frameIndex = 0;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<TinkerAnimatorBasic>().destroyAfterPlayed = true;
    }
}
