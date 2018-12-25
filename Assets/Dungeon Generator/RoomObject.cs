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

        public List<Transform> raycastBounds = new List<Transform>();
        public GridScript AIGrid;

        public GameObject colliderTransform;
        public GameObject mapVisuals;

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
            for (int i = 0; i < transform.childCount; i++)
            {
                SpriteRenderer sr = transform.GetChild(i).GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.enabled = isActiveRoom;
            }
            GetComponent<SpriteRenderer>().enabled = isActiveRoom;

            for (int i = 0; i < northDoors.Count; i++)
            {
                northDoors[i].GetComponent<Collider2D>().enabled = isActiveRoom;
            }
            for (int i = 0; i < southDoors.Count; i++)
            {
                southDoors[i].GetComponent<Collider2D>().enabled = isActiveRoom;
            }
            for (int i = 0; i < eastDoors.Count; i++)
            {
                eastDoors[i].GetComponent<Collider2D>().enabled = isActiveRoom;
            }
            for (int i = 0; i < westDoors.Count; i++)
            {
                westDoors[i].GetComponent<Collider2D>().enabled = isActiveRoom;
            }

            colliderTransform.SetActive(isActiveRoom);

            if (!isActiveRoom)
                initialized = false;

            if (isActiveRoom && !initialized) {
                //AIGrid.Init();
                initialized = true;
            }

        }

        private void OnEnable()
        {
        }

        private void Start()
        {
            //gameObject.SetActive(false);
            AIGrid = GameObject.Find("A_Star_Grid").GetComponent<GridScript>();
        }
    }
}