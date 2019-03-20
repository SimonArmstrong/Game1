using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour {
    private void Start()
    {
        transform.position = Grid.instance.GetNearestNode(transform.position);
    }
}
