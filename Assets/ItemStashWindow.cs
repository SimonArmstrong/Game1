public class ItemStashWindow : ItemContainer {

    public static ItemStashWindow instance;
    
    private void Start() {
        instance = this;
    }

    public void OnOpen(string chestName) {
        ItemSaveManager.instance.LoadItemStash(this, chestName);
    }
    public void OnClose(string chestName){
        ItemSaveManager.instance.SaveItemStash(this, chestName);
    }
}
