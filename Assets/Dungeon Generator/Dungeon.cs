using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DunGen
{
    public class Dungeon : MonoBehaviour
    {
        public int floorCount = 10;
        public Floor currentFloor;
        public List<Floor> floors = new List<Floor>();

        public GameObject floorObject;

        [Header("Stats")]
        public int numberOfFloors = 10;

        public static Dungeon instance;

        GameManager gameManager;

        void Start()
        {
            instance = this;
            gameManager = GameManager.instance;
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < numberOfFloors; i++)
            {
                //floors.Add(Instantiate(floorObject).GetComponent<Floor>());
            }

            if (currentFloor != null) currentFloor.Init();

            //PoolEntities();
        }

        /*
        public void PoolEntities()
        {
            Debug.Log("Pooling entities");

            List<PoolMax> allMaxPoolSizes = new List<PoolMax>();

            //Initialise pools in allMaxPoolSizes
            foreach (ObjectPooler.Pool pool in gameManager.objPooler.enemyPools)
            {
                allMaxPoolSizes.Add(new PoolMax(pool.tag, 0));
            }

            //Check every room for spawnPoints size
            foreach (RoomObject room in currentFloor.allRooms)
            {
                if (room.entitySpawner != null)
                {
                    foreach (EnemyCollection enemArr in room.entitySpawner.enemyTypes)
                    {
                        foreach (PoolMax poolMax in allMaxPoolSizes)
                        {
                            if (poolMax.tag == enemArr.poolTag && enemArr.spawnPoints.Length > poolMax.size)
                            {
                                poolMax.size = enemArr.spawnPoints.Length;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Spawner on room '" + room.name + "' does not exist.");
                }
            }

            //Set entity pool sizes
            foreach (PoolMax poolMax in allMaxPoolSizes)
            {
                gameManager.objPooler.SetPoolSize(poolMax.tag, poolMax.size);
            }
        }
        */


    }
}


public class PoolMax
{
    public string tag;
    public int size;

    public PoolMax(string _tag, int _size)
    {
        tag = _tag;
        size = _size;
    }
}
