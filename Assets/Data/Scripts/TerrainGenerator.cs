using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    [System.Serializable]
    public class Octave
    {
        public float sampleX;
        public float sampleY;

        public float offsetX;
        public float offsetY;

        public float amplitude;

        [Range(0, 1)]
        public float spawnThreshold;

        public GameObject tilePrefab;
    }
    public List<Octave> octaves = new List<Octave>();

	void Start () {
        for (int i = 0; i < octaves.Count; i++) {
            for (int x = 0; x < 20; x++) {
                for (int y = 0; y < 20; y++) {
                    float height = Mathf.PerlinNoise(x * octaves[i].sampleX, y * octaves[i].sampleY) * octaves[i].amplitude;
                    Debug.Log(height);
                    if (height >= octaves[i].spawnThreshold) {
                        Instantiate(octaves[i].tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
