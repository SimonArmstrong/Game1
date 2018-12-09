using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSort : MonoBehaviour {
    public int offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * GameManager.instance.sortingFidelity) + offset;
    }
}
