using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoutDirection{
	up, down, left, right
}		

public class Connection{
	public Vector3 origin, to;
	
	public Connection(Vector3 f, Vector3 t){
		origin = f; to = t;
		
		//if(f == t) 
	}
}

public class Path {
	public List<Vector3> nodes;
	public List<Connection> connections;
	
	public Path(){
		nodes = new List<Vector3>();
		connections = new List<Connection>();
	}
	
	public void AddNode(Vector3 n, Vector3 fr){
		nodes.Add(n);
		//connections.Add(new Connection(fr, to));
	}
}
	
public class Dungeon : MonoBehaviour {
	public GameObject roomObject;
	public GameObject playerSpawnerPrefab;
	public GameObject staircasePrefab;
	[HideInInspector]
	public List<RoomObject> dungeon_rooms = new List<RoomObject>();
	public Room endRoom;
	public List<Path> paths = new List<Path>();
	
	[Header("Dungeon Stats")]
	public int level;
	[Tooltip("Multiplies with level")]
	public float difficulty;
	
	[Header("Generation Settings")]
	[Tooltip("Directly affects the scouts incline to change start rooms")]
	public float noise = 20;
	public Vector2 roomScale = new Vector2(17, 7);
	public int seed;
	public bool randomizeSeed;
	[Tooltip("Number of rooms you desire in this dungeon")]
	public int roomCount = 20;
	
	[Header("Other Settings")]
	public float scoutDelay = 1;
	private int _roomsSoFar = 0;
	public Vector3 centerPoint;
	public float GenerationProgress;
	
	public static Dungeon instance;
	
	public class Scout {
		public ScoutDirection direction;
		public int distanceFromStart = 0;
		public float redness = 0;
		public Vector3 pos;
		public Room startRoom;
		public Path path;
		public List<Room> rooms;
		public float noise;
		int MAX_LOOP = 99;
		//public float scale = 1;
		
		public ScoutDirection GetRandomDirection(){
            ScoutDirection dir = ScoutDirection.up;
			int i = Random.Range(0, 4);
			switch(i){
				case 0:
				dir = ScoutDirection.up;
				break;
				case 1:
				dir = ScoutDirection.down;
				break;
				case 2:
				dir = ScoutDirection.left;
				break;
				case 3:
				dir = ScoutDirection.right;
				break;
			}
			return dir;
		}
		
		public Vector3 GetVectorDirection(ScoutDirection dir){
			Vector3 result = Vector3.zero;
				switch(dir){
					case ScoutDirection.up:
					result = Vector3.up;
					break;
					case ScoutDirection.down:
					result = Vector3.down;
					break;
					case ScoutDirection.left:			
					result = Vector3.left;
					break;
					case ScoutDirection.right:
					result = Vector3.right;
					break;
		
				}
			return result;
		}
		
		Room GetARoom(){
			if(rooms.Count == 0)
				return new Room(Vector3.zero);		// Return the first room
			
			bool getRandomRoom = (Random.Range(0, 100) < noise);
			
			if(getRandomRoom){
				int i = Random.Range(0, rooms.Count - 1);
				return rooms[i];	// Return a random room
			}
			
			return rooms[rooms.Count - 1];	// Return the latest room
		}
		
		public Room Run(){
			startRoom = GetARoom();
			
			Room result = startRoom;
			Room prevRoom = startRoom;
			
			Vector3 fromPos = startRoom.position;
			pos = fromPos;
			
			direction = GetRandomDirection();
			Vector3 dir = GetVectorDirection(direction);
			int mx = 0;
			while(CheckForRoom(dir)){	// While there's a room in the way, find a new direction
				if(CheckForRoom(Vector3.up) && CheckForRoom(Vector3.down) &&
				   CheckForRoom(Vector3.left) && CheckForRoom(Vector3.right))
				{
					startRoom = GetARoom();					
					fromPos = startRoom.position;
					pos = fromPos;
				}
				direction = GetRandomDirection();
				dir = GetVectorDirection(direction);
				mx++;
				if(mx > MAX_LOOP){ 
					Debug.Log("Couldn't find space for a new room");
					break;
				}
			}
			
			if(rooms.Count > 0){
				pos = pos + GetVectorDirection(direction);
				distanceFromStart++;
			}
			
			
			result = new Room(pos);
			
			if(rooms.Count > 0){
				result.ConnectTo(startRoom, direction, true);
				startRoom.ConnectTo(result, direction);
			}
			
			//result.distanceFromStart = distanceFromStart;
			
			return result;
		}
		
