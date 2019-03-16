using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviour2D : MonoBehaviour {
    public LayerShadow shadow;
    public float height2D = 0;
    public float shadowHeightFineOffset = 0;
    public int shadowHeightUnitOffset = 0;
    public int shadowLayerOffset = -7;
    
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

        shadow.offset = shadowLayerOffset;
        shadow.target = transform;
        shadow.height2D = height2D + (shadowHeightUnitOffset / 25) + (shadowHeightFineOffset / 25);
    }
}
