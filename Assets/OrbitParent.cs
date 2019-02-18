using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitParent : MonoBehaviour {
    public float radius = 0.5f;
    public float speed = 5.0f;
    public float heightOffset = 0.2f;
    float x, y;

    // Update is called once per frame
    private void Start()
    {
        x = transform.localPosition.x;
        y = transform.localPosition.y;
    }

    void Update () {
        x += Time.deltaTime * speed;
        y += Time.deltaTime * speed;
        transform.localPosition = new Vector3(Mathf.Sin(x) * radius, heightOffset + -Mathf.Cos(x) * radius, 0);
	}
}
