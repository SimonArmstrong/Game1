using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCondition : MonoBehaviour {
    public string displayName;
    public float strength;
    public float duration;
    public Sprite icon;
    [TextArea(5,3)]
    public string description;
    public Entity target;

    private float conditionTimer;
    public Attributes attributeMod;

    public virtual void Start(){
        conditionTimer = duration;
    }

    public virtual void Calculate(){
        
    }
    
    public virtual void Update(){
        conditionTimer -= Time.deltaTime;
    }
}
