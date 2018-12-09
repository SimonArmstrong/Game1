using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room {
	public List<Door> doors;
	
	public float distanceFromStart = 0;
	
	public Vector3 position;
	
	[Header("Neighbours")]
	public Room roomUp;
	public Room roomDown;
	public Room roomLeft;
	public Room roomRight;

	
	public bool isCurrent = false;
	public bool seen = false;
	
	void Awake() {
		
	}
	
	
	public void ConnectTo(Room r, ScoutDirection dir, bool negativeDir = (false)){
		switch(dir){
			case ScoutDirection.up:
			if(negativeDir) {roomDown = r; break;}
			roomUp = r;
			//doorUp.GetComponent<SpriteRenderer>().enabled = (true);
			break;
			case ScoutDirection.down:
			if(negativeDir) {roomUp = r; break;}
			roomDown = r;
			//doorDown.GetComponent<SpriteRenderer>().enabled = (true);
			break;
			case ScoutDirection.left:
			if(negativeDir) {roomRight = r; break;}
			roomLeft = r;
			//doorLeft.GetComponent<SpriteRenderer>().enabled = (true);
			break;
			case ScoutDirection.right:
			if(negativeDir) {roomLeft = r; break;}
			roomRight = r;
			//doorRight.GetComponent<SpriteRenderer>().enabled = (true);
			break;
		}
	}

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public Room(Vector3 pos){
		doors = new List<Door>();
		position = pos;
	}
}
