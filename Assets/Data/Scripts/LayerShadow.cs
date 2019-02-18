using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerShadow : MonoBehaviour {
    public Transform target;
    public int offset = -7;
    public float height2D = 0;

    public void Update()
    {
        if (target == null) { Destroy(gameObject); return; }
        transform.position = (target.position - (new Vector3(0, height2D / 2, 0)));

        GetComponent<LayerSort>().offset = offset;
    }
}
