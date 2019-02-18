using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour {
    public GameObject projectile;
    public Vector3 direction;
    public Entity owner;
    private void OnEnable()
    {
        if (projectile == null) return;

        Projectile p = Instantiate(projectile, transform.parent.parent.position, Quaternion.identity).GetComponent<Projectile>();
        p.direction = direction;
        p.GetComponent<DamageCollider>().owner = owner;
        
    }
}
