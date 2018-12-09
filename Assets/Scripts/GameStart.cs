using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
    public int scene;
	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        
        SceneManager.sceneLoaded += load;
    }

    void load(Scene s, LoadSceneMode l) {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(scene));
    }

	// Update is called once per frame
	void Update () {
    }
}
