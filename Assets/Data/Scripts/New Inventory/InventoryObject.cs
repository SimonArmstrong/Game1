using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TInv
{
    public class InventoryObject : MonoBehaviour
    {
        public ItemDatabase itemDatabase;
        public ItemData[] items;
        public int slots = 16;

        private void Awake()
        {
            //items = new ItemData[slots];
        }

        public bool AddItem(Item item, int amount = (1)) {
            for (int i = 0; i < items.Length; i++) {
                if (items[i] != null)
                {
                    if (items[i].item != null)
                    {
                        if (items[i].item.databaseID == item.databaseID && items[i].item.MaximumStacks > 1)
                        {
                            items[i].amount += amount;
                            return true;
                        }
                    }
                }
            }

            for (int i = 0; i < items.Length; i++) {
                if (items[i] == null || items[i].item == null) {
                    items[i] = new ItemData(item, amount);
                    return true;
                }
            }
            
            return false;
        }

        public void RemoveItem(Item item, int amount = (1)) {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].item.databaseID == item.databaseID) {
                    items[i].amount -= amount;
                    if (items[i].amount <= 0) {
                        items[i].item = null;
                    }
                }
            }
        }

        public void SaveInventory() {
            var data = new ContainerSaveData(slots);
            for (int i = 0; i < data.savedSlots.Length; i++) {
                if (items[i] != null)
                {
                    if (items[i].item == null)
                    {
                        data.savedSlots[i] = null;
                    }
                    else
                    {
                        data.savedSlots[i] = new ItemSaveData(items[i].item, items[i].amount);
                    }
                }
                else {
                    data.savedSlots[i] = null;
                }
            }

            ItemSaveIO.SaveItems(data, gameObject.name);
        }

        public void LoadInventory() {
            ContainerSaveData data = ItemSaveIO.LoadItems(gameObject.name);

            if (data == null) return;
            ItemData[] idata = new ItemData[data.savedSlots.Length];

            for (int i = 0; i < data.savedSlots.Length; i++) {
                if (data.savedSlots[i] != null) {
                    Item item = itemDatabase.GetItemCopy(data.savedSlots[i].itemID);
                    idata[i] = new ItemData(item, data.savedSlots[i].amount);
                }
            }

            items = idata;
        }
    }

    [System.Serializable]
    public class ItemData
    {
        public Item item;
        public int amount;

        public ItemData(Item i, int amt = (1))
        {
            item = i;
            amount = 0;
            if (item == null) return;
            if (item.MaximumStacks <= 1)
                amt = 1;
            amount = amt;
        }
    }

    [System.Serializable]
    public class ItemSaveData
    {
        public string itemID;
        public int amount;

        public ItemSaveData(Item i, int amt = (1))
        {
            itemID = i.ID;
            amount = 0;
            if (i == null) return;
            if (i.MaximumStacks <= 1)
                amt = 1;
            amount = amt;
        }
    }
    [System.Serializable]
    public class ContainerSaveData {
        public ItemSaveData[] savedSlots;

        public ContainerSaveData(int numItems) {
            savedSlots = new ItemSaveData[numItems];
        }
    }
}