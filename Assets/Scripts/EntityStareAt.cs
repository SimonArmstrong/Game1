using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStareAt : MonoBehaviour {
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            target = GameManager.instance.player.GetComponent<Entity>().transform;

        GetComponent<Entity>().LookAt(target.position);
	}
}
