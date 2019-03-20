using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLayerSort : MonoBehaviour {
    public int offset;
    // Use this for initialization
    void OnValidate()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100) + offset;
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * GameManager.instance.sortingFidelity) + offset;
    }
}
