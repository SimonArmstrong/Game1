using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHeart : MonoBehaviour {
    public Sprite on;
    public Sprite off;

    public bool isOn;

    public void Refresh() {
        GetComponent<Image>().sprite = isOn ? on : off;
    }
}
