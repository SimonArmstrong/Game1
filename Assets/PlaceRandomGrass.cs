using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomGrass : MonoBehaviour {
    public GameObject grassObject;
	void Start () {
        for (int i = 0; i < GetComponent<Grid>().gridPoints.Count; i++) {
            float f = Random.Range(0.0f, 10.0f);
            if (f > 4.5f) {
                Instantiate(grassObject, GetComponent<Grid>().gridPoints[i], Quaternion.identity);
            }
        }
	}
}
