using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class PlayerSettings : MonoBehaviour {
    public static PlayerSettings instance;
    private void Awake()
    {
        instance = this;
    }
    [Header("General")]
    public bool attackTowardsMouse = false;
    public bool useController = false;

    [Header("Video")]
    public float zoom = 300;

    [Header("Audio")]
    public float master = 1;

    [Header("Controls")]
    public string attackInput = "Fire1";

    [Header("References")]
    public Slider cameraZoomSlider;

    public delegate void UpdateZoom(float z);
    public UpdateZoom onZoom;

    public void UpdateZoomSlider() {
        zoom = cameraZoomSlider.value;
        onZoom(zoom);
    }
}
