using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public Inventory inventory;
    public ItemStashWindow itemStashWindow;
    public EquipmentPanel equipmentPanel;
    public StatPanel statPanel;
    [SerializeField] CraftingWindow craftingWindow;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] DropItemArea dropItemArea;
    public QuestionDialogue questionDialogue;

    [SerializeField] GameObject curPanel;

    public Player player;
    public GameObject equipPanel;
    public GameObject invPanel;
    public GameObject craftPanel;
    public GameObject settingsPanel;

    private InventoryManager im;
    [SerializeField] ItemSaveManager itemSaveManager;

    private void OnValidate() {
        if(itemTooltip == null){
            itemTooltip = FindObjectOfType<ItemTooltip>(); 
        }
    }

    private void Awake() {
        itemSaveManager = ItemSaveManager.instance;
    }

    public void Init() {
        statPanel.SetStats(player.Health, player.Mana, player.Stamina, player.Attack, player.MagicAttack, player.BowAttack, player.Defense, player.AttackSpeed, player.Vitality, player.Crit, player.Spirit, player.Endurance, player.Strength, player.Dexterity, player.Intelligence, player.Agility, player.Toughness);
        statPanel.UpdateStatValues();
        im = InventoryManager.instance;
        //Setup Events:
        //RightClick
        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
        //Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        craftingWindow.OnPointerEnterEvent += ShowTooltip;
        //Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        craftingWindow.OnPointerExitEvent += HideTooltip;
        //Begin Drag
        inventory.OnBeginDragEvent += im.BeginDrag;
        equipmentPanel.OnBeginDragEvent += im.BeginDrag;
        //End Drag
        inventory.OnEndDragEvent += im.EndDrag;
        equipmentPanel.OnEndDragEvent += im.EndDrag;
        //Drag
        inventory.OnDragEvent += im.Drag;
        equipmentPanel.OnDragEvent += im.Drag;
        //Drop
        inventory.OnDropEvent += im.Drop;
        equipmentPanel.OnDropEvent += im.Drop;
        dropItemArea.OnDropEvent += im.DropItemOutsideUI;

        itemSaveManager.LoadEquipment(this);
        itemSaveManager.LoadInventory(this);

        curPanel = invPanel;
    }

    private void OnDestroy() {
        itemSaveManager.SaveEquipment(equipmentPanel);
        itemSaveManager.SaveInventory(inventory);
    }

    public void InventoryRightClick(BaseItemSlot itemSlot){
        if(itemSlot.Item is EquippableItem){
            Equip((EquippableItem)itemSlot.Item);
        }
        else if(itemSlot.Item is UsableItem){
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(player);

            if(usableItem.isConsumable){
                inventory.RemoveItem(usableItem);
                usableItem.Destroy();
            }
        }
        else if(itemSlot.Item is PlaceableItem){
            PlaceableItem placeable = (PlaceableItem)itemSlot.Item;
            placeable.Place(player);

            placeable.Destroy();
        }
    }

    public void EquipmentPanelRightClick(BaseItemSlot itemSlot) {
        if (itemSlot.Item is EquippableItem) {
            Unequip((EquippableItem)itemSlot.Item);
        }
    }

    public void ShowTooltip(BaseItemSlot itemSlot){
        if (itemSlot.Item != null) {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }

    public void HideTooltip(BaseItemSlot itemSlot) {
        itemTooltip.HideTooltip();
    }

    public void Equip(EquippableItem item){

        //We'll want to add a way to equip items from anywhere and not just the inventory
        if(inventory.RemoveItem(item)){
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem)){
                if(previousItem != null){
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(player);
                    statPanel.UpdateStatValues();
                }
                item.Equip(player);
                statPanel.UpdateStatValues();
            }
            else{
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item){
        if(inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item)){
            item.Unequip(player);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }

    public void OnInventoryButtonClicked(){
        if(curPanel != invPanel && curPanel != null) {
            curPanel.SetActive(false);
            curPanel = invPanel;
            curPanel.SetActive(true);
        }

        if(!invPanel.activeSelf)
            invPanel.SetActive(true);
    }

    public void OnEquipmentButtonClicked() {
        if (curPanel != equipPanel && curPanel != null) {
            curPanel.SetActive(false);
            curPanel = equipPanel;
            curPanel.SetActive(true);
        }

        if (!equipPanel.activeSelf)
            equipPanel.SetActive(true);
    }

    public void OnCraftingButtonClicked() {
        if (curPanel != craftPanel && curPanel != null) {
            curPanel.SetActive(false);
            curPanel = craftPanel;
            curPanel.SetActive(true);
        }

        if (!craftPanel.activeSelf)
            craftPanel.SetActive(true);
    }

    public void OnSettingsButtonClicked(){
        if (curPanel != settingsPanel && curPanel != null) {
            curPanel.SetActive(false);
            curPanel = settingsPanel;
            curPanel.SetActive(true);
        }

        if (!settingsPanel.activeSelf)
            settingsPanel.SetActive(true);
    }

    public void UpdateStatValues() {
        statPanel.UpdateStatValues();
    }

    //reference to the chest
    private ItemContainer openItemContainer;

    //transfer from player inventory to chest
    public void TransferToItemContainer(BaseItemSlot itemSlot){
        Item item = itemSlot.Item;
        if(item != null && openItemContainer.CanAddItem(item)){
            inventory.RemoveItem(item);
            openItemContainer.AddItem(item);
        }
    }

    //transfer from chest to player inventory
    private void TransferToInventory(BaseItemSlot itemSlot){
        Item item = itemSlot.Item;
        if (item != null && inventory.CanAddItem(item)) {
            openItemContainer.RemoveItem(item);
            inventory.AddItem(item);
        }
    }

    //assign events so that we're interacting with the chest
    public void OpenItemContainer(ItemContainer itemContainer){
        openItemContainer = itemContainer;

        inventory.OnRightClickEvent -= InventoryRightClick;
        inventory.OnRightClickEvent += TransferToItemContainer;

        itemContainer.OnRightClickEvent += TransferToInventory;

        itemContainer.OnPointerEnterEvent += ShowTooltip;
        itemContainer.OnPointerExitEvent += HideTooltip;
        itemContainer.OnBeginDragEvent += im.BeginDrag;
        itemContainer.OnEndDragEvent += im.EndDrag;
        itemContainer.OnDragEvent += im.Drag;
        itemContainer.OnDropEvent += im.Drop;
    } 
    
    //reassign the events to what they were so we're interacting within our inventory
    public void CloseItemContainer(ItemContainer itemContainer){
        openItemContainer = null;

        inventory.OnRightClickEvent += InventoryRightClick;
        inventory.OnRightClickEvent -= TransferToItemContainer;

        itemContainer.OnRightClickEvent -= TransferToInventory;

        itemContainer.OnPointerEnterEvent -= ShowTooltip;
        itemContainer.OnPointerExitEvent -= HideTooltip;
        itemContainer.OnBeginDragEvent -= im.BeginDrag;
        itemContainer.OnEndDragEvent -= im.EndDrag;
        itemContainer.OnDragEvent -= im.Drag;
        itemContainer.OnDropEvent -= im.Drop;
    }
}
