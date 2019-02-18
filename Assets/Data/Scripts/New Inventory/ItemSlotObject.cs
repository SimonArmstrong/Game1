using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace TInv
{
    public class ItemSlotObject : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
    {
        public Image image;
        public InventoryObject inventoryObject;
        public int index;
        public TextMeshProUGUI textBox;
        protected ItemData itemData;

        public void OnClick() {
            //if (itemData == null) return;

            return;
            // Open Context Menu
            _InventoryManager.instance.contextMenu.gameObject.SetActive(true);
            _InventoryManager.instance.contextMenu.Open(itemData);
            _InventoryManager.instance.DisableAllSlots();
        }

        public void Refresh() {
            if (itemData == null) {
                image.color = new Color(0, 0, 0, 0);
                textBox.color = new Color(0, 0, 0, 0);
            }
        }

        public void SetItem(ItemData iData) {
            itemData = iData;
            inventoryObject.items[index] = itemData;

            image.color = new Color(0, 0, 0, 0);
            textBox.color = new Color(0, 0, 0, 0);

            if (iData == null) return;
            if (itemData.item == null) return;

            image.sprite = itemData.item.iconAnim.sprites[0];
            textBox.text = itemData.amount.ToString();
            image.color = Color.white;
            textBox.color = iData.item.MaximumStacks > 1 ? Color.white : new Color(0, 0, 0, 0);
        }

        public void OnSelect(BaseEventData data) {
            // Tooltip
            if (GameCursor.instance.controller) {
                GameCursor.instance.GetComponent<RectTransform>().position = (GetComponent<RectTransform>().position + new Vector3(45, -45, 0));
            }
        }

        public void OnDeselect(BaseEventData data) {
            // Disable Tooltip
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Left) {
                SetItem(GameCursor.HoldItem(itemData));
            }

            if (eventData != null && eventData.button == PointerEventData.InputButton.Right) {
                if (itemData != null && itemData.item != null) {
                    SetItem(GameCursor.HoldItem(itemData, 1));
                }
            }
        }
    }
}