using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TempCharacterContainer : NetworkBehaviour {
    public CharacterData character;
    public GameObject playerController;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
	}

    void InitPlayerController() {
        Instantiate(playerController);
        Destroy(gameObject);
    }
}
