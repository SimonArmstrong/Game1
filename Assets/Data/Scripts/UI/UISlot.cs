using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour {
    public Item item;
    public Image itemIcon;

    public void OnValidate() {
        if(item == null)
            itemIcon.GetComponent<Image>();
    }

    public void OnEnable()
    {
        UpdateSlot();
    }

    public void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot() {
        if (item == null) {
            itemIcon.sprite = null;
            itemIcon.color = new Color(0, 0, 0, 0);
            return;
        }
        if (item.iconAnim.sprites[0] != null)
        {
            itemIcon.color = new Color(1, 1, 1, 1);
            itemIcon.sprite = item.iconAnim.sprites[0];
        }
    }

    public void OnDrag() {
        if (GameCursor.instance.heldItem != null)
        {
            if (item == null) {
                item = GameCursor.instance.heldItem.item;
                GameCursor.instance.heldItem = null;
            }
        }
        else
        {
            //GameCursor.instance.heldItem = item;
            item = null;
        }
        UpdateSlot();
    }
    //public void 
    public void MouseUp()
    {
        if (GameCursor.instance.heldItem != null)
        {
            if (item == null)
            {
                // If there isn't an item in the slot
                //item = GameCursor.instance.heldItem;
                GameCursor.instance.heldItem = null;
            }
            else
            {
                // If there is an item in the slot
                Item tempItem = item;
               // item = GameCursor.instance.heldItem;
             //   GameCursor.instance.heldItem = tempItem;
            }
        }
        UpdateSlot();
    }

    public void MouseEnter() {
        GameCursor.instance.targetItem = item;
    }

    public void MouseExit()
    {
        GameCursor.instance.targetItem = null;
    }
}
