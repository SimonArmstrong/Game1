using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer 
{
    [SerializeField] Transform itemsParent;
    
    protected override void Awake() {
        base.Awake();
    }

    protected override void OnValidate() {
        if (itemsParent != null){
            itemSlots = new ItemSlot[itemsParent.childCount];
            for (int i = 0; i < itemsParent.childCount; i++) {
                itemSlots[i] = itemsParent.GetChild(i).GetChild(0).GetComponent<ItemSlot>();
            }
        }
    }
    
}
