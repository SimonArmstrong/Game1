using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {
    Fire,       // beats Ice, Grass
    Ice,        // beats Water, Spirit
    Lightning,  // beats Water, Void
    Void,       // beats Spirit, Ice
    Spirit,     // beats Void, Lightning
    Earth,      // beats Fire, 
    Grass,
    Air,
    Water,

}

[CreateAssetMenu(fileName = "Status Condition")]
public class StatusCondition : ScriptableObject {
    public enum Type {
        Stun,
        DoT,
        Effect
    }

    public Type type;
    public string displayName;
    public float strength;
    public float duration;
    public Sprite icon;
    [TextArea(5,3)]
    public string description;
    public Entity target;

    public Element element;
    private float conditionTimer;
    public Attributes attributeMod;
    [Tooltip("Instantiates on top of the player when this condition is applied")] public GameObject effectObject;

    public virtual void Start(){
        conditionTimer = duration;
    }

    public virtual void Calculate(){
        
    }
    
    public virtual void Update(){
        conditionTimer -= Time.deltaTime;
    }
}
