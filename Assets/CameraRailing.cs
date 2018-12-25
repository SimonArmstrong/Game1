using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail {
    public Vector2 direction;
    public List<Vector3> positions = new List<Vector3>();

    public Vector2 GetDirection() {
        Vector3 from = positions[0];
        Vector3 to = positions[positions.Count - 1];
        direction = to - from;
        return direction;
    }
}

public class CameraRailing : MonoBehaviour {
    public GameObject cam;
    public List<Transform> points = new List<Transform>();

    public Vector3 snappedPosition;

	// Update is called once per frame
	void Update () {
        Vector3 position = points[0].position;

        for (int i = 0; i < points.Count; i++) {
            snappedPosition = points[i].position;
        }
	}

    Vector3 FindNearestRailingPosition() {
        Vector3 pos = Vector3.zero;

        return pos;
    }
}
