using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstruction : MonoBehaviour {
    public bool shouldUpdate = false;

    public SpriteRenderer sr;

    private void Start()
    {
        if(sr == null) sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null) {
            shouldUpdate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<Player>() != null)
        {
            shouldUpdate = false;
        }
    }

    public void Update()
    {
        if (shouldUpdate)
        {
            sr.color = Color.Lerp(sr.color, new Color(1, 1, 1, 0.4f), Time.deltaTime * 8);
        }
        else {
            sr.color = Color.Lerp(sr.color, new Color(1, 1, 1, 1f), Time.deltaTime * 8);
        }
    }
}
