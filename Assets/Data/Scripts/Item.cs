using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GearType {
    HEAD,
    TORSO,
    HANDS,
    LEGS,
    FEET,
    WEAPON,
    SHIELD,
    CONSUMABLE,
    QUEST,
    AMMO
}

[CreateAssetMenu(fileName="Item")]
public class Item : ScriptableObject {
    public Sprite icon;
    public float value;
    public string id;
}
