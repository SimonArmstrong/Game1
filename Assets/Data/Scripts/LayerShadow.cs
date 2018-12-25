using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerShadow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<LayerSort>().offset = (int)(transform.localPosition.y * GameManager.instance.sortingFidelity) - 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
