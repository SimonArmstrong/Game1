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

    public static PlayerEntry instance;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);

        instance = this;

        if (GameManager.instance == null)
            Instantiate(gameManagerPrefab).GetComponent<GameManager>().playerEntry = this;
    }
    public void Init()
    {

        if (GameManager.instance.player == null)
        {
            FollowTarget cam = null;
            if (playerCanvasInst == null) playerCanvasInst = Instantiate(playerCanvasPrefab);
            if (playerInst == null) playerInst = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            if (camInst == null) {
                camInst = Instantiate(cameraPrefab);
                cam = camInst.GetComponent<FollowTarget>();
                cam.target = playerInst.transform;
                GameManager.instance.player = playerInst;
            }
            Player player = playerInst.GetComponent<Player>();
            Character character = playerCanvasInst.GetComponentInChildren<Character>();
            player.character = character;

            cam.GetComponent<PixelPerfectCam>().zoom = PlayerSettings.instance.zoom;
            PlayerSettings.instance.onZoom = cam.GetComponent<PixelPerfectCam>().ApplyZoom;

            //character.player = player;
            //character.Init();
            //character.gameObject.SetActive(false);
            TInv.UI_Inventory inv = _InventoryManager.instance.playerInventoryUI;
            inv.inventory = player.GetComponent<TInv.InventoryObject>();
            inv.inventory.LoadInventory();
            inv.Refresh();
            DontDestroyOnLoad(playerCanvasInst);
            //playerCanvasInst.GetComponentInChildren<InventoryManager>().player = playerInst.GetComponent<Player>();

            GameManager.instance.canvas = playerCanvasInst.GetComponent<Canvas>();
        }
    }

    public void RespawnPlayer() {
        playerInst.SetActive(true);
        playerInst.GetComponent<Player>().OnHit(9999999);
    }


    // Update is called once per frame
    void LateUpdate () {
        /*
        if (playerInst.activeSelf == false)
        {
            GameManager.instance.died = true;
        }
        */
	}
}
