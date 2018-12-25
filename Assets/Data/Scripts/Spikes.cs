using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    public float forceMultiplier = 500;

    Collider2D col;
    Animator anim;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("trigger");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleDamageCollider()
    {
        col.enabled = !col.enabled;
    }

    void OnTriggerEnter2D(Collider2D go)
    {
        Entity en = go.GetComponent<Entity>();
        if (en == null) return;
        en.hp -= 1.0f;
        en.GetComponent<Rigidbody2D>().AddForce((en.shadow.transform.position - transform.position).normalized * forceMultiplier);
    }

    //void RemoveArrayElemAt(int element, var[] arr)
    //{
    //    var[] finalArr = new var[arr.Length - 1];
    //}
}
