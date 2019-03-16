using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public List<Vector3> gridPoints = new List<Vector3>();

    public Vector3 nearestGridPoint;

    public float scale = 1;  //change this to determine the gap between grid points
    public float g_width;
    public float g_height;

    [Header("Debug")]
    public bool debug;
    public GameObject debugObject;
    public List<GameObject> debugObjInstances;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < g_width; i++) {
            for (int j = 0; j < g_height; j++) {
                Vector3 pos = transform.position + new Vector3(i * scale, j * scale);
                gridPoints.Add(pos);
                if(debug)
                    debugObjInstances.Add(Instantiate(debugObject, pos, Quaternion.identity));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetNearestNode(Camera.main.ScreenPointToRay(Input.mousePosition).origin);
    }

    public Vector3 GetNearestNode(Vector3 pos){     //get nearest node to vector3 pos
        Vector3 result = new Vector3();
        float nearest = 99999;
        int nearestIndex = -1;
        for (int i = 0; i < gridPoints.Count; i++) {
            float distance = (pos - gridPoints[i]).magnitude;
            if(distance < nearest){
                nearest = distance;
                nearestIndex = i;
                result = gridPoints[i];
            }
        }
        if (debug)
        {
            for (int i = 0; i < gridPoints.Count; i++)
            {
                if (i != nearestIndex)
                    debugObjInstances[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
            }

            debugObjInstances[nearestIndex].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        }
        nearestGridPoint = result;
        return result;
    }
}
