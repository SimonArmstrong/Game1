using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour {
    public Color nightColor;
    public Color dayColor;

    public Color current;

    public bool daytime;
    public bool indoors;

    float maxVal = 10;
    float v = 0;

    // Update is called once per frame
    void Update () {
        if (!daytime)
        {
            if (v >= -maxVal) v -= Time.deltaTime * 5;
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, nightColor, v);
        }
        else
        {
            if (v <= maxVal) v += Time.deltaTime * 5;
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, dayColor, v);
        }

        if (indoors) {
            if (v <= maxVal) v += Time.deltaTime * 5;
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, nightColor, v);
        }
	}
}
