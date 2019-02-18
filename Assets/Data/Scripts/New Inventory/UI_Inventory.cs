using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TInv
{
    public class UI_Inventory : MonoBehaviour {
        public InventoryObject inventory;
        public Transform parentObject;
        public GameObject itemSlotObject;
        public ItemSlotObject[] itemSlotInstances;

        public AudioClip openSound;
        public AudioClip closeSound;

        private int slotCount = 36;

        public static UI_Inventory instance;

        public void Refresh() {
            if (inventory == null) return;

            foreach (ItemSlotObject g in itemSlotInstances) {
                Destroy(g.gameObject);
            }

            slotCount = inventory.slots;
            itemSlotInstances = new ItemSlotObject[slotCount];
            for (int i = 0; i < slotCount; i++)
            {
                itemSlotInstances[i] = Instantiate(itemSlotObject, parentObject).GetComponent<TInv.ItemSlotObject>();
            }
        }

        public void MoveItem() {

        }

        public void OnOpen() {
            Refresh();
            Time.timeScale = 0;
            //_InventoryManager.instance.pauseMenu.SetActive(true);
            transform.parent.parent.gameObject.SetActive(true);
            // Animations?
            AudioSource a = (Instantiate(GameManager.instance.genericSoundObject).GetComponent<AudioSource>());
            a.clip = openSound;
            a.volume = 0.1f;
            for (int i = 0; i < inventory.slots; i++) {
                itemSlotInstances[i].inventoryObject = inventory;
                itemSlotInstances[i].index = i;
                itemSlotInstances[i].SetItem(inventory.items[i]);
            }
            EventSystem.current.SetSelectedGameObject(itemSlotInstances[0].gameObject);
            if (GameCursor.instance.controller)
            {
                GameCursor.instance.GetComponent<RectTransform>().position = (itemSlotInstances[0].GetComponent<RectTransform>().transform.position + new Vector3(45, -45, 0));
            }
            for (int i = 0; i < itemSlotInstances.Length; i++)
            {
                itemSlotInstances[i].Refresh();
            }
        }

        public void OnClose() {
            Time.timeScale = 1;
            //_InventoryManager.instance.pauseMenu.SetActive(false);
            transform.parent.parent.gameObject.SetActive(false);
            AudioSource a = (Instantiate(GameManager.instance.genericSoundObject).GetComponent<AudioSource>());
            a.clip = closeSound;
            a.volume = 0.1f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                OnClose();
            }
        }
    }
}