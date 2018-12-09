using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInstance : MonoBehaviour {
    public Weapon weapon;
    public GameObject damageCollider;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Swing() {
        damageCollider.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
