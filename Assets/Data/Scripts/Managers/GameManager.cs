using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DunGen;
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
    public GameObject genericShadowObject;
    public GameObject genericHitObject;
    public GameStates state;
    public float sortingFidelity = 100;

    public float time;
    public float dayLength = 360;
    public float nightLength = 360;
    public bool daytime;
    public bool nighttime = true;

    public bool died = false;

    [HideInInspector]
    public GameObject player;
    public PlayerEntry playerEntry;

    public GridScript curGrid;
    public Canvas canvas;

    [HideInInspector]
    public ObjectPooler objPooler;
    [HideInInspector]
    public List<GameObject> activeEnemies = new List<GameObject>();

    #region DamageNumbers
    [SerializeField]
    public List<DamageNumber> dmgNums = new List<DamageNumber>();
    public int[] dmgNumsToRemove = new int[0];

    [System.Serializable]
    public class DamageNumber
    {
        public GameObject go;
        public float lifetime;

        public DamageNumber(GameObject m_go, float m_lifetime)
        {
            this.go = m_go;
            this.lifetime = m_lifetime;
        }
    }
    #endregion

    void Awake () {
        instance = this;

        state = GameStates.playing;
	}

    private void Start()
    {
        if(playerEntry != null)
            playerEntry.Init();
        objPooler = GetComponent<ObjectPooler>();
        objPooler.Init();
    }
    
    float spawnTime = 4;

    void Update () {
        time += Time.deltaTime;
        if (daytime)
        {
            if (time >= dayLength)
            {
                daytime = false;
                nighttime = true;
                time = 0;
            }
        }
        else if (nighttime) {
            if (time >= nightLength)
            {
                daytime = true;
                nighttime = false;
                time = 0;
            }
        }

        if (died) {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0) {
                died = false;
                spawnTime = 4;
                playerEntry.RespawnPlayer();
            }
        }
        if (dmgNums.Count > 0)
        {
            for (int i = 0; i < dmgNums.Count; i++)
            {
                dmgNums[i].go.transform.position += Vector3.up * Time.deltaTime * 80;
                dmgNums[i].lifetime -= Time.deltaTime;
                if (dmgNums[i].lifetime <= 0)
                {
                    dmgNums[i].lifetime = 1;
                    dmgNums[i].go.SetActive(false);
                    dmgNums.RemoveAt(i);
                    //dmgNumsToRemove = AddValueToArray(i, dmgNumsToRemove);
                }
            }
        }
        for (int i = 0; i < dmgNumsToRemove.Length; i++)
        {
            dmgNums.RemoveAt(dmgNumsToRemove[i]);
            //Debug.Log("Damage Number " + dmgNums[i].go.ToString() + " removed");
        }
        dmgNumsToRemove = new int[0];
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

    public void UpdateCurGrid(GridScript grid)
    {
        curGrid = grid;
    }

    public void SpawnEntities(SpawnPoint[] spawnPoints)
    {
        activeEnemies = new List<GameObject>();
        foreach (SpawnPoint sp in spawnPoints)
        {
            activeEnemies.Add(objPooler.GetFromPool(sp.tag, true, sp.transform.position, Quaternion.identity));
        }
        //activeEnemies[activeEnemies.Count - 1].SetActive(true);
    }

    public void KillEntity(GameObject go)
    {
        go.SetActive(false);
        activeEnemies.Remove(go);
    }

    public void ClearAllActiveEntities()
    {
        int activeEnemiesCount = activeEnemies.Count;
        for(int i = 0; i < activeEnemiesCount; i++)
        {
            KillEntity(activeEnemies[i]);
        }
    }

    public void SpawnDmgNum(Transform location, int dmg)
    {
        Vector3 rn = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        Vector3 tempPoint = Camera.main.WorldToScreenPoint(location.position + (rn * 0.2f));
        if (tempPoint != Vector3.zero)
        {
            GameObject obj = ObjectPooler.Instance.GetFromPool("dmg_num", false, tempPoint, Quaternion.identity);
            obj.GetComponent<TMPro.TextMeshProUGUI>().SetText(dmg.ToString());
            dmgNums.Add(new DamageNumber(obj, 1.0f));
            obj.SetActive(true);
        }
    }
}