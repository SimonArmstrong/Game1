using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDamageCollider : MonoBehaviour {
    public List<GameObject> colliders = new List<GameObject>();

    public Entity owner;
    public float damage;
    public List<StatusCondition> conditions = new List<StatusCondition>();

    public int currentFrame;

    private void Start()
    {
        for (int i = 0; i < colliders.Count; i++) {
            if (colliders[i] != null) {
                DamageCollider dc = colliders[i].GetComponent<DamageCollider>();
                dc.owner = owner;
                dc.damage = damage;
                if(conditions.Count > 0)
                    dc.conditions.AddRange(conditions);
            }
        }

        gameObject.SetActive(false);
    }

    public void End() {
        for (int i = 0; i < colliders.Count; i++) {
            if (colliders[i] != null)
                colliders[i].SetActive(false);
        }
    }

    private void Update()
    {
        currentFrame = owner.model.frameIndex;

        for (int i = 0; i < colliders.Count; i++) {
            if(colliders[i] != null && i != currentFrame)
                colliders[i].SetActive(false);
        }

        if (colliders[currentFrame] != null)
            colliders[currentFrame].SetActive(true);
    }
}
