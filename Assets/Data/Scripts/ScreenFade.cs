using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour {
    public static ScreenFade instance;
    public Color color;
	// Use this for initialization
	void Awake () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Image>().color != color)
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, color, Time.unscaledDeltaTime * 5);
        }
	}
}
