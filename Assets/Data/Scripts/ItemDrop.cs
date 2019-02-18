using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {
    public Item item;
    public SoundFont pickupSound;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.iconAnim.sprites[0];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p != null) {
            p.GiveItem(item);
            Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = pickupSound.GetClip();
            Cutscene c = Instantiate(GameManager.instance.genericCutsceneObject).GetComponent<Cutscene>();
            GetComponent<DialogueTrigger>().TriggerDialogue();
            c.cam.actor = Camera.main.gameObject;
            c.actors.Add(new SceneShot());
            c.actors[0].actor = gameObject;
            c.cam.positions.Add(c.cam.actor.transform);
            c.cam.transitionTimes.Add(4);
            c.cam.holdTimes.Add(3);
            transform.position = p.itemShowTransform.position;
            //Destroy(gameObject);
        }
    }
}
