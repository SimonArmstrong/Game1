using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSort : MonoBehaviour {
    public int offset;
	// Use this for initialization
	void OnValidate () {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100) + offset;
    }
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * GameManager.instance.sortingFidelity) + offset;
        TrailRenderer tr = GetComponent<TrailRenderer>();
        if (tr == null) return;
        tr.sortingOrder = -(int)(transform.position.y * GameManager.instance.sortingFidelity) + offset;
    }
    
}
