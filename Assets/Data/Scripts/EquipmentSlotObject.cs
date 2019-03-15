using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TInv
{
    public class EquipmentSlotObject : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public GearType gearType;

        public Image image;
        protected EquippableItem item;

        public void OnClick() {
            EquippableItem eqp = null;

            if (GameCursor.instance.heldItem == null) {
                if (item != null) {
                    GameCursor.HoldItem(new ItemData(item, 1));
                    item.Unequip(GameManager.instance.player.GetComponent<Player>());
                }

                SetItem(null);
            }

            else if (GameCursor.instance.heldItem.item is EquippableItem) {
                eqp = (EquippableItem)GameCursor.instance.heldItem.item;
                if (eqp.gearType != gearType) return;

                if (item == null)
                    GameCursor.HoldItem(null);
                else
                    GameCursor.HoldItem(new ItemData(item, 1));

                SetItem(eqp);
                eqp.Equip(GameManager.instance.player.GetComponent<Player>());
            }
        }

        public void SetItem(EquippableItem eqp)
        {
            item = eqp;
            image.color = new Color(0, 0, 0, 0);

            if (item == null) return;

            image.sprite = item.iconAnim.sprites[0];
            image.color = Color.white;
        }

        public void OnSelect(BaseEventData data)
        {
            // Tooltip
            if (GameCursor.instance.controller)
            {
                GameCursor.instance.GetComponent<RectTransform>().position = (GetComponent<RectTransform>().position + new Vector3(45, -45, 0));
            }
        }

        public void OnDeselect(BaseEventData data)
        {
            // Disable Tooltip
        }
    }
}