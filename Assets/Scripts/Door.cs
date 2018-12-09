using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public RoomObject roomObject;
	public Transform spawnPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null) {
            other.transform.position = spawnPoint.position;
            roomObject.room.isCurrent = true;
            roomObject.UpdateDoors();
        }
    }
}
