using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public Slider ghostSlider;
    public float ghostSpeed = 5;
    [Tooltip("The pixel width of our health bar if we have 50 MAX HP")]
    public float baseWidth = 160;
    public float maxHPReference = 50;
    private float length;
    Vector2 initSize = new Vector2();
    private void Start()
    {
        initSize.x = GetComponent<RectTransform>().sizeDelta.x;
        initSize.y = GetComponent<RectTransform>().sizeDelta.y;
    }
    // Update is called once per frame
    void Update () {
        length = (GameManager.instance.player.GetComponent<Player>().attributes.maxHP / maxHPReference) * baseWidth;
        slider.maxValue = GameManager.instance.player.GetComponent<Player>().attributes.maxHP;
        slider.value = GameManager.instance.player.GetComponent<Player>().hp;
        ghostSlider.maxValue = slider.maxValue;
        ghostSlider.value = Mathf.Lerp(ghostSlider.value, slider.value, Time.deltaTime * ghostSpeed);
        GetComponent<RectTransform>().sizeDelta = new Vector2(length, initSize.y);
        ghostSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(length, initSize.y);
    }
}
