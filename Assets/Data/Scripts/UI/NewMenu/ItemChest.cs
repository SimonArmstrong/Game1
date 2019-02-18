using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemChest : Interactable {
    public AudioClip openSound;
    public bool open = false;

    public UnityEvent uEvent;

    public void Awake()
    {
        GetComponent<TInv.InventoryObject>().LoadInventory();
    }

    public override void Interact(Player p)
    {
        open = !open;
        GetComponent<TInv.InventoryObject>().SaveInventory();
        AudioSource a = (Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>());
        a.clip = openSound;
        if (open)
        {
            _InventoryManager.instance.otherInventoryUI.inventory = GetComponent<TInv.InventoryObject>();
            _InventoryManager.instance.otherInventoryUI.OnOpen();
        }
        else {
            _InventoryManager.instance.otherInventoryUI.OnClose();
        }
        base.Interact(p);
    }
}
