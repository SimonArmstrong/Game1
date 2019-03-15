using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _InventoryManager : MonoBehaviour {
    public GameObject pauseMenu;
    public Vector2 screenMousePosition;
    public TInv.UI_Inventory playerInventoryUI;
    public TInv.UI_Inventory otherInventoryUI;
    public TInv.ItemContextMenu contextMenu;

    public static _InventoryManager instance;

    public bool paused = false;

    private void Awake()
    {
        instance = this;
    }

    public void DisableAllSlots() {
        foreach (TInv.ItemSlotObject o in playerInventoryUI.itemSlotInstances) {
            o.GetComponent<Button>().interactable = false;
        }
        foreach (TInv.ItemSlotObject o in otherInventoryUI.itemSlotInstances)
        {
            o.GetComponent<Button>().interactable = false;
        }
    }
    public void EnableAllSlots()
    {
        foreach (TInv.ItemSlotObject o in playerInventoryUI.itemSlotInstances)
        {
            o.GetComponent<Button>().interactable = true;
        }
        foreach (TInv.ItemSlotObject o in otherInventoryUI.itemSlotInstances)
        {
            o.GetComponent<Button>().interactable = true;
        }
    }
    public void Update()
    {
        if (Input.GetButtonDown("Inventory")) {
            paused = !paused;
            if (paused) playerInventoryUI.OnOpen();
            else {
                playerInventoryUI.OnClose();
                playerInventoryUI.inventory.SaveInventory();
            }
        }
    }
}
