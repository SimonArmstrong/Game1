using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquippableSlot : ItemSlot {
    public GearType gearType;

    protected override void OnValidate(){
        base.OnValidate();
        gameObject.name = gearType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item) {
        if(item == null){
            return true;
        }

        EquippableItem equippableItem = item as EquippableItem;

        return equippableItem != null && equippableItem.gearType == gearType;
    }
}
