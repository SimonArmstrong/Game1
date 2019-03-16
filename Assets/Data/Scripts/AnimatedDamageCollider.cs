using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Colliders {
    public List<GameObject> colliders = new List<GameObject>();
}

public class AnimatedDamageCollider : MonoBehaviour {
    public List<Colliders> colliders = new List<Colliders>();
    public Entity owner;
    public float damage;
    public List<StatusCondition> conditions = new List<StatusCondition>();

    public Vector3 swingDirection;
    public GameObject projectile;

    public int currentFrame;
    public int currentCombo = 0;

    public float height = 0f;

    public List<GameObject> hitInfo = new List<GameObject>();

    public void Init()
    {
        for (int i = 0; i < colliders[currentCombo].colliders.Count; i++)
        {
            if (colliders[currentCombo].colliders[i] != null)
            {
                DamageCollider dc = colliders[currentCombo].colliders[i].GetComponent<DamageCollider>();
                dc.owner = owner;
                dc.adc = this;
                dc.damage = damage;
                dc.height = height;
                if (conditions.Count > 0)
                    dc.conditions.AddRange(conditions);
            }
        }
        
        gameObject.SetActive(false);
    }

    public void End() {
        for (int i = 0; i < colliders[currentCombo].colliders.Count; i++)
        {
            if (colliders[currentCombo].colliders[i] != null)
                colliders[currentCombo].colliders[i].SetActive(false);
        }
        hitInfo.Clear();
    }

    private void Update()
    {
        currentFrame = owner.model.frameIndex;

        for (int i = 0; i < colliders[currentCombo].colliders.Count; i++)
        {
            if (colliders[currentCombo].colliders[i] != null) {
                DamageCollider dc = colliders[currentCombo].colliders[i].GetComponent<DamageCollider>();
                if (i != currentFrame)
                    colliders[currentCombo].colliders[i].SetActive(false);
            }
        }

        if (colliders[currentCombo].colliders[currentFrame] != null)
        {
            if (colliders[currentCombo].colliders[currentFrame].GetComponentInChildren<ShootProjectile>() != null) {
                ShootProjectile sp = colliders[currentCombo].colliders[currentFrame].GetComponentInChildren<ShootProjectile>();
                sp.direction = swingDirection;
                sp.projectile = projectile;
                sp.owner = owner;
            }
            colliders[currentCombo].colliders[currentFrame].SetActive(true); 
        }
    }
}
