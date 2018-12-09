using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour {
	public Vector3 target;
	public float speed;
	public float damp;
	Vector3 velocity;
	public bool locked = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.x, target.y, transform.position.z), ref velocity, damp);	
	}
}
