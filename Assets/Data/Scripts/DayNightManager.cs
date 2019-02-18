using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour {
    public Color nightColor;
    public Color dayColor;

    public Color current;

    public Gradient colorCycle;
    public AudioClip nightMusic;
    public AudioClip dayMusic;

    Light light;

    float maxVal = 10;
    float v = 0;
    private void Start()
    {
        light = GetComponent<Light>();
    }
    // Update is called once per frame
    void Update () {
        if (GameManager.instance.nighttime)
        {
            //light.color = Color.Lerp(light.color, nightColor, Time.deltaTime * 3); 
            light.color = colorCycle.Evaluate(0.5f + ((GameManager.instance.time / GameManager.instance.nightLength) * 0.5f));
            GetComponent<AudioSource>().clip = nightMusic;
            if(!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
        }
        if (GameManager.instance.daytime)
        {
            light.color = colorCycle.Evaluate((GameManager.instance.time / GameManager.instance.dayLength) * 0.5f);
            GetComponent<AudioSource>().clip = dayMusic;
            if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
        }
	}
}
