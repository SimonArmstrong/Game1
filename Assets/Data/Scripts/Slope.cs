using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slope : MonoBehaviour {

    float tempMs;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity e = collision.GetComponent<Entity>();
        if (e != null) {
            tempMs = e.attributes.moveSpeed;
            e.attributes.moveSpeed = tempMs * 0.75f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity e = collision.GetComponent<Entity>();
        if (e != null)
        {
            e.attributes.moveSpeed = tempMs;
        }
    }
}
