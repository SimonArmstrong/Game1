using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrderManager : MonoBehaviour {
    public List<Collider2D> colliders = new List<Collider2D>();

    void UpdateRenderOrder() {
        float temp = -999999;
        for (int i = 0; i < colliders.Count; i++) {
            if (colliders[i].gameObject.transform.position.y > temp) {
                temp = colliders[i].gameObject.transform.position.y;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliders.Add(collision.GetComponent<Collider2D>());
        UpdateRenderOrder();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders.Remove(collision.GetComponent<Collider2D>());
        UpdateRenderOrder();
    }
}
