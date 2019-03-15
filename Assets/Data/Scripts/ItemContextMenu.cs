using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace TInv
{
    public class ItemContextMenu : MonoBehaviour
    {
        public Item item;
        GameObject lastFocused;
        public void Open(ItemData itemData)
        {
            lastFocused = EventSystem.current.currentSelectedGameObject;
            item = itemData.item;
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }
        public void Equip() { }
        public void Drop() { }
        public void Discard() { }
        public void Cancel()
        {
            gameObject.SetActive(false);
            _InventoryManager.instance.EnableAllSlots();
            EventSystem.current.SetSelectedGameObject(lastFocused);
        }
    }
}