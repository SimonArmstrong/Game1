using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntry : MonoBehaviour {
    public GameObject gameManagerPrefab;
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public GameObject playerCanvasPrefab;

    public GameObject playerInst;
    public GameObject playerCanvasInst;
    public GameObject camInst;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);

        if (GameManager.instance == null)
            Instantiate(gameManagerPrefab).GetComponent<GameManager>().playerEntry = this;
    }
    public void Init()
    {

        if (GameManager.instance.player == null)
        {
            if (playerCanvasInst == null) playerCanvasInst = Instantiate(playerCanvasPrefab);
            if (playerInst == null) playerInst = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            if (camInst == null)
            {
                camInst = Instantiate(cameraPrefab);
                camInst.GetComponent<FollowTarget>().target = playerInst.transform;
                GameManager.instance.player = playerInst;
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
