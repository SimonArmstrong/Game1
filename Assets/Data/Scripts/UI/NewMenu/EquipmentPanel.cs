using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour {

    [SerializeField] Transform equipmentSlotsParent;
    public EquippableSlot[] equipmentSlots;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    private void Start() {
        for (int i = 0; i < equipmentSlots.Length; i++) {
            equipmentSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            equipmentSlots[i].OnPointerExitEvent += slot =>   OnPointerExitEvent(slot);
            equipmentSlots[i].OnRightClickEvent += slot =>     OnRightClickEvent(slot);
            equipmentSlots[i].OnBeginDragEvent += slot =>       OnBeginDragEvent(slot);
            equipmentSlots[i].OnEndDragEvent += slot =>           OnEndDragEvent(slot);
            equipmentSlots[i].OnDragEvent += slot =>                 OnDragEvent(slot);
            equipmentSlots[i].OnDropEvent += slot =>                 OnDropEvent(slot);
        }
    }

    private void OnValidate() {
        equipmentSlots = new EquippableSlot[equipmentSlotsParent.childCount];
        if (equipmentSlotsParent != null) {
            for (int i = 0; i < equipmentSlotsParent.childCount; i++) {
                equipmentSlots[i] = equipmentSlotsParent.GetChild(i).GetChild(0).GetComponent<EquippableSlot>();
            }
        }
    }

    public bool AddItem(EquippableItem item, out EquippableItem previousItem){
        for (int i = 0; i < equipmentSlots.Length; i++) {
            if (equipmentSlots[i].gearType == item.gearType) {
                previousItem = (EquippableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }


    public bool RemoveItem(EquippableItem item){
        for (int i = 0; i < equipmentSlots.Length; i++) {
            if (equipmentSlots[i].Item == item) {
                equipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }

    public EquippableItem GetItemByType(GearType type){
        for (int i = 0; i < equipmentSlots.Length; i++) {
            if(equipmentSlots[i].gearType == type){
                if(equipmentSlots[i].gearType == GearType.WEAPON){
                    return (Weapon)equipmentSlots[i].Item;
                }
                return (EquippableItem)equipmentSlots[i].Item;
            }
        }
        return null;
    }
}
