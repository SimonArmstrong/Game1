using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossModel : Enemy {
    public GameObject partPrefab;
    public int maxParts = 20;
    
    public float spawnInterval = 0.5f;
    public float spawnTimer;

    private List<GameObject> parts = new List<GameObject>();

	// Use this for initialization
	public override void Start () {
        mover = InputMovement;
        spawnTimer = spawnInterval;
        parts.Add(gameObject);
	}

    // Update is called once per frame
    public override void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && parts.Count < maxParts) {
            GameObject p = Instantiate(partPrefab, transform.position, Quaternion.identity);
            p.GetComponent<DragonPart>().target = parts[parts.Count - 1].transform;
            parts.Add(p);
            spawnTimer = spawnInterval;
        }
	}
}
