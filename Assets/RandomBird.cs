using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBird : MonoBehaviour {
    public List<GameObject> birds = new List<GameObject>();

    public float timer = 30;

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer = 30;
            if (Random.Range(0, 3) > 1) {
                Instantiate(birds[Random.Range(0, birds.Count)], transform.position, Quaternion.identity);
            }
        }
    }
}
