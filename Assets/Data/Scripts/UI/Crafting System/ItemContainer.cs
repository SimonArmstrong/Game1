﻿using System;
using UnityEngine;
using UnityEditor;

public abstract class ItemContainer : MonoBehaviour, IItemContainer 
{
    //public ReferencedInventory referencedInventory;
    public ItemSlot[] itemSlots;
    
    //public ItemCollection itemCollection;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    protected virtual void OnValidate(){
        itemSlots = GetComponentsInChildren<ItemSlot>(includeInactive: true);
    }

    protected virtual void Awake() {
        for (int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            itemSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
            itemSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
            itemSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
            itemSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
            itemSlots[i].OnDragEvent += slot => OnDragEvent(slot);
            itemSlots[i].OnDropEvent += slot => OnDropEvent(slot);
        }
    }

    public virtual bool CanAddItem(Item item, int amount = 1){
        int freeSpaces = 0;

        foreach(ItemSlot itemSlot in itemSlots){
            if(itemSlot.Item == null || itemSlot.Item.ID == item.ID){
                freeSpaces += item.MaximumStacks - itemSlot.Amount;
            }
        }

        return freeSpaces >= amount;
    }

    public virtual bool AddItem(Item item) {

        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].CanAddStack(item)) {
                itemSlots[i].Amount++;
                itemSlots[i].Item = item;
                return true;
            }
        }

        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item == null) {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }

        return false;
    }

    public virtual bool AddItem(Item item, int amount) {

        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].CanAddStack(item)) {
                itemSlots[i].Item = item;
                itemSlots[i].Amount += amount;
                return true;
            }
        }

        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item == null) {
                itemSlots[i].Item = item;
                itemSlots[i].Amount += amount;
                return true;
            }
        }

        return false;
    }

    public virtual bool RemoveItem(Item item) {

        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item == item) {
                itemSlots[i].Amount--;
                return true;
            }
        }

        return false;
    }

    public Item RemoveItem(string itemID) {
        for (int i = 0; i < itemSlots.Length; i++) {
            Item item = itemSlots[i].Item;
            if(item != null && item.ID == itemID){
                itemSlots[i].Amount--;
                return item;
            }
        }
        return null;
    }
    

    public virtual bool ContainsItem(Item item) {
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item == item) {
                return true;
            }
        }
        return false;
    }

    public virtual int ItemCount(string itemID) {
        int number = 0;
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item != null) {
                if (itemSlots[i].Item.ID == itemID) {
                    number += itemSlots[i].Amount;
                }
            }
        }
        return number;
    }

        
    public ItemSlot[] GetSlots(){
        return itemSlots;
    }

    public virtual void Clear(){
        for (int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].Item = null;
        }
    }
}