		// Returns true if there is a room
		bool CheckForRoom(Vector3 p){
			bool result = false;
			for(int i = 0; i < rooms.Count; i++){			
				if((this.pos + p) == rooms[i].position){
					result = true;
				}
				if(i > MAX_LOOP) break;
			}
			return result;
		}
		
		void ChangeDir(){
            ScoutDirection currentDir = direction;
			while(currentDir == direction)	// Keep runninng if we haven't found a place to put our room yet
				direction = GetRandomDirection();
		}
		
		// Creates a path going in a direction
		void CreateCorridoor(){
			
		}
		
		// Finds a room, taks a new room onto a free side
		void CreateBonusRoom(){
			
		}
		
		// Finds the furthest room from the start, taks the final room onto that room
		void CreateFinalRoom(){
			
		}
		
		public Scout(){
			rooms = new List<Room>();
			pos = Vector3.zero;
		}
	}
	
	public void GetSeed(){
		if(randomizeSeed) seed = Random.Range(0, 9999999);
		Random.seed = seed;
		//GameManager.instance.LoadSeedOnMap(seed);
		//DisplaySeed.UpdateSeed();
	}
	
	public Scout scout;
	float t = 1;
	// Use this for initialization
	void Start () {
		scout = new Scout();
		t = scoutDelay;
		
		instance = this;
		Regenerate();
	}
	
	public void CalculateBounds(){
		Bounds bounds = new Bounds(dungeon_rooms[0].transform.position, Vector3.zero);
		for(int i = 0; i < dungeon_rooms.Count; i++){
			bounds.Encapsulate(dungeon_rooms[i].transform.position);
		}
		
		centerPoint = bounds.center;
	}
	
	public void InitDungeon(){
		if(level == 0){
			//roomCount = (level * 2) + (Random.Range(1, 3));
			return;
		}
		
		roomCount += (level * 2) + (Random.Range(0, 4));
	}
	
	public void IncreaseLevel(){
		level++;
	}
	
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;
		if(t <= 0){
			if(_roomsSoFar < roomCount)
		//		Generate();
			
			t = scoutDelay;
		}
	}
	
	public void ReadMap(){
		for(int i = roomCount - 1; i >= 0; i--){
			dungeon_rooms[i].room.seen = true;
		}
		UpdateRooms();
	}
	
	public void UpdateRooms(){
		for(int i = roomCount - 1; i >= 0; i--){
			dungeon_rooms[i].UpdateDoors();	
		}
	}
	
	public RoomObject GetRoomObjectFromRoom(Room room){
		for(int i = 0; i < roomCount; i++){
			if(ReferenceEquals(room, dungeon_rooms[i].room)){
				return dungeon_rooms[i];
				break;
			}
		}
		return null;
	}
	
	public void Progress(){
	}
	
	public void Regenerate(){
		////////////////////// Dungeon Gen Pipeline ////////////////////////////////////////
		
		GetSeed();
		InitDungeon();
		Generate();
		
		////////////////////////////////////////////////////////////////////////////////////
		CalculateBounds();
	}
	
	public void Generate(){
		//dungeon_rooms.Add();
		
		scout.noise = noise;
		//scout.scale = roomScale;
		bool z = false;//true;
		for(; _roomsSoFar < roomCount;){
			Room r = scout.Run();
			r.distanceFromStart = _roomsSoFar;
			Vector3 spawnPos = ((Vector2)r.position) * roomScale;
			if(z) spawnPos = new Vector3(spawnPos.x, 0, spawnPos.y);
			GameObject inst = Instantiate(roomObject, spawnPos, Quaternion.identity);
			inst.GetComponent<RoomObject>().room = r;
			scout.rooms.Add(r);
			dungeon_rooms.Add(inst.GetComponent<RoomObject>());
			_roomsSoFar++;
		}
		
		dungeon_rooms[0].room.isCurrent = true;
		dungeon_rooms[0].room.seen = true;
		
		UpdateRooms();
		
		HandleRoomRendering();
		
		//playerSpawnerPrefab.transform.position = dungeon_rooms[0].gameObject.transform.position;// + (Vector3.up * 2);
		//Instantiate(staircasePrefab, dungeon_rooms[dungeon_rooms.Count - 1].gameObject.transform.position, Quaternion.identity);
		
		GenerationProgress = (_roomsSoFar / roomCount) * 100;
	}
	
	public void HandleRoomRendering(){
		for(int i = 0; i < dungeon_rooms.Count; i++){
			
		}
	}
}
