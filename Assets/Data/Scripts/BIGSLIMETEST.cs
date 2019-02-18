using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIGSLIMETEST : MonoBehaviour {
    public Transform player;
	// Use this for initialization
	void Start () {
        player = GameManager.instance.player.transform;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<NPC>().dirVec = (player.position - transform.position);
	}
}
