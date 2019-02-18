using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlotData {
    public Item item;
    public int amount;

    public ItemSlotData(Item i, int amt) {
        item = i;
        amount = amt;
    }
}

public class ItemCollection : MonoBehaviour {
    public List<ItemSlotData> items = new List<ItemSlotData>();
}
