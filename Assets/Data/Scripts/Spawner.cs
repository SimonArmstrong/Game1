using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour { 

    public EnemyCollection[] enemyTypes = new EnemyCollection[0];
    void OnValidate()
    {
        foreach (EnemyCollection ec in enemyTypes)
        {
            if (ec.spawnPoints.Length > 0)
            {
                if (ec.spawnPoints[0] != null)
                {
                    ec.poolTag = ec.spawnPoints[0].tag;
                    ec.name = ec.poolTag;
                }
            }
        }
    }
}

[System.Serializable]
public class EnemyCollection {
    [HideInInspector]
    public string name;
    [HideInInspector]
    public string poolTag;
    public SpawnPoint[] spawnPoints;
}
