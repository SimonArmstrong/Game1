using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DunGen
{
    public class RoomObject : MonoBehaviour
    {
        public List<RoomObject> children;
        public RoomObject parent;
        public int id;

        public List<Transform> northDoors = new List<Transform>();
        public List<Transform> southDoors = new List<Transform>();
        public List<Transform> eastDoors = new List<Transform>();
        public List<Transform> westDoors = new List<Transform>();

        public SpawnPoint[] entitySpawnPoints = new SpawnPoint[0];

        public List<Transform> raycastBounds = new List<Transform>();
        public GridScript AIGrid;

        public GameObject colliderTransform;
        public GameObject mapVisuals;

        public Spawner entitySpawner;
        public EnemySpawner spawner;

        public bool isActiveRoom = false;
        bool initialized = false;


        private void Update()
        {
            for (int x = 0; x < 1; x++)
            {
                for (int i = 0; i < raycastBounds.Count; i++)
                {
                    Vector2 from = raycastBounds[i].position + Vector3.up * ((x) * 0.03f) + Vector3.right * ((x) * 0.03f);
                    Vector2 target = (raycastBounds[(i + 1 > raycastBounds.Count - 1) ? 0 : i + 1].position - raycastBounds[i].position);
                    //Debug.DrawRay(from, target);
                }
            }

            //return;
            
            //if (!isActiveRoom)
            //    initialized = false;

            if (isActiveRoom && !initialized) {
                //AIGrid.Init();
                initialized = true;
            }

        }

        public void OnRoomEnter() {

            isActiveRoom = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                SpriteRenderer sr = transform.GetChild(i).GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.enabled = isActiveRoom;
            }
            GetComponent<SpriteRenderer>().enabled = isActiveRoom;

            colliderTransform.SetActive(isActiveRoom);

            #region SetDoorsTrue
            for (int i = 0; i < northDoors.Count; i++)
            {
                northDoors[i].GetComponent<Collider2D>().enabled = true;
            }
            for (int i = 0; i < southDoors.Count; i++)
            {
                southDoors[i].GetComponent<Collider2D>().enabled = true;
            }
            for (int i = 0; i < eastDoors.Count; i++)
            {
                eastDoors[i].GetComponent<Collider2D>().enabled = true;
            }
            for (int i = 0; i < westDoors.Count; i++)
            {
                westDoors[i].GetComponent<Collider2D>().enabled = true;
            }
            #endregion

            GetComponent<GridScript>().Init();
            PathRequestManager.instance.ClearQueue();
            Camera.main.GetComponent<FollowTarget>().shootRays = false;
            mapVisuals.SetActive(true);
            if (entitySpawner == null) return;
            /*
            foreach (EnemyCollection enemyCollection in entitySpawner.enemyTypes) {
                GameManager.instance.SpawnEntities (enemyCollection.spawnPoints);
            }
            */
        }

        public void OnRoomExit()
        {
            isActiveRoom = false;

            if(GameManager.instance.activeEnemies.Count > 0)
            {
                foreach (GameObject go in GameManager.instance.activeEnemies) {
                    if(go != null && go.activeSelf) go.SetActive(false);
                }
            }

            //GameManager.instance.ClearAllActiveEntities();
            //PathRequestManager.instance.ClearQueue();

            for (int i = 0; i < transform.childCount; i++)
            {
                SpriteRenderer sr = transform.GetChild(i).GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.enabled = isActiveRoom;
            }
            GetComponent<SpriteRenderer>().enabled = isActiveRoom;

            colliderTransform.SetActive(isActiveRoom);

            #region SetDoorsFalse
            for (int i = 0; i < northDoors.Count; i++)
            {
                northDoors[i].GetComponent<Collider2D>().enabled = false;
            }
            for (int i = 0; i < southDoors.Count; i++)
            {
                southDoors[i].GetComponent<Collider2D>().enabled = false;
            }
            for (int i = 0; i < eastDoors.Count; i++)
            {
                eastDoors[i].GetComponent<Collider2D>().enabled = false;
            }
            for (int i = 0; i < westDoors.Count; i++)
            {
                westDoors[i].GetComponent<Collider2D>().enabled = false;
            }
            #endregion

            GetComponent<GridScript>().ClearGrid();
        }

        
    }
}