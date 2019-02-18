using UnityEditor;
using UnityEngine;


public class ItemStash : Interactable {

    private string itemStashID;

    public Animator anim;
    public AudioClip openSound;
    public ItemCollection items;
    
    public bool isOpen;

    public Player player;

    public void Start(){
        isOpen = false;

        itemStashID = name;
    }
    
    public override void Interact(Player p) {

        //anim.SetTrigger("opened");
        player = p;
        AudioSource a = (Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>());
        a.clip = openSound;
        isOpen = !isOpen;
        if (isOpen) {
            p.character.itemStashWindow.OnOpen(itemStashID);
            p.character.itemStashWindow.gameObject.SetActive(isOpen);
            p.character.OpenItemContainer(p.character.itemStashWindow);
        }
        else {
            p.character.itemStashWindow.gameObject.SetActive(isOpen);
            p.character.CloseItemContainer(p.character.itemStashWindow);
            p.character.itemStashWindow.OnClose(itemStashID);
        }
        base.Interact(p);
    }

    public void OnOpenedFinish() {

    }

    /*TODO:
    need to implement a method that will close the Inventory UI when you are out of range
    just call character.CloseItemContainer(this) when the player is out of range of the chest
    */
    private void Update() {
        if ((transform.position - GameManager.instance.player.transform.position).magnitude > 1f && isOpen) {
            isOpen = false;
            player.character.itemStashWindow.gameObject.SetActive(false);
            player.character.CloseItemContainer(player.character.itemStashWindow);
            player.character.itemStashWindow.OnClose(itemStashID);
        }
    }
}
