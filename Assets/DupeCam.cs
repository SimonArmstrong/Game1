using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DupeCam : MonoBehaviour {

    public Camera parentCam;
    public Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        parentCam = transform.parent.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        cam.orthographicSize = parentCam.orthographicSize;
    }
}
