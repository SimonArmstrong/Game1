using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DisplayRupees : MonoBehaviour {
    public Player p;
	// Use this for initialization
	void Start () {
        p = GameManager.instance.player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if(p != null)
            GetComponent<TMPro.TextMeshProUGUI>().text = p.money.ToString("00");
	}
}
