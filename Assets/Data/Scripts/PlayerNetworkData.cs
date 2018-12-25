using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkData : NetworkBehaviour {

    public GameObject playerObject;
    private CharacterController player;

	// Use this for initialization
	void Start () {
        CmdSpawnSelf();
	}

    [Command]
    void CmdSpawnSelf() {
        // Executed on the server
        GameObject go = Instantiate(playerObject);

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
