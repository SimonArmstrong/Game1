using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPart : MonoBehaviour {
    public Transform target;
    public float partDistance = 0.75f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float z = Mathf.Atan2(target.transform.position.y, target.transform.position.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));

        Vector3 diff = target.position - transform.position;
        float dist = (diff).magnitude;
        if (dist >= partDistance)
        {
            transform.position += diff.normalized * Time.deltaTime * 3;
        }
    }
}
