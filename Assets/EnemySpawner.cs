using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public int count;
    public float interval;
    public float squaredArea = 1;
    public int spawnedCount = 0;

    float initInterval;
    float timer;

	// Use this for initialization
	void Start () {
        initInterval = interval + 3;
        timer = initInterval;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnedCount >= count) return;
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer = interval;
            GameObject f = Instantiate(enemy, transform);
            f.transform.localPosition = new Vector2(Random.Range(-squaredArea / 2, squaredArea / 2), Random.Range(-squaredArea / 2, squaredArea / 2));
            spawnedCount++;
        }
	}
}
