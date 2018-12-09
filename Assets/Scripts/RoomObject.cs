using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour {
	public Room room;
	public List<GameObject> plaques = new List<GameObject>();
	
	public SpriteRenderer mapRenderer;
		
	[Header("Doors")]
	public GameObject doorUp;
	public GameObject doorDown;
	public GameObject doorLeft;
	public GameObject doorRight;
	
	public float distFrmStrt;
	
	// Use this for initialization
	public void UpdateDoors () {	
		
		if(room.isCurrent){
			bool up = room.roomUp != null;
			doorUp.SetActive(up);
			if(up) doorUp.GetComponent<Door>().roomObject = Dungeon.instance.GetRoomObjectFromRoom(room.roomUp);
			
			bool dwn = room.roomDown != null;
			doorDown.SetActive(dwn);
			if(dwn) doorDown.GetComponent<Door>().roomObject = Dungeon.instance.GetRoomObjectFromRoom(room.roomDown);
			
			bool lft = room.roomLeft != null;
			doorLeft.SetActive(lft);
			if(lft) doorLeft.GetComponent<Door>().roomObject = Dungeon.instance.GetRoomObjectFromRoom(room.roomLeft);
			
			bool rht = room.roomRight != null;
			doorRight.SetActive(rht);
			if(rht) doorRight.GetComponent<Door>().roomObject = Dungeon.instance.GetRoomObjectFromRoom(room.roomRight);
			
			distFrmStrt = room.distanceFromStart;
			float r = (distFrmStrt / 10);
			
		}
		
		//transform.position = room.position;
		if(GetComponent<SpriteRenderer>() == null) return;
		
		GetComponent<SpriteRenderer>().enabled = room.isCurrent;
		foreach(SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>()){
			if(r.gameObject.layer != 9)
				r.enabled = room.isCurrent;
		}
		
		//mapRenderer.enabled = (room.seen);
	}
	
	void OnEnable(){
		int i = Random.Range(0, 6);
		if(i >= plaques.Count) return;
		Instantiate(plaques[i], transform.position, Quaternion.identity, transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
