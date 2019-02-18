using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public BaseItemSlot dragItemSlot;
    public static InventoryManager instance;
    [SerializeField] Image draggableItem;

    public Player player;

    private void Awake() {
        instance = this;
    }

    public void BeginDrag(BaseItemSlot itemSlot) {
        if (itemSlot.Item != null) {
            dragItemSlot = itemSlot;
            draggableItem.sprite = dragItemSlot.Item.iconAnim.sprites[0];
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    public void EndDrag(BaseItemSlot itemSlot) {
        dragItemSlot = null;
        draggableItem.enabled = false;
    }

    public void Drag(BaseItemSlot itemSlot) {
        if (draggableItem.enabled) {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    public void Drop(BaseItemSlot dropItemSlot) {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStack(dragItemSlot.Item)) {
            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item)) {
            SwapItems(dropItemSlot);
        }
    }

    public void DropItemOutsideUI() {
        if (dragItemSlot == null) return;

        player.character.questionDialogue.Show();
        BaseItemSlot baseItemSlot = dragItemSlot;
        player.character.questionDialogue.OnYesEvent += () => DestroyItemInSlot(baseItemSlot);
    }

    public void DestroyItemInSlot(BaseItemSlot baseItemSlot){
        baseItemSlot.Item.Destroy();
        baseItemSlot.Item = null;
        player.character.questionDialogue.gameObject.SetActive(false);
    }

    public void SwapItems(BaseItemSlot dropItemSlot) {
        EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

        if (dragItemSlot is EquippableSlot) {
            if (dragItem != null) dragItem.Unequip(player);
            if (dropItem != null) dropItem.Equip(player);
        }

        if (dropItemSlot is EquippableSlot) {
            if (dragItem != null) dragItem.Equip(player);
            if (dropItem != null) dropItem.Unequip(player);
        }
        player.character.statPanel.UpdateStatValues();

        Item draggedItem = dragItemSlot.Item;
        int draggedItemAmount = dragItemSlot.Amount;

        dragItemSlot.Item = dropItemSlot.Item;
        dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    public void AddStacks(BaseItemSlot dropItemSlot) {
        int numAddableStacks = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
    }
}
