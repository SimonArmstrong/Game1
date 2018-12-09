using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemyPrefab;

    StateController enemyController;
    GameManager gameManager;

	// Use this for initialization
	void Start () {

        gameManager = GameManager.instance;

        //player = gameManager.player;

        //if(enemyPrefab != null) enemyController = Instantiate(enemyPrefab, new Vector3(0, 3, 0), Quaternion.identity).GetComponent<StateController>();
        //if (player != null) Debug.Log("player exists");
        //if (player != null) enemyController.SetupAI(true, player.transform);
	}

}
