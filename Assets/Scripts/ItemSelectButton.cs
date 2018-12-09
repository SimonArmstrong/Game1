using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectButton : MonoBehaviour {
    public int itemID;
    public enum Type {
        TOP,
        BOTTOM,
        FEET,
        GLOVES,
        EYES,
        HAIR,
        HEAD
    }

    public Type type;

    public void Click() {
        switch (type) {
            case Type.TOP:
                (CreationManager.instance.character.equipment.top = new SerializedItemData()).itemID = itemID;
                CreationManager.instance.ReloadModel();

                break;
            case Type.BOTTOM:
                (CreationManager.instance.character.equipment.bottom = new SerializedItemData()).itemID = itemID;
                CreationManager.instance.ReloadModel();
                break;
            case Type.FEET:
                (CreationManager.instance.character.equipment.feet = new SerializedItemData()).itemID = itemID;
                CreationManager.instance.ReloadModel();
                break;
            case Type.GLOVES:
                (CreationManager.instance.character.equipment.gloves = new SerializedItemData()).itemID = itemID;
                CreationManager.instance.ReloadModel();
                break;
            case Type.EYES:
                (CreationManager.instance.character.equipment.eyes = new SerializedItemData()).itemID = itemID;
                CreationManager.instance.ReloadModel();
                break;
            case Type.HAIR:
                (CreationManager.instance.character.equipment.hair = new SerializedItemData()).itemID = itemID;
                CreationManager.instance.ReloadModel();
                break;
            case Type.HEAD:
                //(CreationManager.instance.character.equipment.top = new SerializedItemData()).itemID = itemID;
                CreationManager.instance.ReloadModel();
                break;
        }
    }
}
