using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : MonoBehaviour {
    public Sprite[] obstacles;
    int i = 0;
    float r = 0;
    public float tintedChance = 0.5f;

	// Use this for initialization
	void Start () {
        if (obstacles.Length <= 0) return;
        i = Random.Range(0, obstacles.Length);
        GetComponent<SpriteRenderer>().sprite = obstacles[i];
        r = Random.Range(0, 100);
        if (r <= tintedChance) {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.8f, 0.8f, 1.0f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
