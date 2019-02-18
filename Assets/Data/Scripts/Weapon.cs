using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class Weapon : EquippableItem {
    public GameObject dcUp;
    public GameObject dcDown;
    public GameObject dcLeft;
    public GameObject dcRight;

    [Header("Bonus Objects To Spawn")]
    public GameObject projectile;
    public GameObject effectObject;
    public GameObject otherObject;

    public float attackSpeed = 1.0f;

    public List<StatusCondition> conditions;

    public Weapon()
    {
        gearType = GearType.WEAPON;
    }
}
