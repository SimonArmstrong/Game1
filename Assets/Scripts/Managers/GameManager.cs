using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates {
    mainMenu,
    paused,
    playing,
    cutscene,
    loading
}

public class GameManager : MonoBehaviour {
    public ItemDatabase itemDatabase;
    public static GameManager instance;
    public GameObject genericSoundObject;
    public GameObject genericCutsceneObject;
    public GameObject genericItemDropObject;
    public GameStates state;
    public float sortingFidelity = 100;
    [HideInInspector]
    public GameObject player;
    public PlayerEntry playerEntry;
    // Use this for initialization
    void Awake () {
        instance = this;

        state = GameStates.playing;
	}

    private void Start()
    {
        playerEntry.Init();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ChangeState(GameStates s) {
        state = s;
        if (state == GameStates.paused)
        {
            Time.timeScale = 0;
        }
        else if (state == GameStates.playing)
        {
            Time.timeScale = 1;
        }
        else if (state == GameStates.cutscene)
        {
            Time.timeScale = 0;
        }
    }
}
