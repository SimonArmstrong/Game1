using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviour2D : MonoBehaviour {
    public LayerShadow shadow;
    public float height2D = 0;
    public int shadowOffset = -7;
    
    public void OnValidate()
    {
        
    }

    private void Start()
    {
        GetComponent<LayerSort>().offset += (int)(height2D * GameManager.instance.sortingFidelity);
    }

    public virtual void Update()
    {
        if (shadow == null) {
            shadow = Instantiate(GameManager.instance.genericShadowObject, transform.position, Quaternion.identity).GetComponent<LayerShadow>();
        }

        shadow.offset = shadowOffset;
        shadow.target = transform;
        shadow.height2D = height2D;
    }
}
