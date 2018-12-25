using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupNotification : MonoBehaviour {
    private SpriteRenderer sr;
    public float timer = 1.2f;
    private bool start = false;

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        //Refresh();
    }

    public void Display(Sprite sprite) {
        if (sprite != null)
        {
            start = true;
            sr.color = new Color(1, 1, 1, 1);
            sr.sprite = sprite;
        }
        else
        {
            sr.color = new Color(1, 1, 1, 0);
            sr.sprite = null;
        }
    }

    private void Update()
    {
        if (start) {
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0) {
                timer = 1.2f;
                start = false;
                sr.sprite = null;
            }
        }
    }
}
