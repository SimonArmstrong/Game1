using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public SoundFont sound;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p != null)
        {
            p.ppn.Display(GetComponent<SpriteRenderer>().sprite);
            Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = sound.GetClip();
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
    }
}
