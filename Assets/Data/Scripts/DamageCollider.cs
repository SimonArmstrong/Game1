using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour {
    public float lifetime;
    public bool doesDissapear = false;

    public float damage;
    public Entity owner;
    public List<StatusCondition> conditions = new List<StatusCondition>();

    private void Update()
    {
        if (!doesDissapear) return;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }
}
