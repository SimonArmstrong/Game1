using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAtTime : MonoBehaviour {
    public int time;
    public bool daytime = true;

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(GameManager.instance.time >= time && GameManager.instance.daytime == daytime);
    }
}
