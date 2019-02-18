using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : MonoBehaviour {
    [SerializeField] ItemDatabase itemDatabase;

    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";
    private const string ItemStashFileName = "Item Stash";

    public static ItemSaveManager instance;

    public void Awake() {
        instance = this;
    }

    public void LoadInventory(Character character) {
        ItemContainerSaveData savedSlots = ItemSaveIO._LoadItems(InventoryFileName);
        if(savedSlots == null) return;
        character.inventory.Clear();

        for (int i = 0; i < savedSlots.SavedSlots.Length; i++) {
            ItemSlot itemSlot = character.inventory.itemSlots[i];
            ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

            if(savedSlot == null){
                itemSlot.Item = null;
                itemSlot.Amount = 0;
            }
            else{
                itemSlot.Item = itemDatabase.GetItemCopy(savedSlot.ItemID);
                itemSlot.Amount = savedSlot.Amount;
            }
        }

    }

    public void LoadEquipment(Character character){
        ItemContainerSaveData savedSlots = ItemSaveIO._LoadItems(EquipmentFileName);
        if (savedSlots == null) return;
        foreach (ItemSlotSaveData savedSlot in savedSlots.SavedSlots) {
            if(savedSlot == null){
                continue;
            }

            Item item = itemDatabase.GetItemCopy(savedSlot.ItemID);
            character.inventory.AddItem(item);
            character.Equip((EquippableItem)item);
        }
    
    }

    public void LoadItemStash(ItemStashWindow itemStashWindow, string ItemStashID){
        ItemContainerSaveData savedSlots = ItemSaveIO._LoadItems(ItemStashID);
        if (savedSlots == null) return;
        itemStashWindow.Clear();

        for (int i = 0; i < savedSlots.SavedSlots.Length; i++) {
            ItemSlot itemSlot = itemStashWindow.itemSlots[i];
            ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

            if (savedSlot == null) {
                itemSlot.Item = null;
                itemSlot.Amount = 0;
            }
            else {
                itemSlot.Item = itemDatabase.GetItemCopy(savedSlot.ItemID);
                itemSlot.Amount = savedSlot.Amount;
            }
        }
    }

    public void SaveInventory(Inventory inventory) {
        SaveItems(inventory.itemSlots, InventoryFileName);
    }

    public void SaveEquipment(EquipmentPanel equipmentPanel) {
        SaveItems(equipmentPanel.equipmentSlots, EquipmentFileName);
    }

    public void SaveItemStash(ItemStashWindow itemStashWindow, string itemStashID){
        SaveItems(itemStashWindow.itemSlots, itemStashID);
    }

    private void SaveItems(IList<ItemSlot> itemSlots, string fileName){
        var saveData = new ItemContainerSaveData(itemSlots.Count);
        for (int i = 0; i < saveData.SavedSlots.Length; i++) {
            ItemSlot itemSlot = itemSlots[i];
            
            if (itemSlot.Item == null){
                saveData.SavedSlots[i] = null;
            } else {
                saveData.SavedSlots[i] = new ItemSlotSaveData(itemSlot.Item.ID, itemSlot.Amount);
            }
        }

        ItemSaveIO.SaveItems(saveData, fileName);
    }

}
