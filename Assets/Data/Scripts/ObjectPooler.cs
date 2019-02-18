using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public List<Pool> enemyPools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void Init () {



        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            poolDictionary.Add(pool.tag, objectPool);
            SetPoolSize(pool.tag, pool.size);
        }
        foreach (Pool pool in enemyPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            poolDictionary.Add(pool.tag, objectPool);
            SetPoolSize(pool.tag, pool.size);
        }

    }

    public GameObject GetFromPool (string tag, bool active, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        //Get objectToSpawn from respective poolDictionary
        GameObject objectToSpawn = null;
        if (poolDictionary[tag].Count < 0)
            Debug.LogWarning("Queue with tag " + tag + "is empty.");

        objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        if (active)objectToSpawn.SetActive(true);

        //Put objectToSpawn back into the top of the respective poolDictionary
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }	

    public void SetPoolSize(string poolTag, int newSize)
    {
        if (poolDictionary[poolTag] == null) Debug.LogWarning("Cannot find pool in dictionary");



        Pool poolToExpand = GetPoolFromTag(poolTag);

        if (poolDictionary[poolTag].Count > 0)
        {
            int poolCount = poolDictionary[poolTag].Count;
            //Debug.Log("Clearing Queue at poolTag in poolDictionary");
            for (int i = 0; i < poolCount; i++)
            {
                GameObject go = poolDictionary[poolTag].Dequeue();
                Destroy(go);
                //Debug.Log("Destroyed GameObject");
            }

        }
        //Debug.Log("Adding " + newSize + " " + poolTag + "(s) to respective queue in poolDictionary");
        for(int i = 0; i < newSize; i++)
        {
            GameObject obj = Instantiate(poolToExpand.prefab);
            if (poolTag == "dmg_num") obj.transform.SetParent(GameManager.instance.canvas.transform);
            obj.SetActive(false);
            poolDictionary[poolTag].Enqueue(obj);
        }
        poolToExpand.size = newSize;


    }

    private Pool GetPoolFromTag(string poolTag)
    {
        foreach (Pool pool in pools)
        {
            if (pool.tag == poolTag)
            {
                return pool;
            }
        }
        foreach (Pool pool in enemyPools)
        {
            if (pool.tag == poolTag)
            {
                return pool;
            }
        }
        Debug.LogWarning("Could not find pool from tag");
        return null;
    }
}
