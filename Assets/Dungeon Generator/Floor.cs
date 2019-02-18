using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DunGen
{
    [System.Serializable]
    public class GenerationData
    {
        public string name;
        public List<Room> possibleRooms = new List<Room>();

        [HideInInspector]
        public List<RoomObject> roomInstances = new List<RoomObject>();
        public int numberOfRooms;

        [HideInInspector]
        public int roomCount;

        [Range(0, 1)]
        public float spawnThreshold;

        public Color color;
    }

    public class Floor : MonoBehaviour
    {
        public List<RoomObject> allRooms = new List<RoomObject>();

        [Range(0.0f, 0.2f)]
        public float separation = 0.1f;
        [Range(0.0f, 1.0f)]
        public float generationDelay = 0.0f;
        public int seed;
        public bool randomizeSeed;
        public bool showGenerationProcess = false;

        public List<GenerationData> mainRoomData = new List<GenerationData>();
        public List<GenerationData> additionalRoomData = new List<GenerationData>();

        public RoomObject startRoom;
        RoomObject currentRoom;

        void Start()
        {
            DunGen.Dungeon.instance.currentFloor = this;
        }

        public void Init()
        {
            startRoom = Instantiate(mainRoomData[0].possibleRooms[0].roomObject, transform.position, Quaternion.identity, transform).GetComponent<RoomObject>();
            mainRoomData[0].roomInstances.Add(startRoom);
            startRoom.OnRoomEnter();
            startRoom.gameObject.GetComponent<GridScript>().Init();
            allRooms.Add(startRoom);
            currentRoom = startRoom;
            if (randomizeSeed) seed = Random.Range(0, 9999999);
            Random.seed = seed;
            int mainRoomCount = 0;
            if (showGenerationProcess) return;
            //SetupData();
            for (int i = 0; i < mainRoomData.Count; i++)
            {
                // CREATE MAIN DUNGEON PATHS
                int threshold = (int)(mainRoomData[i].spawnThreshold * mainRoomData[i].numberOfRooms);
                if (threshold >= allRooms.Count - 1) threshold = allRooms.Count - 1;
                currentRoom = allRooms[Random.Range(threshold, allRooms.Count)];
                while (mainRoomData[i].roomCount < mainRoomData[i].numberOfRooms)
                {
                    GenerationData data = mainRoomData[i];
                    allRooms.Add(Generate(ref data));

                    mainRoomData[i] = data;
                }

                mainRoomCount += mainRoomData[i].numberOfRooms;


            }


            for (int i = 0; i < additionalRoomData.Count; i++)
            {
                // APPEND ADDITIONAL ROOMS BASED ON THRESHOLDS
                while (additionalRoomData[i].roomCount < additionalRoomData[i].numberOfRooms)
                {
                    if (additionalRoomData[i].spawnThreshold >= allRooms.Count - 1)
                    {
                        additionalRoomData[i].spawnThreshold = allRooms.Count - 1;
                    }

                    int threshold = (int)(additionalRoomData[i].spawnThreshold * mainRoomCount);
                    int v = Random.Range(threshold, allRooms.Count - 1);
                    currentRoom = allRooms[v];

                    GenerationData data = additionalRoomData[i];
                    allRooms.Add(Generate(ref data));

                    additionalRoomData[i] = data;
                }
            }
            for(int i = 1; i < allRooms.Count; i++)
            {
                allRooms[i].OnRoomExit();
            }
        }
        void SetupData()
        {

        }
        bool didPlaceRoom = false;
        int mx = 0;
        int nmx = 0;
        int mmax = 20000;
        int max = 5000;
        public RoomObject Generate(ref GenerationData data)
        {
            didPlaceRoom = false;
            //Vector2 direction = RandomDir();
            GameObject toSpawn = null;
            RoomObject newRoom = null;
            Transform me = null;
            Transform other = null;
            int roomAttempts = Random.Range((int)0, (int)data.possibleRooms.Count);
            int direction = Random.Range((int)0, (int)4);
            while (!didPlaceRoom)
            {
                toSpawn = data.possibleRooms[roomAttempts].roomObject;
                newRoom = Instantiate(toSpawn, currentRoom.transform.position, Quaternion.identity, transform).GetComponent<RoomObject>();

                roomAttempts = Random.Range((int)0, (int)data.possibleRooms.Count - 1);
                direction = Random.Range((int)0, (int)4);

                #region Placement
                if (direction == 0)
                {
                    me = currentRoom.northDoors[Random.Range(0, currentRoom.northDoors.Count)];
                    other = newRoom.southDoors[Random.Range(0, newRoom.southDoors.Count)];

                    Vector3 distance = (me.position - other.position);
                    newRoom.transform.position += (distance) + (Vector3)(Vector2.up * separation);
                }
                else if (direction == 1)
                {
                    me = currentRoom.southDoors[Random.Range(0, currentRoom.southDoors.Count)];
                    other = newRoom.northDoors[Random.Range(0, newRoom.northDoors.Count)];

                    Vector3 distance = (me.position - other.position);
                    newRoom.transform.position += (distance) + (Vector3)(Vector2.down * separation);
                }
                else if (direction == 2)
                {
                    me = currentRoom.westDoors[Random.Range(0, currentRoom.westDoors.Count)];
                    other = newRoom.eastDoors[Random.Range(0, newRoom.eastDoors.Count)];

                    Vector3 distance = (me.position - other.position);
                    newRoom.transform.position += (distance) + (Vector3)(Vector2.left * separation);
                }
                else if (direction == 3)
                {
                    me = currentRoom.eastDoors[Random.Range(0, currentRoom.eastDoors.Count)];
                    other = newRoom.westDoors[Random.Range(0, newRoom.westDoors.Count)];

                    Vector3 distance = (me.position - other.position);
                    newRoom.transform.position += (distance) + (Vector3)(Vector2.right * separation);
                }
                #endregion

                RoomObject obstacle = CheckIfRoomCanSpawn(newRoom);
                if (obstacle == null)
                {
                    didPlaceRoom = true;
                    me.gameObject.SetActive(true);
                    other.gameObject.SetActive(true);

                    other.GetComponent<Door>().to = currentRoom;
                    me.GetComponent<Door>().from = currentRoom;
                    currentRoom = newRoom;
                    data.roomInstances.Add(currentRoom);
                    data.roomCount++;
                    currentRoom.GetComponent<SpriteRenderer>().color = new Color(data.color.r, data.color.g, data.color.b, data.color.a);
                    other.GetComponent<Door>().from = currentRoom;
                    me.GetComponent<Door>().to = currentRoom;
                }
                else
                {
                    me = null;
                    other = null;
                    didPlaceRoom = false;
                    DestroyImmediate(newRoom.gameObject);
                }

                #region Loop exceptions
                mx++;
                nmx++;
                if (mx >= max)
                {
                    if (data.roomInstances.Count > 0)
                        currentRoom = data.roomInstances[Random.Range(0, data.roomInstances.Count - 1)];
                    else
                        currentRoom = allRooms[0];
                    mx = 0;
                    //return null;
                }

                if (nmx >= mmax)
                {
                    Debug.LogError("Can't fit [" + data.name + "] dungeon");
                    nmx = 0;
                    return null;
                }
                #endregion
            }
            return newRoom;
        }

        public RoomObject CheckIfRoomCanSpawn(RoomObject newRoom)
        {
            for (int x = 0; x < 1; x++)
            {
                for (int i = 0; i < newRoom.raycastBounds.Count; i++)
                {
                    Vector2 from = newRoom.raycastBounds[i].position;// + Vector3.up * ((x) * 0.03f) + Vector3.right * ((x) * 0.03f);
                    Vector2 target = (newRoom.raycastBounds[(i + 1 > newRoom.raycastBounds.Count - 1) ? 0 : i + 1].position - newRoom.raycastBounds[i].position);
                    RaycastHit2D[] hit = Physics2D.CircleCastAll(from, 0.25f, target, target.magnitude);
                    if (hit.Length > 0)
                    {
                        for (int j = 0; j < hit.Length; j++)
                        {
                            RoomObject obx = hit[j].collider.GetComponent<RoomObject>();
                            if (obx != null)
                            {
                                if (obx != newRoom)
                                {
                                    //Debug.Log("Room [" + newRoom.ToString() + "] Hit Room [" + hit[j].collider.GetComponent<RoomObject>().ToString() + "]");
                                    return hit[j].collider.GetComponent<RoomObject>();
                                }
                            }
                        }
                    }
                }
            }

            //Debug.Log(newRoom.ToString() + " Was placed");
            return null;
        }

        float timer = 0;
        int mainRoomccc = 0;
        bool mainRoomsDone = false;
        bool addRoomsDone = false;

        private void Update()
        {
            if (!showGenerationProcess) return;
            timer -= Time.deltaTime;
            if (addRoomsDone)
            {
                return;
            }
            if (timer <= 0)
            {
                timer = generationDelay;
                for (int i = 0; i < mainRoomData.Count; i++)
                {
                    if (mainRoomData[i].roomCount < mainRoomData[i].numberOfRooms)
                    {
                        int threshold = (int)(mainRoomData[i].spawnThreshold * mainRoomData[i].numberOfRooms);
                        if (threshold >= allRooms.Count - 1) threshold = allRooms.Count - 1;
                        currentRoom = allRooms[Random.Range(threshold, allRooms.Count)];
                        GenerationData data = mainRoomData[i];
                        allRooms.Add(Generate(ref data));

                        mainRoomccc++;
                        mainRoomData[i] = data;
                    }
                    else { mainRoomsDone = true; }
                }
                if (mainRoomsDone)
                {
                    for (int i = 0; i < additionalRoomData.Count; i++)
                    {
                        if (additionalRoomData[i].roomCount < additionalRoomData[i].numberOfRooms)
                        {
                            if (additionalRoomData[i].spawnThreshold >= allRooms.Count - 1)
                            {
                                additionalRoomData[i].spawnThreshold = allRooms.Count - 1;
                            }

                            int threshold = (int)(additionalRoomData[i].spawnThreshold * mainRoomccc);
                            int v = Random.Range(threshold, allRooms.Count - 1);
                            currentRoom = allRooms[v];
                            GenerationData data = additionalRoomData[i];
                            allRooms.Add(Generate(ref data));

                            additionalRoomData[i] = data;
                        }
                        else { addRoomsDone = true; }
                    }

                }

            }
        }

    }
}