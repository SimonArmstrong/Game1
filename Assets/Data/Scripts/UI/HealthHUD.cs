using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour {
    public Entity entity;
    private int hearts;
    private int currentHearts;

    public GameObject UIHeartContainer;

    public List<HUDHeart> hudHearts = new List<HUDHeart>();

    public static HealthHUD instance;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        UpdateHearts();
	}

    public void UpdateHearts() {
        hearts = (int)entity.attributes.maxHP;
        currentHearts = (int)entity.hp;

        for (int i = 0; i < hearts; i++) {
            if(hudHearts.Count <= i)
                hudHearts.Add(Instantiate(UIHeartContainer, transform).GetComponent<HUDHeart>());

            hudHearts[i].isOn = false;
        }

        for (int i = 0; i < currentHearts; i++) {
            hudHearts[i].isOn = true;
        }

        for (int i = 0; i < hearts; i++)
        {
            hudHearts[i].Refresh();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
